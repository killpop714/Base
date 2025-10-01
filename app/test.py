import requests

# Flask 서버 주소 (Cloud Run 배포 후 주소로 바꿔야 함)
BASE_URL = "https://my-flask-app-706305219870.asia-northeast3.run.app"   # 로컬 실행 시
# BASE_URL = "https://my-flask-app-706305219870.asia-northeast3.run.app/"  # Cloud Run URL

def chat_with_ai(message: str):
    url = f"{BASE_URL}/chatMessage"
    payload = {"message": message}
    try:
        response = requests.post(url, json=payload)
        response.raise_for_status()
        data = response.json()
        if "response" in data:
            return data["response"]
        else:
            return f"❌ Error: {data.get('error', 'Unknown error')}"
    except Exception as e:
        return f"❌ Request failed: {e}"

if __name__ == "__main__":
    while True:
        user_msg = input("👤 You: ")
        if user_msg.lower() in ["quit", "exit"]:
            break
        reply = chat_with_ai(user_msg)
        print(f"🤖 Bot: {reply}")