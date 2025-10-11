using Unity.Entities;
using UnityEngine;

// 기존 Combatant 클래스
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
        // 테스트용 Entity 생성
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entity playerEntity = entityManager.CreateEntity();

        // Unit 컴포넌트 추가 (예시)
        entityManager.AddComponentData(playerEntity, new Unit { sid = 1, hp = 100 });

        // Combatant에 연결
        playerCombatant = new Combatant
        {
            entity = playerEntity,
            isPlayer = true,
            name = "Hero"
        };

        // Unit 데이터 가져오기
        ReadUnitData(playerCombatant);
    }

    void ReadUnitData(Combatant c)
    {
        if (!entityManager.Exists(c.entity))
        {
            Debug.LogWarning($"Entity {c.name} does not exist!");
            return;
        }

        // Entity로 Unit 데이터 읽기
        Unit unitData = entityManager.GetComponentData<Unit>(c.entity);

        Debug.Log($"Combatant {c.name} - SID: {unitData.sid}, HP: {unitData.hp}");

        unitData.hp = 50;

        entityManager.SetComponentData(c.entity, unitData);
    }
}

