using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Battle
{
    public class TurnRule : MonoBehaviour
    {

        // class ActionDef
        // {
        //      public string Name;
        //     public int Cost;         // 신호(AP) 코스트
        //     public int MinDmg, MaxDmg;
        //     public bool IsAttack;

        //     public ActionDef(string name, int cost, int minDmg, int maxDmg, bool isAttack = true)
        //     { Name = name; Cost = cost; MinDmg = minDmg; MaxDmg = maxDmg; IsAttack = isAttack; }
        // }
        

        public UIDocument uiDocument;  // UIRoot 오브젝트에 붙은 UIDocument 할당

        VisualElement root, main, actList;
        Button attackBtn, swapBtn, startBtn, backBtn;

        int turn = 1;


        int playerHP = 40;
        int playerSignal = 0;
        int playerSpead = 0;
        public WeaponSO playerMainHand;
        readonly public List<WeaponSO> playerPlan;
        int enemyHP = 40;

        int enemySignal = 0;
        int enemySpeed = 0;
        public WeaponSO enemyMainHand;
        readonly public List<WeaponSO> enemyPlan;


        VisualElement _root, _main, _skillPanel, _skillList;
        Button _attackBtn, _swapBtn, _startBtn, _backBtn;


        void Start()
        {
            root = uiDocument.rootVisualElement;
            main = root.Q<VisualElement>("main");
            actList = root.Q<VisualElement>("actList");

            attackBtn = root.Q<Button>("attackBtn");
            swapBtn = root.Q<Button>("swapBtn");
            startBtn = root.Q<Button>("startBtn");
            backBtn = root.Q<Button>("backBtn");

            ShowMain();


            attackBtn.clicked += ShowActList;
            startBtn.clicked += ExcuteTurn;
            ReadyTurn();

            
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
            if (playerSpead < enemySpeed)
            {
                
            }
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

        void Enqueue()
        {
            // var d = _defs[id];
            // if (playerSignal + d.Cost > playerSpead)
            // {
            //     Debug.Log("신호를 넘었습니다. \n다른 기술로 넣든 아님 턴을 넘기세요.");
            //     return;
            // }
            // _plan.Add(id);
            // playerSignal += d.Cost;

            // Debug.Log($"{d.Name}을 넣었습니다. \n현재 신호 : {playerSignal} 사용 가능 신호 : {playerSpead - playerSignal}");

        }
        void ShowMain()
        {
            main.style.display = DisplayStyle.Flex;
            actList.style.display = DisplayStyle.None;
        }
        void ShowActList()
        {
            main.style.display = DisplayStyle.None;
            actList.style.display = DisplayStyle.Flex;
        }
    } 
}