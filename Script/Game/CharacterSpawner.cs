using UnityEngine;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.Rendering;
using static UnityEditor.MaterialProperty;

public class CharacterSpawner : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public float moveSpeed = 5f;

    void Start()
    {
        if (mesh == null || material == null)
        {
            Debug.LogError("Mesh 또는 Material이 할당되지 않았습니다!");
            return;
        }

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Archetype 생성
        var archetype = entityManager.CreateArchetype(
            typeof(LocalTransform),
            typeof(LocalToWorld),
            typeof(MoveSpeedComponent),
            typeof(RenderBounds)
        );

        // Entity 생성
        var entity = entityManager.CreateEntity(archetype);

        // 초기 Transform 설정
        entityManager.SetComponentData(entity, new LocalTransform
        {
            Position = transform.position,
            Rotation = quaternion.identity,
            Scale = 1f
        });

        // 이동 속도 설정
        entityManager.SetComponentData(entity, new MoveSpeedComponent { Value = moveSpeed });

        var desc = new RenderMeshDescription(
            shadowCastingMode: ShadowCastingMode.Off,
            receiveShadows: false
            );

        var renderMeshArray = new RenderMeshArray(new Material[] { material }, new Mesh[] { mesh });



        // Hybrid Renderer 컴포넌트 추가
        RenderMeshUtility.AddComponents(
            entity,
            entityManager,
            desc,
            renderMeshArray,
            MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));

    }
}