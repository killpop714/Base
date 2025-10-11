using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public partial struct TurnRuleSystem : ISystem
{
    NativeList<Entity> turnQueue;

    public void OnCreate(ref SystemState state)
    {
        turnQueue = new NativeList<Entity>(Allocator.Persistent);
        state.RequireForUpdate<Unit>();
    }

    public void OnDestroy(ref SystemState state)
    {
        if(turnQueue.IsCreated) turnQueue.Dispose();
    }

    public void OnUpdate(ref SystemState state)
    {
        var manager = state.EntityManager;

        if(turnQueue.Length == 0)
        {
            var query = manager.CreateEntityQuery(typeof(Unit), typeof(Turn));
            var target = query.ToEntityArray(Allocator.Temp);

            foreach(var e in target)
            {
                turnQueue.Add(e);   
            }

            target.Dispose();
        }

        if (turnQueue.Length > 0)
        {
            Entity currentTarget = turnQueue[0];
            turnQueue.RemoveAtSwapBack(0);

            var unitData = manager.GetComponentData<Unit>(currentTarget);
            Debug.Log($"Turn 시작 - Unit {unitData.sid} 행동");


        }
    }
}
