using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UIElements;


namespace Game.Battle
{
    public class BattleRule : MonoBehaviour
    {
        //턴 정보
        public enum TurnState { PlayerTurn, EnemyTurn}
        public TurnState currentTurn;
        public int turn = 1;


        //UI 정보
        public UIDocument uiDocument;  // UIRoot 오브젝트에 붙은 UIDocument 할당
        VisualElement root, main, actList;
        Button attackBtn, swapBtn, startBtn, backAttackBtn;


        //플레이어 정보
        int playerSpeed = 0;
        public int playerSignal = 0;
        public List<CombtantEntity> player;
        public List<ActionDef> playerPlan;

        //적 정보
        int enemySpeed = 0;
        public int enemySignal = 0;
        public List<CombtantEntity> enemy;
        public List<ActionDef> enemyPlan;


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

            //전체 UI 조작
            //MainShow();

            //메인 이벤트
            attackBtn.clicked += ShowActList;
            startBtn.clicked += ExcuteTurn;

            //행동 선택 이벤트
            backAttackBtn.clicked += MainShow;


            //시작 세팅
            ReadyTurn();

            playerSpeed = Random.Range(player[0].Data.minSpeed, player[0].Data.maxSpeed);
            enemySpeed = Random.Range(enemy[0].Data.minSpeed, enemy[0].Data.maxSpeed);
            Debug.Log(playerSpeed);
            Debug.Log(enemySpeed);

        }

        //기본 턴 시스템
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
        void EndTurn()
        {
            Debug.Log($"적 사망 시각: {turn} 이다");
        }

        //전투 턴 시스템
        void ExcuteTurn()
        {
            //확률로 선공 후공 정하기
            RTurn();

            while (true)
            {
                Debug.Log(enemy[0].Data.parts[0].HP);
                //조건문으로 속도 확인
                if (currentTurn == TurnState.PlayerTurn)
                {
                    //플랜에 아무것도 없을 경우
                    if (playerPlan.Count == 0)
                    {
                        Debug.Log("값이 없으므로 상대에게 턴을 넘깁니다.");
                    }

                    //공격일 경우
                    else if (playerPlan[0].Tag==ActTag.Attack)
                    {
                        Debug.Log($"플레이어가 적 공격:{playerPlan[0].Damage}");
                        enemy[0].TakeDamage("Head", playerPlan[0].Damage);
                        playerPlan.RemoveAt(0);

                        Debug.Log(enemy[0].runtimeParts[0].HP);

                    }

                    //방어일 경우
                    else if (playerPlan[0].Tag == ActTag.Defense)
                    {
                        Debug.Log($"방어 버림");
                        playerPlan.RemoveAt(0);

                    }
                }
                else if(currentTurn == TurnState.EnemyTurn)
                {
                    if (enemyPlan.Count == 0)
                    {
                        Debug.Log("값이 없으므로 상대에게 턴을 넘깁니다.");
                    }
                    else if(playerPlan[0].Tag == ActTag.Attack)
                    {
                        player[0].TakeDamage("Head", enemyPlan[0].Damage);
                        enemyPlan.RemoveAt(0);

                    }
                    //방어일 경우
                    else if (playerPlan[0].Tag == ActTag.Defense)
                    {
                        Debug.Log($"방어 버림");
                        playerPlan.RemoveAt(0);

                    }
                }

                if (!player[0].IsAlive || !enemy[0].IsAlive)
                {
                    EndTurn();
                    break;
                }
                //enemy나 player의 플랜이 없을시 턴 종료
                else if (playerPlan.Count == 0 && enemyPlan.Count == 0)
                {
                    Debug.Log("다음턴으로 갑니다");
                    NextTurn();
                    break;
                }

            }
        }

        void ShowActList()
        {
            main.style.display = DisplayStyle.None;
            actList.style.display = DisplayStyle.Flex;

            //리스트 초기화
            actList.Clear();

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
            actList.Add(backAttackBtn);
        }

        void Enqueue(ActionDef act)
        {
            if (playerSignal + act.Signal > playerSpeed)
            {
                Debug.Log("신호를 넘었습니다. \n다른 기술로 넣든 아님 턴을 넘기세요.");
                return;
            }

            playerPlan.Add(act);
            playerSignal += act.Signal;

            Debug.Log($"{act.DisplayName}을 넣었습니다. \n현재 신호 : {playerSignal} 사용 가능 신호 : {playerSpeed - playerSignal}");

        }

        void EnemyAiEnqueue()
        {

        }

        void MainShow()
        {
            main.style.display = DisplayStyle.Flex;
            actList.style.display = DisplayStyle.None;
        }

        void RTurn()
        {
            int totalSpeed = playerSpeed + enemySpeed;

            int roll = Random.Range(0, totalSpeed);

            if (roll < playerSpeed) 
            {
                Debug.Log("player 먼저");
                currentTurn = TurnState.PlayerTurn;
            }
            else
            {
                Debug.Log("enemy 먼저");
                currentTurn = TurnState.EnemyTurn;
            }
        }
    } 
}