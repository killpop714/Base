using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public partial class MoveSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();

        
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (localTransform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeedComponent>>())
        {
            localTransform.ValueRW.Position += new float3(1, 0, 0) * speed.ValueRO.Value * deltaTime;
        }
    }
}
