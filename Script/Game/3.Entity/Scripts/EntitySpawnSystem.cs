//using Unity.Entities;
//using Unity.Burst;
//using Unity.Collections;

//[BurstCompile]
//public partial struct EntitySpawnerSystem : ISystem
//{
//    public void OnUpdate(ref SystemState state)
//    {
//        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

//        foreach (var (req, reqEntity) in SystemAPI.Query<RefRO<UnitData>>().WithEntityAccess())
//        {
//            // 새 엔티티 생성
//            Entity unit = ecb.CreateEntity();

//            ecb.AddComponent(unit, new UnitData
//            {
//                displayName = req.ValueRO.displayName,
//                hp = req.ValueRO.hp,
//                damage = req.ValueRO.damage,
//                minSpeed = 1,
//                maxSpeed = 3
//            });
//            ecb.DestroyEntity(reqEntity);
//        }

//        ecb.Playback(state.EntityManager);
//    }
//}