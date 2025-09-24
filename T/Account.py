import requests

base_url = "http://172.30.1.87:8000/ChatGptM"
playload = {"userInput":"안녕!"}

response = requests.post(base_url,json=playload)
print("Status Code:", response.status_code)
print("Raw Text:", response.text)   # 응답이 실제로 뭐가 오는지 확인