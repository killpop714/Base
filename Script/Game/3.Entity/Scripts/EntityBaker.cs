// UnitAuthoring.cs
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

// GameObject에 붙여 값 입력 -> Baker로 ECS Entity 생성
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
    public bool isPlayer = false; // 체크하면 PlayerTag, 아니면 EnemyTag

    // Baker 정의 (Entities 1.0 스타일)
    class Baker : Baker<EntityBaker>
    {
        public override void Bake(EntityBaker authoring)
        {
            // 변환 중 생성되는 Entity 핸들 가져오기
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            // UnitData 컴포넌트 추가 (FixedString으로 변환)
            UnitData unit = new UnitData
            {
                displayName = (FixedString64Bytes)authoring.displayName,
                hp = authoring.hp,
                damage = authoring.damage,
                minSpeed = authoring.minSpeed,
                maxSpeed = authoring.maxSpeed
            };
            AddComponent(entity, unit);

            // 태그 컴포넌트 추가
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