using System;
using System.Collections.Generic;
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




        private void Awake()
        {
            if (Data == null)
            {
                Debug.LogError("Data가 할당되지 않았습니다!");
                return;
            }

            if (Data.parts == null)
            {
                Debug.LogError("Data.parts가 비어 있습니다!");
                return;
            }
            //runtimeParts를 세팅
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

        public void IsBrokenPart(Parts part)
        {
            if (part.HP == 0)
            {
                
            }
        }

    }
}

