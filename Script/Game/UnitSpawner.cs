//using UnityEngine;
//using Unity.Entities;

//public class unitSpawner : MonoBehaviour
//{
//    [SerializeField] int sid = 0;

//    [SerializeField] int spawnCount = 0;

//    [SerializeField] int hp = 0;

//    [SerializeField] int attack = 0;

//    private void Start()
//    {
//        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;

//        for(int i = 0; i < sid; i++)
//        {
//            Entity e = manager.CreateEntity(typeof(Unit));
//            manager.SetComponentData(e, new Unit
//            {
//                sid = sid,
//                hp = hp,
//                attack = attack

//            });
//            Debug.Log($"생성 개체 수:{sid}, 채력:{hp}, 대미지:{attack}");

//        }
//    }
//}
