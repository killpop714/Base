//using Game.Battle;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using UnityEngine;


//// ������ HP �����̳�
//[Serializable]
//public class Parts
//{
//    public string DisplayName = "Part";
//    [Tooltip("���̵�� Ư�� ��ü�� �����ϱ� ���ؼ� ���̴� ���� �ٸ��� �̸��� �����ּ���")]
//    public string Id = "Id";
//    [Min(1)] public int MaxHP = 20;
//    public int HP = 20;
//    public List<Passives> passives =new();
//    public bool Enabled = true; // ���� ���/�̻�� ���

//    public void Clamp() => HP = Mathf.Clamp(HP, 0, MaxHP);
//    public PartPenaltyType Penalty = PartPenaltyType.None;
//    public bool IsBroken => HP <= 0;

//}
