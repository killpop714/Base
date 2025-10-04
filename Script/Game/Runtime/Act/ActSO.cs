using Game.Battle;
using UnityEngine;


public enum ActTag { None, Suppress, Survival, Utility}

[CreateAssetMenu(menuName = "Battle/Act", fileName = "NewAct")]
public class ActSO : ScriptableObject
{
    [SerializeField] string displayName = "Act";

    //제압, 생존, 기타
    [SerializeField] ActTag tag = ActTag.None;  

    //Act의 신호와 상대 신호를 제거하는 값
    [SerializeField] int signal = 1;
    [SerializeField] int deleteSignal = 1;

    //Act 대미지나 또는 방어의 값들
    [SerializeField] int minValue = 10;
    [SerializeField] int maxValue = 10;

    //패시브 전용 변수
    [SerializeField] Triger triger =  Triger.None;
    [SerializeField] Passives passives; //Act의 패시브

   

    public string DisplayName => displayName;

    public ActTag Tag => tag;

    public int Signal => signal;
    public int DeleteSignal => deleteSignal;

    public int MinValue => minValue;
    public int MaxValue => maxValue;
    public Passives Passives => passives;

    public int RGetDamage()
    {
        return Random.Range(minValue, maxValue);
    }



}