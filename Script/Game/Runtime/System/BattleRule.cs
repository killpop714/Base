using System;
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

        //배경 패시브 정보
        public PassiveRule passiveRule;

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

        //적 정보
        public List<CombtantEntity> enemy;


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
            startBtn.clicked += ExecuteTurn;

            //행동 선택 이벤트
            backAttackBtn.clicked += MainShow;


            //시작 세팅
            StartTurn();
        }

        //기본 턴 시스템
        void StartTurn()
        {
            player[0].RSetSpeed();
            enemy[0].RSetSpeed();

            player[0].signal = 0;

            EnemyAiEnqueue();

            Debug.Log($"현재 턴 : {turn}");
            Debug.Log($"[플레이어 턴 시작] AP: {player[0].signal}");
        }
        void NextReadyTurn()
        {
            ++turn;
        }
        void EndTurn()
        {
            Debug.Log($"적 사망 시각: {turn} 이다");
        }

        //전투 턴 시스템
        void ExecuteTurn()
        {
            //확률로 선공 후공 정하기
            RTurn();



            while (true)
            {
                //튜플 반환식으로 선공 객체와 후공 객체를 구분
                var (self, target) = GetTurnEntites();


                //플랜에 값이 없을때 공수교대를 합니다
                if (self[0].plans.Count == 0)
                {
                    Debug.Log("값이 없으므로 상대에게 선공을 넘깁니다.");
                    //공수교대
                    currentTurn = currentTurn == TurnState.PlayerTurn ? TurnState.EnemyTurn : TurnState.PlayerTurn;
                    (self, target) = GetTurnEntites();

                    //다음 self도 값이 없을 시 턴을 넘김
                    if (self[0].plans.Count == 0)
                    {
                        Debug.Log("둘다 값이 없으니 턴을 종료합니다");
                        NextReadyTurn();
                        break;
                    }
                    
                 
                }
                else
                {

                    var selfPlan = self[0].plans[0];

                    switch (selfPlan.Tag)
                    {
                        //self의 plan이 공격 태그일 경우
                        case ActTag.Attack:

                            while (true)
                            {
                                try
                                {
                                    var targetPlan = target[0].plans[0];

                                    if (targetPlan.Tag == ActTag.Defense)
                                    {
                                        int damage = selfPlan.RGetDamage() / 10;

                                        PassiveRule rule = new();

                                        target[0].TakeDamage(target[0].runtimeParts[0], damage);
                                       
                                        

                                        Debug.Log($"{target[0].Data.DisplayName}이 {target[0].runtimeParts[0].DisplayName}에 {damage}만큼 대미지를 줬다");
                                        target[0].plans.RemoveAt(0);
                                        self[0].plans.RemoveAt(0);
                                        break;
                                    }

                                    else if (targetPlan.Tag == ActTag.Attack)
                                    {
                                        Debug.Log("상대 공격 버림");
                                        target[0].plans.RemoveAt(0);
                                    }
                                }
                                //targetPlan의 첫 값이 아예 없을때 예외 처리
                                catch (ArgumentException)
                                {
                                    int damage = selfPlan.RGetDamage();
                                    //target[0].TakeDamage("Head", damage);
                                    Debug.Log($"{target[0].Data.DisplayName}이 {target[0].runtimeParts[0].DisplayName}에 {damage}만큼 대미지를 줬다");
                                    self[0].plans.RemoveAt(0);
                                    break;
                                }
                            }
                            break;
                        //self의 plan이 방어 태그일 경우
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
                    NextReadyTurn();
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
            // 1. 적 엔티티 및 기본 정보 확인
            if (enemy == null || enemy.Count == 0 || enemy[0] == null || enemy[0].MainWeapon == null)
            {
                Debug.LogError("적 엔티티 또는 무기 정보가 유효하지 않습니다. AI 턴을 건너뜝니다.");
                return;
            }

            CombtantEntity Actor = enemy[0];

            // 이전 계획 및 시그널 초기화
            Actor.plans.Clear();
            Actor.signal = 0;

            // 2. 공격과 방어 행동 목록 필터링
            List<ActionDef> attackActions = new();
            List<ActionDef> defenseActions = new();

            if (Actor.MainWeapon.ActList != null)
            {
                foreach (var act in Actor.MainWeapon.ActList)
                {
                    if (act == null) continue;

                    if (act.Tag == ActTag.Attack)
                    {
                        attackActions.Add(act);
                    }
                    else if (act.Tag == ActTag.Defense)
                    {
                        defenseActions.Add(act);
                    }
                }
            }

            if (attackActions.Count == 0)
            {
                Debug.LogWarning("적에게 사용할 수 있는 공격 행동이 없습니다.");
            }

            // 3. 남은 Signal이 허용하는 한 행동을 무작위로 추가
            while (true)
            {
                int remainingSignal = Actor.speed - Actor.signal;

                // 실행 가능한 공격 및 방어 행동 리스트 준비
                List<ActionDef> affordableAttack = attackActions
                    .FindAll(act => act.Signal <= remainingSignal);

                List<ActionDef> affordableDefense = defenseActions
                    .FindAll(act => act.Signal <= remainingSignal);

                // 더 이상 아무 행동도 할 수 없으면 종료
                if (affordableAttack.Count == 0 && affordableDefense.Count == 0)
                {
                    break;
                }

                ActionDef selectedAct = null;

                // 행동 선택 로직: 25% 확률로 방어, 75% 확률로 공격 (방어가 없거나 Signal이 안되면 공격)
                int roll = UnityEngine.Random.Range(0, 100);

                if (roll < 25 && affordableDefense.Count > 0) // 25% 확률로 방어 선택 시도
                {
                    // 실행 가능한 방어 행동 중 무작위 선택
                    int randomIndex = UnityEngine.Random.Range(0, affordableDefense.Count);
                    selectedAct = affordableDefense[randomIndex];
                    Debug.Log("적 AI: 방어 행동을 선택했습니다.");
                }
                else if (affordableAttack.Count > 0) // 75% 확률 또는 방어 불가 시 공격 선택
                {
                    // 실행 가능한 공격 행동 중 무작위 선택
                    int randomIndex = UnityEngine.Random.Range(0, affordableAttack.Count);
                    selectedAct = affordableAttack[randomIndex];
                    Debug.Log("적 AI: 공격 행동을 선택했습니다.");
                }
                else if (affordableDefense.Count > 0) // 공격도 불가능한데 방어는 가능하다면 방어
                {
                    int randomIndex = UnityEngine.Random.Range(0, affordableDefense.Count);
                    selectedAct = affordableDefense[randomIndex];
                    Debug.Log("적 AI: 공격 불가로 방어 행동을 선택했습니다.");
                }
                else
                {
                    // 모든 행동이 Signal 한도 초과
                    break;
                }

                // 선택된 행동 큐에 추가 및 Signal 업데이트
                Actor.plans.Add(selectedAct);
                Actor.signal += selectedAct.Signal;

                Debug.Log($"적 AI: {selectedAct.DisplayName}을(를) 계획에 추가. 현재 신호: {Actor.signal}/{Actor.speed}");
            }
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
            int roll = UnityEngine.Random.Range(0, totalSpeed);

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