using System;
using System.Collections.Generic;
using System.Linq;
using TurnRpg;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Game.Battle
{
    // 팀 구분
    public enum Team { Player, Enemy }

    [CreateAssetMenu(menuName = "Battle/Unit")]
    public class Combatant : ScriptableObject
    {
        public string DisplayName = "Unit";
        public Team Team = Team.Player;

        [Header("Speed 스탯")]
        [Tooltip("최소값과 최대값은 신호값의 랜덤값을 돌릴때 사용됩니다.")]
        [SerializeField] int MinSpeed = 0;
        [SerializeField] int MaxSpeed = 0;

        public int minSpeed => MinSpeed;
        public int maxSpeed => MaxSpeed;


        [Header("Body Parts (Optional)")]
        [Tooltip("CoreLoss는 전체 체력 담당하고 VitalLoss는 즉사 나머지는 아이템 사용불가나 속도저하. 패널티를 없애려면 None으로 설정하세요.")]
        public Parts[] parts = new Parts[]
        {
            new() { DisplayName="Head", MaxHP=30, HP=30, Penalty=PartPenaltyType.VitalLoss,Enabled=true},
            new() { DisplayName="Trunk", MaxHP=30, HP=30, Penalty=PartPenaltyType.CoreLoss,Enabled=true},
            new() { DisplayName="Arm", MaxHP=30, HP=30, Penalty=PartPenaltyType.HandLoss,Enabled=true},
            new() { DisplayName="Leg", MaxHP=30, HP=30, Penalty=PartPenaltyType.SpeedLoss,Enabled=true}
        };

        
       // public bool IsAlive => !(parts?.Any(p => p.IsBroken && (p.Penalty == PartPenaltyType.CoreLoss || p.Penalty == PartPenaltyType.VitalLoss)) ?? false);

    }
}


