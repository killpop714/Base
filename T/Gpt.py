from openai import OpenAI
import os
from dotenv import load_dotenv
from flask import Flask, jsonify, request

load_dotenv()

app = Flask(__name__)
client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))

@app.route("/ChatGptM",methods=["POST"])
def ChatGpt():
    data = request.get_json()
    userInput = data.get("userInput","")

    msg = client.chat.completions.create(
        model="gpt-5-2025-08-07",
        messages=[
            {"role":"user","content":userInput}
        ]
    )
    return msg.choices[0].message.content


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8000, debug=True)
