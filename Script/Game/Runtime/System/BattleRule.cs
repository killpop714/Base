using System.Collections.Generic;
using UnityEditor.UI;
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
        public List<CombtantEntity> player;
        public List<ActionDef> playerPlan;

        //적 정보
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
        }

        //기본 턴 시스템
        void ReadyTurn()
        {
            player[0].RSetSpeed();
            enemy[0].RSetSpeed();

            player[0].signal = 0;

            EnemyAiEnqueue();

            Debug.Log($"현재 턴 : {turn}");
            Debug.Log($"[플레이어 턴 시작] AP: {player[0].signal}");
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
                //튜플 반환식으로 선공 객체와 후공 객체를 구분
                var (self, target) = GetTurnEntites();


                //플랜에 아무것도 없을 경우
                if (self[0].plans.Count == 0)
                {
                    Debug.Log("값이 없으므로 상대에게 선공을 넘깁니다.");
                    currentTurn = TurnState.EnemyTurn;
                }
                else
                {

                    var selfPlan = self[0].plans[0];
                    self[0].plans.RemoveAt(0);

                    switch (selfPlan.Tag)
                    {
                        case ActTag.Attack:

                            while (target[0].plans.Count > 0)
                            {
                                var targetPlan = target[0].plans[0];

                                if (targetPlan.Tag == ActTag.Defense)
                                {
                                    target[0].TakeDamage("Head", selfPlan.RGetDamage());
                                    break;
                                }

                                if (targetPlan.Tag == ActTag.Attack)                                    
                                    target[0].plans.RemoveAt(0);
                                else
                                    break;
                            }
                            break;

                        case ActTag.Defense:
                            Debug.Log("방어 버림");
                            self[0].plans.RemoveAt(0);
                            break;

                    }
                }

                if (!self[0].IsAlive || !target[0].IsAlive)
                {
                    EndTurn();
                    break;
                }
                //enemy나 player의 플랜이 없을시 턴 종료
                else if (self[0].plans.Count == 0 && target[0].plans.Count == 0)
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
        void Enqueue(ActionDef act, CombtantEntity Actor)
        {

            if (Actor.team == Team.Player)
            {
                if (Actor.signal + act.Signal > Actor.speed)
                {
                    Debug.Log("신호를 넘었습니다. \n다른 기술로 넣든 아님 턴을 넘기세요.");
                    return;
                }

                Actor.plans.Add(act);
                Actor.signal += act.Signal;

                Debug.Log($"{act.DisplayName}을 넣었습니다. \n현재 신호 : {Actor.signal} 사용 가능 신호 : {Actor.speed - Actor.signal}");
            }
        }

        //적 ai가 댁을 짜는 함수
        void EnemyAiEnqueue()
        {
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
            int totalSpeed = player[0].speed + enemy[0].speed;

            //그 값을 Range에 넣고는 그 사이 값이 나오게 한 다음
            int roll = Random.Range(0, totalSpeed);

            //여기 조건문에서 roll 값이 플레이어 또는 적의 값에 조건이 됬을경우 그 대상이 선이 된다. 물론 선공 후공은 Excute함수에 넣었지만.
            if (roll < player[0].speed) 
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

        private (List<CombtantEntity> self, List<CombtantEntity> target) GetTurnEntites()
        {
            if (currentTurn == TurnState.PlayerTurn)
                return (player, enemy);
            else
                return (enemy, player);


        }
    } 
}