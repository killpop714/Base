using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using Unity.Burst;



[BurstCompile]
public partial struct TestSystem : ISystem
{
    NativeList<Entity> selected;

    public void OnCreate(ref SystemState state)
    {
        selected = new NativeList<Entity>(Allocator.Persistent);
    }

    public void OnDestroy(ref SystemState state)
    {
        if(selected.IsCreated) selected.Dispose();
    }

    public void OnUpdate(ref SystemState state)
    {
        if (selected.Length > 0) return;

        var manager = state.EntityManager;
        var query = manager.CreateEntityQuery(typeof(Unit));
        var entities = query.ToEntityArray(Allocator.Temp);

        if(entities.Length > 0)
        {
            Entity firstUnit = entities[0];
            selected.Add(firstUnit);
            Debug.Log($"TestSystem에서 선택된 Unit Entity: {selected[0].Index}-{selected[0].Version}");
        }

        entities.Dispose();
    }
}
