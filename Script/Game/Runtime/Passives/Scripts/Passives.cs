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

        //Ư�� �ൿ Ʈ����
        OnFirePutOut

    }

    public abstract class Passives : ScriptableObject
    {
        //UI ���� ����
        public string disPlayName { get; protected set; }

        public string description { get; protected set; }

        //�нú� ���� ����
        public int id { get; protected set; }

        public Triger AllBattleRuleTriger = Triger.OnStartTurn | Triger.OnNextReadyTurn | Triger.OnEndTurn | Triger.OnSuppress | Triger.OnHit;

        //�нú� ���� ����
        public abstract void Apply(Triger triger, Combtant self , Combtant target, Parts part);
        //�нú� �۵� ����
        public abstract void Excute(Triger triger, Combtant self, Combtant target, Parts part);
        //�нú� ���� ����
        public abstract void Remove(Combtant self , Combtant target, Parts part);
        
    }
}