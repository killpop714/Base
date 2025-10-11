using UnityEngine;
using Unity.Entities;
using Game.Battle;

namespace Game.Entity
{
    public class EntityBaker : MonoBehaviour
    {
        CombatantSO combatantSO;
        Team team;
        Type type;

        class Baker : Baker<EntityBaker>
        {
            public override void Bake(EntityBaker authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                
            }
        }
    }
}

