using Game.Battle;
using UnityEngine;


public enum ActTag { None, Attack, Defense,Use ,Swap}

public interface IAction
{
    string DisplayName { get; }
    ActTag Tag { get; }
    int Signal { get; }
    int MinValue { get; }
    int MaxValue { get; }
    Passives Passives { get; }
}

[SerializeField]
[CreateAssetMenu(menuName = "Battle/Act", fileName = "NewAct")]
public class ActionDef : ScriptableObject, IAction
{
    string displayName = "Act";
    ActTag tag = ActTag.None;  // 인스펙터에서 체크박스로 선택
    int signal = 1;
    int minValue = 10;
    int maxValue = 10;
    Passives passives = new None();

   

    public string DisplayName => displayName;
    public ActTag Tag => tag;
    public int Signal => signal;
    public int MinValue => minValue;
    public int MaxValue => maxValue;
    public Passives Passives => passives;

    public int RGetDamage()
    {
        return Random.Range(minValue, maxValue);
    }



}