using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace Game.Battle 
{ 


    public abstract class Passives 
    {
        public string DisplayName { get; protected set; }
        public int id { get; protected set; }

        public abstract void Apply(CombtantEntity target, Parts part);

        public abstract void Excute(CombtantEntity target);
        
    }

    public class PassiveRule 
    {
        //단일 개체에게 패시브 적용
        public void ApplyPassive(Passives ins, CombtantEntity target, Parts part)
        {
            ins.Apply(target, part);
        }

        //모두에게 패시브 적용
        public void ApplyAllPassives(Passives glocalins, List<CombtantEntity> AllTarget)
        {
            for (int i = 0; i < AllTarget.Count; i++)
            {
                Debug.Log(AllTarget[i]);
            }

        }

        //단일 개체에게 패시브  작동
        public void ExcutePassive(CombtantEntity target)
        {

        }

        //모든 개체에게 패시브 작동
        public void ExcuteAllPassives(List<CombtantEntity> AllTarget) 
        {
            for (int i = 0; i < AllTarget.Count; i++)
            {
                Debug.Log(AllTarget[i]);
            }
        }

    }

    public class None : Passives
    {
        public override void Apply(CombtantEntity target, Parts part)
        {
            throw new NotImplementedException();
        }

        public override void Excute(CombtantEntity target)
        {
            Debug.Log("아무것도 없는 패시브");
        }
    }

    
    public class BurningPassive : Passives
    {
        public BurningPassive()
        {
            DisplayName = "불타는 중";
            id = 0;
        }
        private int minValue = 0;
        private int maxValue = 0;

        //대상 패시브 적용 규칙
        public override void Apply(CombtantEntity target, Parts part)
        {
            //대상 전역 패시브 적용 규칙
            if (part == null)
            {
                //대상 전역 패시브에 해당 클래스가 있는지 탐색
                if (target.passives.Any(p => p.id == id))
                {
                    target.passives.Add(this);
                    return;
                }
            }
            else // 대상 파트 패시브 적용 규칙
            {
                //대상 파트 패시브에 해당 클래스가 있는지 탐색
                if (part.passives.Any(p => p.id == id))
                {
                    target.passives.Add(this);
                    return;
                }
            }

        }

        //대상 전역 패시브 발동
        public override void Excute(CombtantEntity target)
        {
            //대상에 파트에만 적용 규칙
            if (target.passives.Any(p => p.id == id))
            {
                target.TakeDamage(target.runtimeParts[0], 10);
                return;
            }
        }

       
    }
}