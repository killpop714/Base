

using Game.Battle;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ActSystem : MonoBehaviour
{
    [SerializeField] List<ActSO> Data;
    public List<Act> runtimeActs = new List<Act>();

    //초기 대이터 초기화 할때만 사용
    public void SetAct()
    {
        runtimeActs.Clear();

        // Data 리스트의 크기만큼 반복하며 런타임 Act 객체를 생성합니다.
        for (int i = 0; i < Data.Count; i++)
        {
            // 1. DataSO의 정보를 가져옵니다.
            ActSO dataTemplate = Data[i];

            // 2. 새로운 Act 객체를 생성하여 데이터를 복사합니다.
            Act newAct = new Act();

            // 3. 안전한 UUID를 확보하고 객체에 할당하는 루프 시작
            string safeUuid;

            // 중복이 발견되지 않을 때까지 반복합니다. (GUID 특성상 99.999...% 확률로 한 번에 통과)
            while (true)
            {
                // 새로운 UUID 생성
                safeUuid = Guid.NewGuid().ToString();

                // 중복 검사: runtimeActs 리스트에 이미 같은 UUID를 가진 요소가 있는지 확인
                // 이 검사가 바로 '만약의 버그'를 대비하는 보험입니다.
                if (runtimeActs.Any(p => p.uuid == safeUuid))
                {
                    // 중복 발생 (천문학적 버그): 경고 로그를 띄우고 루프를 다시 시작 (재생성)
                    Debug.LogWarning($"UUID 충돌 감지! 재시도: {safeUuid}");
                }
                else
                {
                    // 중복이 없음: 루프를 탈출합니다.
                    break;
                }
            }

            // 4. 안전한 UUID를 복사본에 할당합니다.
            // (CreateRuntimeCopy가 UUID를 받아 Act 객체의 필드에 할당하는 코드를 가정)
            Act act = dataTemplate.CreateRuntimeCopy(safeUuid);

            // 5. 런타임 리스트에 추가합니다.
            runtimeActs.Add(act);
        }
    }
}
