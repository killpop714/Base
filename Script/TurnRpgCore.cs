using System;
using System.Collections.Generic;
using UnityEngine;



namespace TurnRpg
{
    [System.Serializable] public class Stats { public int MaxHp = 100, Hp = 100, Attack = 10, Defense = 2; }
    [System.Serializable] public class BodyParts { public int Head = 50, Trunk = 80, Arm = 60, Leg = 60; }
    public enum Part { head, trunk, arm, leg }

    [Serializable]
    public class Entity
    {
        public string name = "Player";
        public Stats Stats = new();
        public BodyParts Parts = new();
        public List<string> Item = new(); 
    }
}

