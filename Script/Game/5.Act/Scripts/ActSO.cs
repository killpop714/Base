using UnityEngine;

[CreateAssetMenu(fileName = "New Act", menuName = "Game/Act")]
public class ActSO : ScriptableObject
{

    [Header("Act 기본 정보")]
    public string displayName;
    public ActTag tag;

    [Header("전투 수치")]
    public int minValue = 10;
    public int maxValue = 20;

    [Header("기타 속성")]
    public int signal = 1;
    public int deleteSignal = 1;
    public int skillLevel = 1;
    public int chance = 100;

    //public Passives passives;

    //public Act CreateRuntimeCopy(string uuid)
    //{
    //    // 런타임용 Act 인스턴스 복제
    //    return new Act
    //    {
    //        displayName = displayName,
    //        uuid = uuid,
    //        tag = tag,
    //        signal = signal,
    //        deleteSignal = deleteSignal,
    //        minValue = minValue,
    //        maxValue = maxValue,
    //        skillLevel = skillLevel,
    //        chance = chance,
    //        passives = passives

    //    };
    //}
}