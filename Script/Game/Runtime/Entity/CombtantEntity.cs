

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Battle
{
    public class CombtantEntity : MonoBehaviour
    {
        [Tooltip("이 캐릭터의 데이터")]
        public Combatant Data;
        public WeaponSO MainWeapon;
        public Parts[] runtimeParts;
        public Team team;




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
        public void TakeDamage(string partName, int amount)
        {
            var part = runtimeParts.FirstOrDefault(p => p.DisplayName == partName && p.Enabled);
            var core = runtimeParts.FirstOrDefault(p => p.Penalty == PartPenaltyType.CoreLoss);
            if (part == null) return;



            if (part.Id == core.Id)
            {
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
        public int GetSpeed()
        {
            return Random.Range(Data.minSpeed, Data.maxSpeed);
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

