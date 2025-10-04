using Game.Battle;
using System.Linq;
using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(menuName = "Battle/Passives/Burning", fileName = "Burning")]
public class TurnBurning : Passives
{
    public TurnBurning()
    {
        disPlayName = "����";
        description = $"��Ÿ�� ���� ��� �ൿ ������ ����� {damage}�� ������ ������ 3 �̻����� 3���� ���θ� ���ſ� ���� �нú갡 �ٽ��ϴ�.\n�нú긦 �����Ϸ��� ������ ���������� �����ؾ� �մϴ�.";
        id = 0;
    }

    //���� ��
    [SerializeField] int Value = 0;
    [SerializeField] int maxValue = 0;

    //�����
    [SerializeField] int damage = 0;


    //��� �нú� ���� ��Ģ
    public override void Apply(Triger triger, Combtant self, Combtant target, Parts part)
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

    //�нú�� Ư�� ����� �߽����� �۵��ϴ°� �ƴ� �ڽ��� �߽����� �۵� ��Ű�� ���踦 �ؾ߸� �Ѵ�.
    //��� ���� �нú� �ߵ�
    public override void Excute(Triger triger, Combtant self, Combtant target, Parts part)
    {
        //��� ��Ʋ�� Ʈ���ſ� �۵���
        if (target.passives.Any(p => p.id == id) && triger == AllBattleRuleTriger)
        {
            
            target.TakeDamage(part, damage);
            Debug.Log($"{target.Data.DisplayName}�� {part.DisplayName}�� {damage}��ŭ ��ź��.");
            Debug.Log($"{target.Data.DisplayName}�� ���� ä��: {part.HP}");

            Remove(self, target, part);
            return;
        }

        self.TakeDamage(part, damage);
        Debug.Log($"{self.Data.DisplayName}�� {part.DisplayName}�� {damage}��ŭ ��ź��.");
        Debug.Log($"{self.Data.DisplayName}�� ���� ä��: {part.HP}");
    }

    public override void Remove(Combtant self, Combtant target, Parts part)
    {
        throw new NotImplementedException();
    }


}
