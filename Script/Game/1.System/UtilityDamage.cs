using UnityEngine;
using Unity.Entities;

public class UtilityDamage
{
    public static void TakeDamage(EntityManager manager, Entity target, int amount)
    {
        if (!manager.HasComponent<CombatantComponent>(target)) return;

        var targetData = manager.GetComponentData<CombatantComponent>(target);
        targetData.hp -= amount;
        if(targetData.hp < 0) targetData.hp = 0;
        manager.SetComponentData(target, targetData);
    }
}
