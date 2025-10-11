using UnityEngine;
using Unity.Entities;

public struct Turn : IComponentData
{
    public bool isMyTurn;
    public int initiative;
}
