using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// 서버 응답 파싱용 클래스
[System.Serializable]
public class GPTResponse
{
    public string conversation;
    public string text;
}

public class GptTest : MonoBehaviour
{
    private string serverUrl = "http://localhost:3000/talk"; 
    private string conversationId = null; // 대화 이어가기용 conv_xxx 저장

    void Start()
    {
        // 첫 대화 테스트
        StartCoroutine(SendMessageToServer("안녕, 너 누구야?"));
    }

    public IEnumerator SendMessageToServer(string userInput)
    {
        // 요청 JSON 생성
        var requestBody = JsonUtility.ToJson(new RequestData
        {
            userInput = userInput,
            conversation = conversationId
        });

        using (UnityWebRequest req = new UnityWebRequest(serverUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBody);
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("❌ Error: " + req.error + "\n" + req.downloadHandler.text);
            }
            else
            {
                Debug.Log("✅ Raw Response: " + req.downloadHandler.text);

                // JSON 파싱
                GPTResponse res = JsonUtility.FromJson<GPTResponse>(req.downloadHandler.text);

                // 다음 대화를 위해 conversationId 갱신
                conversationId = res.conversation;

                // 모델 답변 출력
                Debug.Log("🤖 GPT: " + res.text);
            }
        }
    }

    // 요청 데이터 구조
    [System.Serializable]
    public class RequestData
    {
        public string userInput;
        public string conversation;
    }
}