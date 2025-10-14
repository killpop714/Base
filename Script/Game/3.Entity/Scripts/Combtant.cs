using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Entities;


public class Combatant
{
    //현재 캐릭터의 정보
    [Tooltip("이 캐릭터의 데이터")]
    public Entity Data;

    ////무기 전역 관리 시스템
    //public WeaponSystem weaponSystem;

    ////지금 들고 있는 무기
    //public Weapon mainWeapon;

    //해당 객체의 동적 파트
    public List<CombatantPart> parts;


    //내부 전투 규칙용 변수
    public List<ActComponent> plans;

    public PassiveOverride Skin;
    public int speed;
    public int signal;
}

    










//    private void Awake()
//    {
//        //엔티티의 기본 정보들을 runtime에 굴러가게 생성

//        //엔티티의 파트들의 정보들을 runtime에 굴러가게 생성
//        runtimeParts = Data.parts.Select(p => new Parts
//        {
//            DisplayName = p.DisplayName,
//            Id = p.Id,
//            MaxHP = p.MaxHP,
//            HP = p.HP,
//            Enabled = p.Enabled,
//            Penalty = p.Penalty
//        }).ToArray();

//        actSystem.SetAct();
//        weaponSystem.SetWeapon();

//        for (int i = 0; i < actSystem.runtimeActs.Count; i++)
//        {
//            int index = mainWeapon.data.actList.FindIndex(p => p.displayName == actSystem.runtimeActs[i].displayName);
//            Debug.Log(index);
//            mainWeapon.actList.Add(actSystem.runtimeActs[index]);
//        }
//    }

//    public bool IsAlive =>
//    !(runtimeParts?.Any(p => p.IsBroken &&
//    (p.Penalty == PartPenaltyType.CoreLoss ||
//     p.Penalty == PartPenaltyType.VitalLoss)) ?? false);
//    public void TakeDamage(Parts part, int amount)
//    {
//        Parts core = runtimeParts.FirstOrDefault(p => p.Penalty == PartPenaltyType.CoreLoss);
//        if (part == null) return;



//        if (part.Id == core.Id)
//        {
//            core.HP = Mathf.Clamp(core.HP - amount, 0, core.MaxHP);

//        }
//        else
//        {
//            part.HP = Mathf.Clamp(part.HP - amount, 0, part.MaxHP);
//            core.HP = Mathf.Clamp(core.HP - amount, 0, core.MaxHP);
//        }

//        Debug.Log(part.HP);



//        IsBrokenPart(part);
//    }

//    //속도가 최소치와 최대치를 랜덤하게 구하는 함수
//    public void RSetSpeed()
//    {
//        speed = Random.Range(Data.minSpeed, Data.maxSpeed);
//    }

//    //파트가 부서질때 주어지는 패널티
//    public void IsBrokenPart(Parts part)
//    {
//        if (part.HP == 0)
//        {

//        }
//    }

//}


