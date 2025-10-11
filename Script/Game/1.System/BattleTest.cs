using Unity.Entities;
using UnityEngine;

// ���� Combatant Ŭ����
public class Combatant
{
    public Entity entity;
    public bool isPlayer;
    public string name;
}

public class BattleTest : MonoBehaviour
{
    public EntityManager entityManager;

    public Combatant playerCombatant;

    void Start()
    {
        // �׽�Ʈ�� Entity ����
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entity playerEntity = entityManager.CreateEntity();

        // Unit ������Ʈ �߰� (����)
        entityManager.AddComponentData(playerEntity, new Unit { sid = 1, hp = 100 });

        // Combatant�� ����
        playerCombatant = new Combatant
        {
            entity = playerEntity,
            isPlayer = true,
            name = "Hero"
        };

        // Unit ������ ��������
        ReadUnitData(playerCombatant);
    }

    void ReadUnitData(Combatant c)
    {
        if (!entityManager.Exists(c.entity))
        {
            Debug.LogWarning($"Entity {c.name} does not exist!");
            return;
        }

        // Entity�� Unit ������ �б�
        Unit unitData = entityManager.GetComponentData<Unit>(c.entity);

        Debug.Log($"Combatant {c.name} - SID: {unitData.sid}, HP: {unitData.hp}");

        unitData.hp = 50;

        entityManager.SetComponentData(c.entity, unitData);
    }
}

