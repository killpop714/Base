// server.js
require("dotenv").config();              // .env 불러오기
const express = require("express");
const bodyParser = require("body-parser");
const OpenAI = require("openai");

const app = express();
app.use(bodyParser.json());

// OpenAI 클라이언트
const client = new OpenAI({
  apiKey: process.env.OPENAI_API_KEY
});

// /talk 라우트
app.post("/talk", async (req, res) => {
  try {
    const { userInput, conversation } = req.body;

    // 기본 요청 payload
    const payload = {
      model: "gpt-4.1-mini",
      input: userInput
    };

    // conversation 값이 있으면만 붙임
    if (conversation && conversation.trim().length > 0) {
      payload.conversation = conversation;
    }

    // OpenAI 호출
    const response = await client.responses.create(payload);

    res.json({
      conversation: response.conversation,  // conv_xxx
      text: response.output_text            // 모델 답변
    });

  } catch (e) {
    console.error("❌ Error:", e);
    res.status(500).json({ error: e.message });
  }
});

// 서버 실행
app.listen(3000, () => {
  console.log("✅ Local server running at http://localhost:3000");
});