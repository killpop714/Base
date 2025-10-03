

using Game.Battle;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Battle
{
    // 팀 구분
    public enum Team { Player, Enemy }
    public enum Type { Entity, Object }



    public class CombtantEntity : MonoBehaviour
    {
        //현재 캐릭터의 정보
        [Tooltip("이 캐릭터의 데이터")]
        public Combatant Data;
        public WeaponSO MainWeapon;
        public Parts[] runtimeParts;

        //생물체 물체 표기
        public Team team;
        public Type Type;

        //내부 전투 규칙용 변수
        public List<ActionDef> plans;
        public int speed;
        public int signal;
        public List<Passives> passives =new();








        private void Awake()
        {
            //엔티티의 기본 정보들을 runtime에 굴러가게 생성

            //엔티티의 파트들의 정보들을 runtime에 굴러가게 생성
            runtimeParts = Data.parts.Select(p => new Parts
            {
                DisplayName = p.DisplayName,
                Id = p.Id,
                MaxHP = p.MaxHP,
                HP = p.HP,
                Enabled = p.Enabled,
                Penalty = p.Penalty
            }).ToArray();
            
        }

        public bool IsAlive =>
        !(runtimeParts?.Any(p => p.IsBroken &&
        (p.Penalty == PartPenaltyType.CoreLoss ||
         p.Penalty == PartPenaltyType.VitalLoss)) ?? false);
        public void TakeDamage(Parts part, int amount)
        {
            Parts core = runtimeParts.FirstOrDefault(p => p.Penalty == PartPenaltyType.CoreLoss);
            if (part == null) return;



            if (part.Id == core.Id)
            {
                part.HP = Mathf.Clamp(core.HP - amount, 0, core.MaxHP);
                core.HP = Mathf.Clamp(core.HP - amount, 0, core.MaxHP);

            }
            else
            {
                part.HP = Mathf.Clamp(part.HP - amount, 0, part.MaxHP);
            }

            Debug.Log(part.HP);

            

            IsBrokenPart(part);
        }

        //속도가 최소치와 최대치를 랜덤하게 구하는 함수
        public void RSetSpeed()
        {
            speed = Random.Range(Data.minSpeed, Data.maxSpeed);
        }

        //파트가 부서질때 주어지는 패널티
        public void IsBrokenPart(Parts part)
        {
            if (part.HP == 0)
            {
                
            }
        }

    }
}

