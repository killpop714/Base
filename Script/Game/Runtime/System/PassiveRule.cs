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
        //���� ��ü���� �нú� ����
        public void ApplyPassive(Passives ins, CombtantEntity target, Parts part)
        {
            ins.Apply(target, part);
        }

        //��ο��� �нú� ����
        public void ApplyAllPassives(Passives glocalins, List<CombtantEntity> AllTarget)
        {
            for (int i = 0; i < AllTarget.Count; i++)
            {
                Debug.Log(AllTarget[i]);
            }

        }

        //���� ��ü���� �нú�  �۵�
        public void ExcutePassive(CombtantEntity target)
        {

        }

        //��� ��ü���� �нú� �۵�
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
            Debug.Log("�ƹ��͵� ���� �нú�");
        }
    }

    
    public class BurningPassive : Passives
    {
        public BurningPassive()
        {
            DisplayName = "��Ÿ�� ��";
            id = 0;
        }
        private int minValue = 0;
        private int maxValue = 0;

        //��� �нú� ���� ��Ģ
        public override void Apply(CombtantEntity target, Parts part)
        {
            //��� ���� �нú� ���� ��Ģ
            if (part == null)
            {
                //��� ���� �нú꿡 �ش� Ŭ������ �ִ��� Ž��
                if (target.passives.Any(p => p.id == id))
                {
                    target.passives.Add(this);
                    return;
                }
            }
            else // ��� ��Ʈ �нú� ���� ��Ģ
            {
                //��� ��Ʈ �нú꿡 �ش� Ŭ������ �ִ��� Ž��
                if (part.passives.Any(p => p.id == id))
                {
                    target.passives.Add(this);
                    return;
                }
            }

        }

        //��� ���� �нú� �ߵ�
        public override void Excute(CombtantEntity target)
        {
            //��� ��Ʈ���� ���� ��Ģ
            if (target.passives.Any(p => p.id == id))
            {
                target.TakeDamage(target.runtimeParts[0], 10);
                return;
            }
        }

       
    }
}