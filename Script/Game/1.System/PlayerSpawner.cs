using UnityEngine;
using Unity.Entities;

namespace Game.Entity
{
    public class EntityBaker : MonoBehaviour
    {
        
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

