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
        private Parts[] runtimeParts;

        private void Awake()
        {
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

        public void TakeDamage(string partName, int amount)
        {
            var part = runtimeParts.FirstOrDefault(p => p.DisplayName == partName && p.Enabled);
            var core = runtimeParts.FirstOrDefault(p => p.Penalty == PartPenaltyType.CoreLoss);
            if (part == null) return;



            if (part.Id == core.Id) core.HP = Mathf.Clamp(core.HP - amount, 0, core.MaxHP);
            else part.HP = Mathf.Clamp(part.HP - amount, 0, part.MaxHP);

                IsBrokenPart();
        }

        public void IsBrokenPart()
        {

        }

    }
}

