using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Battle
{
    public class BattleRule : MonoBehaviour
    {

        

        public UIDocument uiDocument;  // UIRoot 오브젝트에 붙은 UIDocument 할당


        VisualElement root, main, actList;
        Button attackBtn, swapBtn, startBtn, backAttackBtn;
        int turn = 1;

        public List<CombtantEntity> player;
        public int playerSignal =0;

        int playerSpeed = 0;

        public List<CombtantEntity> enemy;
        public int enemySignal = 0;



        void Start()
        {
            root = uiDocument.rootVisualElement;
            //메인 조합
            main = root.Q<VisualElement>("main");

            attackBtn = root.Q<Button>("attackBtn");
            swapBtn = root.Q<Button>("swapBtn");
            startBtn = root.Q<Button>("startBtn");

            //행동 선택 조합
            actList = root.Q<VisualElement>("actList");

            backAttackBtn = root.Q<Button>("backAttackBtn");

            MainShow();

            //메인 이벤트
            attackBtn.clicked += ShowActList;
            startBtn.clicked += ExcuteTurn;

            //행동 선택 이벤트
            backAttackBtn.clicked += MainShow;
            ReadyTurn();

            playerSpeed = UnityEngine.Random.Range(player[0].Data.minSpeed, player[0].Data.maxSpeed);
        }


        void ReadyTurn()
        {
            playerSignal = 0;
            

            Debug.Log($"현재 턴 : {turn}");
            Debug.Log($"[플레이어 턴 시작] AP: {playerSignal}");
        }
        void NextTurn()
        {
            ++turn;
            ReadyTurn();
        }

        void ExcuteTurn()
        {
            
            // if (_plan.Count == 0)
            // {
            //     Debug.Log("담은 행동 없음");
            //     NextTurn();
            //     return;
            // }

            // var copy = new List<Act>(_plan);

            // foreach (var id in copy)
            // {
            //     var d = _defs[id];

            //     if (d.IsAttack)
            //     {
            //         int dmg = Random.Range(d.MinDmg, d.MaxDmg + 1);
            //         enemyHP = Mathf.Max(0, enemyHP - dmg);
            //         Debug.Log($"✅ {d.Name}: {dmg} 피해 → 적 HP {enemyHP}");
            //         if (enemyHP <= 0)
            //         {
            //             Debug.Log("🎉 적 격파! 전투 종료");
            //             _plan.Clear();
            //             return;
            //         }
            //     }
            //     else
            //     {
            //         Debug.Log($"🔁 {d.Name} 처리(데모: 효과 없음)");
            //     }
            // }
            // _plan.Clear();
            // NextTurn();
        }

        void ShowActList()
        {
            Debug.Log("어택 눌림");
            main.style.display = DisplayStyle.None;
            actList.style.display = DisplayStyle.Flex;

            var MainWeapon = player[0].MainWeapon;
            foreach (var act in MainWeapon.ActList)
            {
                if (act == null) continue;

                var b = new Button(()=>Enqueue(act))
                {
                    text = $"{act.name}(Signal: {act.Signal})"
                };
                actList.Add(b);
            }
        }

        void Enqueue(ActionDef act)
        {
            if (playerSignal + act.Signal > playerSpeed)
            {
                Debug.Log("신호를 넘었습니다. \n다른 기술로 넣든 아님 턴을 넘기세요.");
                return;
            }
            _plan.Add(id);
            playerSignal += act.Signal;

            Debug.Log($"{act.DisplayName}을 넣었습니다. \n현재 신호 : {playerSignal} 사용 가능 신호 : {playerSpeed - playerSignal}");

        }

        void MainShow()
        {
            main.style.display = DisplayStyle.Flex;
            actList.style.display = DisplayStyle.None;
        }
    } 
}