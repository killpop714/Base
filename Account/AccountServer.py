from flask import Flask, request, jsonify
from openai import OpenAI

OPEN_API_KEY = "NULL"
if not OPEN_API_KEY:
    print("오류 OPEN_API_KET가 없습니다.")
    exit(1)

app = Flask(__name__)

client = OpenAI(api_key=OPEN_API_KEY)

@app.route('/chatMessage',methods=['POST'])
def chatMessage():
    data = request.get_json()
    user_message = data.get('message')

    if not user_message:
        return jsonify({'error':'메시지가 없습니다'}),400
    
    try:
        completion = client.chat.completions.create(
            model='gpt-4o-2024-11-20',
            messages=[
                {"role":"user", "content": user_message}
            ]
        )
    except Exception as e:
        return jsonify({"error":str(e)}),500


if __name__ == "__main__":
    app.run(debug=True, host='0.0.0.0')