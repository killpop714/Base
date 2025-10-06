

using Game.Battle;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ActSystem : MonoBehaviour
{
    [SerializeField] List<ActSO> Data;
    public List<Act> runtimeActs = new List<Act>();

    //�ʱ� ������ �ʱ�ȭ �Ҷ��� ���
    public void SetAct()
    {
        runtimeActs.Clear();

        // Data ����Ʈ�� ũ�⸸ŭ �ݺ��ϸ� ��Ÿ�� Act ��ü�� �����մϴ�.
        for (int i = 0; i < Data.Count; i++)
        {
            // 1. DataSO�� ������ �����ɴϴ�.
            ActSO dataTemplate = Data[i];

            // 2. ���ο� Act ��ü�� �����Ͽ� �����͸� �����մϴ�.
            Act newAct = new Act();

            // 3. ������ UUID�� Ȯ���ϰ� ��ü�� �Ҵ��ϴ� ���� ����
            string safeUuid;

            // �ߺ��� �߰ߵ��� ���� ������ �ݺ��մϴ�. (GUID Ư���� 99.999...% Ȯ���� �� ���� ���)
            while (true)
            {
                // ���ο� UUID ����
                safeUuid = Guid.NewGuid().ToString();

                // �ߺ� �˻�: runtimeActs ����Ʈ�� �̹� ���� UUID�� ���� ��Ұ� �ִ��� Ȯ��
                // �� �˻簡 �ٷ� '������ ����'�� ����ϴ� �����Դϴ�.
                if (runtimeActs.Any(p => p.uuid == safeUuid))
                {
                    // �ߺ� �߻� (õ������ ����): ��� �α׸� ���� ������ �ٽ� ���� (�����)
                    Debug.LogWarning($"UUID �浹 ����! ��õ�: {safeUuid}");
                }
                else
                {
                    // �ߺ��� ����: ������ Ż���մϴ�.
                    break;
                }
            }

            // 4. ������ UUID�� ���纻�� �Ҵ��մϴ�.
            // (CreateRuntimeCopy�� UUID�� �޾� Act ��ü�� �ʵ忡 �Ҵ��ϴ� �ڵ带 ����)
            Act act = dataTemplate.CreateRuntimeCopy(safeUuid);

            // 5. ��Ÿ�� ����Ʈ�� �߰��մϴ�.
            runtimeActs.Add(act);
        }
    }
}
