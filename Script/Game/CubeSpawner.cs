using UnityEngine;
using Unity.Entities;

namespace ECS
{
    //public class CubeSpawner : MonoBehaviour
    //{
    //    public GameObject prefab;
    //    public float spawnRate;
    //}

    //class CubeSpawnerBaker : Baker<CubeSpawner>
    //{
    //    public override void Bake(CubeSpawner authoring)
    //    {
    //        Entity entity = GetEntity(TransformUsageFlags.None);

    //        AddComponent(entity, new Test
    //        {
    //            prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
    //            spawnpos = authoring.transform.position,
    //            nextSpawnTime = 0.0f,
    //            spawnRate = authoring.spawnRate,
    //        });
    //    }
    //}
}