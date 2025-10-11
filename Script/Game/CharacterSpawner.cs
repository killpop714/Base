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
            Debug.LogError("Mesh �Ǵ� Material�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Archetype ����
        var archetype = entityManager.CreateArchetype(
            typeof(LocalTransform),
            typeof(LocalToWorld),
            typeof(MoveSpeedComponent),
            typeof(RenderBounds)
        );

        // Entity ����
        var entity = entityManager.CreateEntity(archetype);

        // �ʱ� Transform ����
        entityManager.SetComponentData(entity, new LocalTransform
        {
            Position = transform.position,
            Rotation = quaternion.identity,
            Scale = 1f
        });

        // �̵� �ӵ� ����
        entityManager.SetComponentData(entity, new MoveSpeedComponent { Value = moveSpeed });

        var desc = new RenderMeshDescription(
            shadowCastingMode: ShadowCastingMode.Off,
            receiveShadows: false
            );

        var renderMeshArray = new RenderMeshArray(new Material[] { material }, new Mesh[] { mesh });



        // Hybrid Renderer ������Ʈ �߰�
        RenderMeshUtility.AddComponents(
            entity,
            entityManager,
            desc,
            renderMeshArray,
            MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));

    }
}