using UnityEngine;


public enum ActTag { None, Attack, Defense,Use ,Swap}

public interface IAction
{
    string DisplayName { get; }
    ActTag Tag { get; }
    int Signal { get; }
    int MinValue { get; }
    int MaxValue { get; }
}

[CreateAssetMenu(menuName = "Battle/Act", fileName = "NewAct")]
public class ActionDef : ScriptableObject, IAction
{
    [SerializeField] string displayName = "Act";
    [SerializeField] ActTag tag = ActTag.None;  // 인스펙터에서 체크박스로 선택
    [SerializeField] int signal = 1;
    [SerializeField] int minValue = 10;
    [SerializeField] int maxValue = 10;

   

    public string DisplayName => displayName;
    public ActTag Tag => tag;
    public int Signal => signal;
    public int MinValue => minValue;
    public int MaxValue => maxValue;
    
    public int RGetDamage()
    {
        return Random.Range(minValue, maxValue);
    }

}