using Game.Battle;
using UnityEngine;


public enum ActTag { None, Suppress, Survival, Utility}



[CreateAssetMenu(menuName = "Battle/Act", fileName = "NewAct")]
public class ActSO : ScriptableObject
{
    [SerializeField] string displayName = "Act";
    [SerializeField] int id;

    //제압, 생존, 기타
    [SerializeField] ActTag tag = ActTag.None;  

    //Act의 신호와 상대 신호를 제거하는 값
    [SerializeField] int signal = 1;
    [SerializeField] int deleteSignal = 1;

    //Act 공격값이나 또는 방어값
    [SerializeField] int minValue = 10;
    [SerializeField] int maxValue = 10;

    
    [SerializeField] int skillLevel = 1;
    [SerializeField] int chance = 1;

    //패시브 전용 변수
    [SerializeField] PassiveOverride passiveOverride =  PassiveOverride.None;
    [SerializeField] Passives passives; //Act의 패시브

   

    public string DisplayName => displayName;
    public int Id => id;

    public ActTag Tag => tag;

    public int Signal => signal;
    public int DeleteSignal => deleteSignal;

    public int MinValue => minValue;
    public int MaxValue => maxValue;


    public int SkillLevel => skillLevel;
    public int Chance => chance;

    public PassiveOverride PassiveOverride => passiveOverride;
    public Passives Passives => passives;
}