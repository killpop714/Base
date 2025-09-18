from openai import OpenAI
import os
from dotenv import load_dotenv

load_dotenv()

client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))


def ChatGpt(userInput=str):
    msg = client.chat.completions.create(
        model="gpt-5-2025-08-07",
        messages=[
            {"role":"user","content":userInput}
        ]
    )

    print(msg.choices[0].message.content)

if __name__ == "__main__":
    ChatGpt("아아 테스트중 어떤가 python으로 굴리고 있는데?")
