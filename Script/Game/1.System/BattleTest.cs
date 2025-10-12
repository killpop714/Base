using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;



public class BattleTest : MonoBehaviour
{
    public EntityManager manager;

    public Entity Cplayer;
    public Entity[] Cenemy;

    

    void Start()
    {

        // 테스트용 Entity 생성
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var query = manager.CreateEntityQuery(typeof(UnitData), typeof(PlayerTag));
        var player = query.ToEntityArray(Allocator.Temp);
        Cplayer = manager.CreateEntity();
        Cplayer = player[0];
        player.Dispose();


        var enemyQuery = manager.CreateEntityQuery(typeof(UnitData), typeof(EnemyTag));
        var enemies = enemyQuery.ToEntityArray(Allocator.Temp);

        Cenemy = new Entity[enemies.Length];
        for(int i=0; i<enemies.Length;i++)
        {
            Cenemy[i] = manager.CreateEntity();
            Cenemy[i] = enemies[i];
        }
        
        enemies.Dispose();



       



    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {

            if (Cenemy.Length > 0)
            {
                var target = Cenemy[0];

                UtilityDamage.TakeDamage(manager, target, 20);


                var data = manager.GetComponentData<UnitData>(target);
                Debug.Log($"데미지! 현재 HP = {data.hp}");

                if(data.hp <= 0)
                {
                   
                }
            }
        }
    }


}




