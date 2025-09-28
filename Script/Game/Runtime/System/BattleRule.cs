using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UIElements;


namespace Game.Battle
{
    public class BattleRule : MonoBehaviour
    {
        //참조 정보
        public Combatant Combatant;

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

            playerSpeed = player[0].GetSpeed();
            enemySpeed = enemy[0].GetSpeed();
            Debug.Log(playerSpeed);
            Debug.Log(enemySpeed);

        }

        //기본 턴 시스템
        void ReadyTurn()
        {
            playerSignal = 0;
            EnemyAiEnqueue();

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
            //Debug.Log(player[0].Data.Team);
            while (true)
            {
                //조건문으로 속도 확인
                if (currentTurn == TurnState.PlayerTurn)
                {
                    //플랜에 아무것도 없을 경우
                    if (playerPlan.Count == 0)
                    {
                        Debug.Log("값이 없으므로 상대에게 선공을 넘깁니다.");
                        currentTurn = TurnState.EnemyTurn;
                    }
                    //공격일 경우
                    else if (playerPlan[0].Tag==ActTag.Attack)
                    {
                        // 적의 공격 플랜을 모두 버린다
                        while (enemyPlan.Count > 0 && enemyPlan[0].Tag == ActTag.Attack)
                        {
                            enemyPlan.RemoveAt(0);
                        }

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
                        currentTurn = TurnState.PlayerTurn;
                    }
                    else if(enemyPlan[0].Tag == ActTag.Attack)
                    {
                        player[0].TakeDamage("Head", enemyPlan[0].Damage);
                        enemyPlan.RemoveAt(0);

                    }
                    //방어일 경우
                    else if (enemyPlan[0].Tag == ActTag.Defense)
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

        //UI 보여주기
        void ShowActList()
        {
            main.style.display = DisplayStyle.None;
            actList.style.display = DisplayStyle.Flex;

            //리스트 초기화
            actList.Clear();

            //ActList값 읽어서 ui바에 추가하기
            var MainWeapon = player[0].MainWeapon;
            foreach (var act in MainWeapon.ActList)
            {
                if (act == null) continue;

                var b = new Button(() => Enqueue(act, player[0]))
                {
                    text = $"{act.name}(Signal: {act.Signal})"
                };
                actList.Add(b);
            }
            //back 버튼추가
            actList.Add(backAttackBtn);
        }

        //댁을 넣는 조건문 함수
        void Enqueue(ActionDef act, CombtantEntity CheckEntity)
        {
            if (CheckEntity.team == Team.Player)
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
            else if (CheckEntity.team == Team.Enemy)
            {
                if (enemySignal + act.Signal > enemySpeed)
                {
                    Debug.Log("신호를 넘었습니다. \n다른 기술로 넣든 아님 턴을 넘기세요.");
                    return;
                }

                enemyPlan.Add(act);
                enemySignal += act.Signal;

                Debug.Log($"{act.DisplayName}을 넣었습니다. \n현재 신호 : {enemySignal} 사용 가능 신호 : {enemySpeed - enemySignal}");
            }

        }

        //적 ai가 댁을 짜는 함수
        void EnemyAiEnqueue()
        {
            var enemyEntity = enemy[0]; // 지금은 적 1명만 있다고 가정
            var actList = enemyEntity.MainWeapon.ActList;

            enemyPlan.Clear();   // 이전 턴의 계획 초기화
            enemySignal = 0;

            // 적이 덱을 짜는 루프
            while (true)
            {
                // 현재 신호가 속도를 초과할 경우 중단
                if (enemySignal >= enemySpeed)
                    break;

                // 가능한 액션들만 필터링 (null 제외, Signal 초과하지 않는 것만)
                var availableActs = new List<ActionDef>();
                foreach (var act in actList)
                {
                    if (act == null) continue;
                    if (enemySignal + act.Signal <= enemySpeed)
                        availableActs.Add(act);
                }

                // 선택 가능한 액션이 없으면 중단
                if (availableActs.Count == 0)
                    break;

                // 랜덤으로 하나 골라 Enqueue
                var choice = availableActs[Random.Range(0, availableActs.Count)];
                Enqueue(choice, enemyEntity);

                // 랜덤으로 "행동 종료" 시도 (덱을 꼭 꽉 채우지 않도록)
                if (Random.value < 0.3f) // 30% 확률로 멈춤
                    break;
            }

            Debug.Log("Enemy Plan 준비 완료");
        }

        void MainShow()
        {
            main.style.display = DisplayStyle.Flex;
            actList.style.display = DisplayStyle.None;
        }

        //무작위 각 속도의 차이로 선공 후공 선택하는 함수
        void RTurn()
        {
            //먼저 이둘의 전체 스피드 값을 구하고
            int totalSpeed = playerSpeed + enemySpeed;

            //그 값을 Range에 넣고는 그 사이 값이 나오게 한 다음
            int roll = Random.Range(0, totalSpeed);

            //여기 조건문에서 roll 값이 플레이어 또는 적의 값에 조건이 됬을경우 그 대상이 선이 된다. 물론 선공 후공은 Excute함수에 넣었지만.
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