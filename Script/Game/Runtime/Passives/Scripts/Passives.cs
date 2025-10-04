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
        //전투 규칙 트리거
        OnStartTurn,
        OnNextReadyTurn,
        OnEndTurn,

        //기본 제압 생존 행동 트리거
        OnHit,
        OnSuppress,
        OnSurvival,

        //특수 행동 트리거
        OnFirePutOut

    }

    public abstract class Passives : ScriptableObject
    {
        //UI 전용 변수
        public string disPlayName { get; protected set; }

        public string description { get; protected set; }

        //패시브 추적 변수
        public int id { get; protected set; }

        public Triger AllBattleRuleTriger = Triger.OnStartTurn | Triger.OnNextReadyTurn | Triger.OnEndTurn | Triger.OnSuppress | Triger.OnHit;

        //패시브 적용 로직
        public abstract void Apply(Triger triger, Combtant self , Combtant target, Parts part);
        //패시브 작동 로직
        public abstract void Excute(Triger triger, Combtant self, Combtant target, Parts part);
        //패시브 제거 로직
        public abstract void Remove(Combtant self , Combtant target, Parts part);
        
    }
}