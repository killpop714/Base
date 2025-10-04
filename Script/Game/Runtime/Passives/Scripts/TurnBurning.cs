using Game.Battle;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(menuName = "Battle/Passives/Burning", fileName = "Burning")]
public class TurnBurning : Passives
{
    public TurnBurning()
    {
        disPlayName = "버닝";
        description = $"불타는 동안 모든 행동 내에서 대미지 {damage}를 받으며 강도가 3 이상으로 3턴을 냅두면 전신에 버닝 패시브가 붙습니다.\n패시브를 제거하려면 무조건 아이템으로 제거해야 합니다.";
        id = 0;
    }

    //측정 값
    [SerializeField] int Value = 0;
    [SerializeField] int maxValue = 0;

    //대미지
    [SerializeField] int damage = 0;


    //대상 패시브 적용 규칙
    public override void Apply(Triger triger, Combtant self, Combtant target, Parts part)
    {
        //대상 전역 패시브 적용 규칙
        if (part == null)
        {
            //대상 전역 패시브에 해당 클래스가 있는지 탐색
            if (target.passives.Any(p => p.id == id))
            {
                target.passives.Add(this);
                return;
            }
        }
        else // 대상 파트 패시브 적용 규칙
        {
            //대상 파트 패시브에 해당 클래스가 있는지 탐색
            if (part.passives.Any(p => p.id == id))
            {
                target.passives.Add(this);
                return;
            }
        }

    }

    //패시브는 특정 대상이 중심으로 작동하는게 아닌 자신을 중심으로 작동 시키는 설계를 해야만 한다.
    //대상 전역 패시브 발동
    public override void Excute(Triger triger, Combtant self, Combtant target, Parts part)
    {
        //모든 배틀룰 트리거에 작동함
        if (target.passives.Any(p => p.id == id) && triger == AllBattleRuleTriger)
        {
            
            target.TakeDamage(part, damage);
            Debug.Log($"{target.Data.DisplayName}이 {part.DisplayName}에 {damage}만큼 불탄다.");
            Debug.Log($"{target.Data.DisplayName}의 현재 채력: {part.HP}");

            Remove(self, target, part);
            return;
        }

        self.TakeDamage(part, damage);
        Debug.Log($"{self.Data.DisplayName}이 {part.DisplayName}에 {damage}만큼 불탄다.");
        Debug.Log($"{self.Data.DisplayName}의 현재 채력: {part.HP}");
    }

    public override void Remove(Combtant self, Combtant target, Parts part)
    {
        throw new NotImplementedException();
    }


}
