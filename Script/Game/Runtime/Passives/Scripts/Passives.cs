using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;


namespace Game.Battle 
{ 
    //���� ��Ģ ���� ���� Ʈ����
    public enum Triger
    {
        None,
        //���� ��Ģ Ʈ����
        OnStartTurn,
        OnNextReadyTurn,
        OnEndTurn,

        //�⺻ ���� ���� �ൿ Ʈ����
        OnHit,
        OnSuppress,
        OnSurvival,

        //Ư�� ��Ÿ �ൿ Ʈ����
         OnItemUse,
         OnInteraction
    }

    public enum PassiveType
    {
        immediately,

        beginning,

        progress

    }

    //Act ���� Ư�� �ൿ Ʈ����
    public enum PassiveOverride
    {
        None,
        OnFirePutOut // �Ҳ��� �̺�Ʈ
    }

    public abstract class Passives : ScriptableObject
    {
        //UI ���� ����
        public string disPlayName { get; protected set; }

        public string description { get; protected set; }

        public PassiveType type { get; protected set; }

        //�нú� ���� ����
        public int id { get; protected set; }

        public Triger AllBattleRuleTriger = Triger.OnStartTurn | Triger.OnNextReadyTurn | Triger.OnEndTurn | Triger.OnSuppress | Triger.OnHit;

        //�нú� ���� ����
        public abstract void Apply(Triger triger, PassiveOverride passiveOverrider, Combtant self , Combtant target, Parts part);
        //�нú� �۵� ����
        public abstract void Execute(Triger triger, PassiveOverride passiveOverrider, Combtant self, Combtant target, Parts part);
        //�нú� ���� ����
        public abstract void Remove(Combtant self , Combtant target, Parts part);

        
    }
}