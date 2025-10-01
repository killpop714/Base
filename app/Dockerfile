FROM python:3.12-slim

WORKDIR /app

# COPY와 RUN 명령의 파일 이름을 동일하게 통일합니다.
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY app.py .

EXPOSE 8080

# gunicorn으로 실행 (Flask 개발 서버 대신)
CMD ["gunicorn", "-b", "0.0.0.0:8080", "app:app"]
