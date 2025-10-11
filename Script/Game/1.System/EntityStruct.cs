using Game.Battle;
using UnityEngine;
using Unity.Entities;

public struct Unit : IComponentData
{
    public int sid;
    public int hp;
    public int attack;
}



