using Game.Battle;
using System;
using System.Collections.Generic;
using Unity.InferenceEngine;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("무기 기본 정보")]
    public string displayName = "Sword";
    public int id;
    public Sprite icon;

    [Header("연결된 행동 템플릿")]
    public List<ActSO> actList;  // 🧠 무기별 행동 (ActSO 연결)

    public void CreateCheckDisplayname(List<Act> actList)
    {
        actList.Clear();

        for(int i =0; i < actList.Count; i++)
        {
            actList.Add(new Act());
            actList[i].displayName = displayName = actList[0].displayName;
    }

    }
}