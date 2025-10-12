//using Game.Battle;
//using System.Linq;
//using System;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEngine.EventSystems.EventTrigger;

//[CreateAssetMenu(menuName = "Battle/Passives/Burning", fileName = "Burning")]
//public class TurnBurning : Passives
//{
//    public TurnBurning()
//    {
//        disPlayName = "버닝";
//        description = $"불타는 동안 모든 행동 내에서 대미지 {damage}를 받으며 강도가 3 이상으로 3턴을 냅두면 전신에 버닝 패시브가 붙습니다.\n패시브를 제거하려면 무조건 아이템으로 제거해야 합니다.";
//        type = PassiveType.beginning;
//        id = 0;
//    }

//    //측정 값
//    private int count = 0;
//    private int maxCount = 3;

//    //대미지
//    private int damage = 1;


//    //대상 패시브 적용 규칙
//    public override void Apply(Triger triger, PassiveOverride passiveOverrider, Combtant self, Combtant target, Parts part)
//    {
//        //대상 전역 패시브 적용 규칙
//        //if (part == null)
//        //{
//        //    //대상 전역 패시브에 해당 클래스가 있는지 탐색
//        //    if (target.passives.Any(p => p.id == id))
//        //    {
//        //        target.passives.Add(this);
//        //        return;
//        //    }
//        //}
//        //대상 파트 패시브에 해당 클래스가 있는지 탐색
//        if(triger == Triger.OnSuppress)
//        {
//            if (!part.passives.Any(p => p.id == id))
//            {
//                TurnBurning ins = ScriptableObject.CreateInstance<TurnBurning>();

//                part.passives.Add(ins);
//                count = Math.Min(count + 1, maxCount);

//                return;
//            }
//            count = Math.Min(count + 1, maxCount);
//        }
//    }

//    //패시브는 특정 대상이 중심으로 작동하는게 아닌 자신을 중심으로 작동 시키는 설계를 해야만 한다.
//    //제거는 무조건 Execute에 작동 시키고 return 시켜라
//    //대상 전역 패시브 발동
//    public override void Execute(Triger triger, PassiveOverride passiveOverrider, Combtant self, Combtant target, Parts part)
//    {
//        //모든 배틀룰 트리거에 작동함

//        if(passiveOverrider == PassiveOverride.OnFirePutOut || part.IsBroken)
//        {
//            Remove(self, target, part);
//            return;
//        }

//        Debug.Log("버닝 작동중");
//        if (type == PassiveType.beginning)
//        {
//            Debug.Log("beginning 활성화");
//            type = PassiveType.progress;
//            return;
//        }

//        self.TakeDamage(part, damage);
//        Debug.Log($"{self.Data.DisplayName}이 {part.DisplayName}에 {damage}만큼 불탄다.");
//        Debug.Log($"{self.Data.DisplayName}의 현재 채력: {part.HP}");
//    }

//    public override void Remove(Combtant self, Combtant target, Parts part)
//    {
//        Debug.Log("버닝 패시브 제거");
//        part.passives.Remove(this);
//    }
//}


