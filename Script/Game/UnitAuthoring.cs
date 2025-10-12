//using UnityEngine;
//using Unity.Entities;

//public class UnitAuthoring : MonoBehaviour
//{
//    [SerializeField] private int count = 1;

//    void Start()
//    {
//        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

//        for (int i = 0; i < count; i++)
//        {
//            Entity e = entityManager.CreateEntity(typeof(Unit));
//            entityManager.SetComponentData(e, new Unit { sid = i });
//            Debug.Log($"UnitSpawner에서 생성된 Entity: {e.Index}-{e.Version}");
//        }
//    }
//}
