// UnitAuthoring.cs
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

// GameObject�� �ٿ� �� �Է� -> Baker�� ECS Entity ����
[DisallowMultipleComponent]
public class EntityBaker : MonoBehaviour
{
    [Header("Basic Stats")]
    public string displayName = "Unit";
    public int hp = 100;
    public int damage = 10;
    public int minSpeed = 1;
    public int maxSpeed = 3;

    [Header("Role")]
    public bool isPlayer = false; // üũ�ϸ� PlayerTag, �ƴϸ� EnemyTag

    // Baker ���� (Entities 1.0 ��Ÿ��)
    class Baker : Baker<EntityBaker>
    {
        public override void Bake(EntityBaker authoring)
        {
            // ��ȯ �� �����Ǵ� Entity �ڵ� ��������
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            // UnitData ������Ʈ �߰� (FixedString���� ��ȯ)
            UnitData unit = new UnitData
            {
                displayName = (FixedString64Bytes)authoring.displayName,
                hp = authoring.hp,
                damage = authoring.damage,
                minSpeed = authoring.minSpeed,
                maxSpeed = authoring.maxSpeed
            };
            AddComponent(entity, unit);

            // �±� ������Ʈ �߰�
            if (authoring.isPlayer)
            {
                AddComponent<PlayerTag>(entity);
            }
            else
            {
                AddComponent<EnemyTag>(entity);
            }
        }
    }
}