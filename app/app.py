from flask import Flask, request, jsonify,Response
import requests
import json
import pymysql
from datetime import datetime 


app = Flask(__name__)
app.config['JSON_AS_ASCII']=False

sql = pymysql.connect(
        host='localhost',
        user='root',
        password='MySQLDatabase2025*',
        database='testdb',
        cursorclass=pymysql.cursors.DictCursor
    )

API_KEY = "CWXE488NZ8TV9260M14A"
BASE_URL = "https://ecos.bok.or.kr/api/StatisticSearch"



@app.route('/FetchCPI',methods=['GET'])
def fetchCpi():

    start = request.args.get("start","202301")
    end = request.args.get("end","202312")
    code = "901Y014"
    cycle = "M"

    url = f"{BASE_URL}/{API_KEY}/json/kr/1/100/{code}/{cycle}/{start}/{end}"
    res = requests.get(url)
    data = res.json()

    if "StatisticSearch" not in data:
        return jsonify({"error": "API 호출 실패", "detail":data})
    
    rows = data["StatisticSearch"]["row"]

    with sql.cursor() as cursor:
        for r in rows:
            time = r["TIME"]
            value = r["DATA_VALUE"]
            item = r["ITEM_NAME1"]

            cursor.execute(
                "INSERT INTO prices (item, year, month, price) VALUES (%s, %s, %s, %s) "
                "ON DUPLICATE KEY UPDATE price=%s",
                (item, int(time[:4]), int(time[4:]), float(value), float(value))
            )
            sql.commit()

            return Response(
                json.dumps(rows, ensure_ascii=False),
                content_type="application/json; charset=utf-8"
            )


@app.route('/FormatData',methods=['POST'])
def FormatData():

    with sql.cursor() as cursor:
        cursor.execute()

    return "성공했다고!"

@app.route('/SearchData',methods=['GET'])
def SearchData():


    return "성공"

@app.route('/RegisterRecipe')
def RegisterRecipe():
    with sql.cursor() as cursor:

        name = "김치찌개"
        created = datetime.now().strftime("%Y-%m-%d %H:%M:%S")  # 문자열 변환
        updated = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        title = "얼큰한 김치찌개 만들기"
        creator = "ChefYoon"
        portion = 2
        difficulty = 1

        query = "INSERT INTO recipe (name,created, updated, title, creator, portion,difficulty) VALUES(%s,%s,%s,%s,%s,%s,%s)"
        cursor.execute(query,(name, created, updated, title, creator, portion,difficulty))
        sql.commit()

        sql.close()

        return "등록 완료"

if __name__ == "__main__":
    app.run(debug=False, host="0.0.0.0", port= 8080)