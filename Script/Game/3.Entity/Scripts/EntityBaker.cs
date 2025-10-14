// UnitAuthoring.cs
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Game.Battle;

// GameObject에 붙여 값 입력 -> Baker로 ECS Entity 생성
[DisallowMultipleComponent]
public class EntityBaker : MonoBehaviour
{
    [Header("복사할 프리팹")]
    [Tooltip("대상에 모든 데이터를 담고 돌릴것")]
    public CombatantSO combatant;
    public ActSO[] act;


    public bool isPlayer = false; // 체크하면 PlayerTag, 아니면 EnemyTag

    // Baker 정의 (Entities 1.0 스타일)
    class Baker : Baker<EntityBaker>
    {
        public override void Bake(EntityBaker authoring)
        {
            // 변환 중 생성되는 Entity 핸들 가져오기
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            // UnitData 컴포넌트 추가 (FixedString으로 변환)
            CombatantComponent unit = new CombatantComponent
            {
                displayName = (FixedString64Bytes)authoring.combatant.DisplayName,

                minSpeed = authoring.combatant.minSpeed,
                maxSpeed = authoring.combatant.maxSpeed,

                Skin = PassiveOverride.None,

                speed = 1,
                signal = 0,

                isAlive = true
            };
            AddComponent(entity, unit);


            var buffer = AddBuffer<ActComponent>(entity);

            //ActComponent 추가
            buffer.Add(new ActComponent
            {
                displayName = "빠른 펀치",
                id = 0,

                tag = ActTag.None,

                signal = 0,
                deleteSignal = 0,

                minValue = 0,
                maxValue = 0,

                skillLevel = 0,
                chance = 0,


            });


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