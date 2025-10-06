using Game.Battle;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;


//부위 파괴시 패널티
public enum PartPenaltyType { None, CoreLoss, VitalLoss, SpeedLoss, HandLoss }

// 부위별 HP 컨테이너
[Serializable]
public class Parts
{
    public string DisplayName = "Part";
    [Tooltip("아이디는 특정 개체를 참조하기 위해서 쓰이니 전부 다르게 이름을 지어주세요")]
    public string Id = "Id";
    [Min(1)] public int MaxHP = 20;
    public int HP = 20;
    public List<Passives> passives =new();
    public bool Enabled = true; // 부위 사용/미사용 토글

    public void Clamp() => HP = Mathf.Clamp(HP, 0, MaxHP);
    public PartPenaltyType Penalty = PartPenaltyType.None;
    public bool IsBroken => HP <= 0;

}
