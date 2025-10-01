from flask import Flask, request, jsonify
from openai import OpenAI
import os # os 모듈을 사용하여 환경 변수를 읽어옵니다.

# 환경 변수에서 API 키를 로드합니다. (로컬 테스트용 기본값은 그대로 둡니다.)
# Cloud Run에서는 우리가 --set-env-vars로 넘긴 값이 사용됩니다.
OPEN_API_KEY = os.getenv("OPEN_API_KEY", "NULL_OR_NONE")

# 서버 시작 시 강제 종료 로직 제거
# if not OPEN_API_KEY or OPEN_API_KEY == "NULL_OR_NONE":
#     print("오류 OPEN_API_KET가 없습니다.")
#     exit(1) # <-- 이 줄을 제거했습니다.

app = Flask(__name__)

# 키가 유효할 때만 client를 초기화하거나, 함수 내에서 오류 처리
if OPEN_API_KEY and OPEN_API_KEY != "NULL_OR_NONE":
    client = OpenAI(api_key=OPEN_API_KEY)
else:
    client = None # 클라이언트 초기화 실패 플래그

@app.route('/chatMessage',methods=['POST'])
def chatMessage():
    if client is None:
        # 키가 설정되지 않았을 경우, 친절하게 오류 메시지를 반환합니다.
        return jsonify({'error':'Server is not configured. OPEN_API_KEY is missing.'}), 500

    data = request.get_json()
    user_message = data.get('message')
    # ... (나머지 로직은 그대로)
    
    try:
        completion = client.chat.completions.create(
            # ... (OpenAI 호출 로직)
        )
        return jsonify({"response": completion.choices[0].message.content}), 200
    except Exception as e:
        return jsonify({"error":str(e)}), 500

# if __name__ == "__main__": 블록은 이미 삭제하셨길 바랍니다.