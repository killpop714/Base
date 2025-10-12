
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

//부위 파괴시 패널티
public enum PartType { None, Core, Vital, Leg, Hand }

public enum PassiveOverride { None}

// 팀 구분
public enum Team { Player, Enemy }
public enum Type { Entity, Object }

public struct UnitData : IComponentData
{
    public FixedString64Bytes displayName;

    Team team;
    Type type;

    public int minSpeed;
    public int maxSpeed;

    public int hp;
    public int damage;

    

    public PassiveOverride Skin;

    public int speed;
    public int signal;

    public bool isAlive;
}

public struct UnitPart : IComponentData
{
    public FixedString64Bytes displayName;

    public PartType type;

    public int maxHp;
    public int hp;


    public bool isBroken;


}

public struct PlayerTag : IComponentData
{

}

public struct EnemyTag : IComponentData
{

}



