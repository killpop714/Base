// UnitAuthoring.cs
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Game.Battle;

// GameObject�� �ٿ� �� �Է� -> Baker�� ECS Entity ����
[DisallowMultipleComponent]
public class EntityBaker : MonoBehaviour
{
    [Header("������ ������")]
    [Tooltip("��� ��� �����͸� ��� ������")]
    public CombatantSO combatant;
    public ActSO[] act;


    public bool isPlayer = false; // üũ�ϸ� PlayerTag, �ƴϸ� EnemyTag

    // Baker ���� (Entities 1.0 ��Ÿ��)
    class Baker : Baker<EntityBaker>
    {
        public override void Bake(EntityBaker authoring)
        {
            // ��ȯ �� �����Ǵ� Entity �ڵ� ��������
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            // UnitData ������Ʈ �߰� (FixedString���� ��ȯ)
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

            //ActComponent �߰�
            buffer.Add(new ActComponent
            {
                displayName = "���� ��ġ",
                id = 0,

                tag = ActTag.None,

                signal = 0,
                deleteSignal = 0,

                minValue = 0,
                maxValue = 0,

                skillLevel = 0,
                chance = 0,


            });


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