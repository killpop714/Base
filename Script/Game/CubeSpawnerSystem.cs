using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS
{
    //[BurstCompile]
    //public partial struct CubeSpawnerSystem : ISystem
    //{
    //    [BurstCompile]
    //    public void OnUpdate(ref SystemState state)
    //    {
    //        if (!SystemAPI.TryGetSingletonEntity<Test>(out Entity spawnEntity))
    //        {
    //            return;
    //        }

    //        RefRW<Test> spawner = SystemAPI.GetComponentRW<Test>(spawnEntity);

    //        EntityCommandBuffer ecb = new(Allocator.Temp);

    //        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
    //        {
    //            Entity newEntity = ecb.Instantiate(spawner.ValueRO.prefab);

    //            spawner.ValueRW.nextSpawnTime =
    //                (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

    //            ecb.Playback(state.EntityManager);
    //            ecb.Dispose();
    //        }

    //    }
    //}
}

