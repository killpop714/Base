using Unity.Entities;

public struct HealthComponent : IComponentData
{
    public int Value;
}

public struct MoveSpeedComponent : IComponentData
{
    public float Value; 
}
