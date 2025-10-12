//using Game.Battle;
//using System.Linq;
//using System;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEngine.EventSystems.EventTrigger;

//[CreateAssetMenu(menuName = "Battle/Passives/Burning", fileName = "Burning")]
//public class TurnBurning : Passives
//{
//    public TurnBurning()
//    {
//        disPlayName = "����";
//        description = $"��Ÿ�� ���� ��� �ൿ ������ ����� {damage}�� ������ ������ 3 �̻����� 3���� ���θ� ���ſ� ���� �нú갡 �ٽ��ϴ�.\n�нú긦 �����Ϸ��� ������ ���������� �����ؾ� �մϴ�.";
//        type = PassiveType.beginning;
//        id = 0;
//    }

//    //���� ��
//    private int count = 0;
//    private int maxCount = 3;

//    //�����
//    private int damage = 1;


//    //��� �нú� ���� ��Ģ
//    public override void Apply(Triger triger, PassiveOverride passiveOverrider, Combtant self, Combtant target, Parts part)
//    {
//        //��� ���� �нú� ���� ��Ģ
//        //if (part == null)
//        //{
//        //    //��� ���� �нú꿡 �ش� Ŭ������ �ִ��� Ž��
//        //    if (target.passives.Any(p => p.id == id))
//        //    {
//        //        target.passives.Add(this);
//        //        return;
//        //    }
//        //}
//        //��� ��Ʈ �нú꿡 �ش� Ŭ������ �ִ��� Ž��
//        if(triger == Triger.OnSuppress)
//        {
//            if (!part.passives.Any(p => p.id == id))
//            {
//                TurnBurning ins = ScriptableObject.CreateInstance<TurnBurning>();

//                part.passives.Add(ins);
//                count = Math.Min(count + 1, maxCount);

//                return;
//            }
//            count = Math.Min(count + 1, maxCount);
//        }
//    }

//    //�нú�� Ư�� ����� �߽����� �۵��ϴ°� �ƴ� �ڽ��� �߽����� �۵� ��Ű�� ���踦 �ؾ߸� �Ѵ�.
//    //���Ŵ� ������ Execute�� �۵� ��Ű�� return ���Ѷ�
//    //��� ���� �нú� �ߵ�
//    public override void Execute(Triger triger, PassiveOverride passiveOverrider, Combtant self, Combtant target, Parts part)
//    {
//        //��� ��Ʋ�� Ʈ���ſ� �۵���

//        if(passiveOverrider == PassiveOverride.OnFirePutOut || part.IsBroken)
//        {
//            Remove(self, target, part);
//            return;
//        }

//        Debug.Log("���� �۵���");
//        if (type == PassiveType.beginning)
//        {
//            Debug.Log("beginning Ȱ��ȭ");
//            type = PassiveType.progress;
//            return;
//        }

//        self.TakeDamage(part, damage);
//        Debug.Log($"{self.Data.DisplayName}�� {part.DisplayName}�� {damage}��ŭ ��ź��.");
//        Debug.Log($"{self.Data.DisplayName}�� ���� ä��: {part.HP}");
//    }

//    public override void Remove(Combtant self, Combtant target, Parts part)
//    {
//        Debug.Log("���� �нú� ����");
//        part.passives.Remove(this);
//    }
//}


