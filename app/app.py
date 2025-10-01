from flask import Flask, request, jsonify
from openai import OpenAI
import os

OPEN_API_KEY = os.getenv("OPEN_API_KEY", "NULL_OR_NONE")

app = Flask(__name__)

if OPEN_API_KEY and OPEN_API_KEY != "NULL_OR_NONE":
    client = OpenAI(api_key=OPEN_API_KEY)
else:
    client = None

@app.route('/chatMessage', methods=['POST'])
def chatMessage():
    if client is None:
        return jsonify({'error': 'Server is not configured. OPEN_API_KEY is missing.'}), 500

    data = request.get_json()
    user_message = data.get('message')

    try:
        completion = client.chat.completions.create(
            model="gpt-4o-mini",
            messages=[{"role": "user", "content": user_message}]
        )
        return jsonify({"response": completion.choices[0].message.content}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500


# ✅ 로컬 개발 전용
if __name__ == "__main__":
    port = int(os.environ.get("PORT", 8080))
    app.run(host="0.0.0.0", port=port, debug=True)