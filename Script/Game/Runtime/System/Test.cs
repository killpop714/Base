using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public string Name;
    public int Speed;
    public float ActionGauge;
    public bool IsPlayer;

    public Unit(string name, int speed, bool isPlayer)
    {
        Name = name;
        Speed = speed;
        ActionGauge = 0;
        IsPlayer = isPlayer;
    }

    public void Act()
    {
        Debug.Log($"{Name}의 턴입니다!");
        // 여기에 실제 행동 (공격, 스킬 등) 코드를 넣어요
    }
}

public class TurnManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    private void Start()
    {
        // 테스트 유닛 추가
        units.Add(new Unit("Player", 15, true));
        units.Add(new Unit("Goblin", 10, false));
        units.Add(new Unit("Slime", 5, false));
    }

    private void Update()
    {
        foreach (var unit in units)
        {
            unit.ActionGauge += unit.Speed * Time.deltaTime;

            if (unit.ActionGauge >= 100f)
            {
                unit.Act();
                unit.ActionGauge = 0f;
            }
        }
    }
}