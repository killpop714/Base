

using Game.Battle;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActSystem : MonoBehaviour
{
    public List<ActSO> Data;
    public List<ActSO> runtimeActs;
    
    public void SetActData(Combtant playerTeam, Combtant enemyTeam)
    {
        if (runtimeActs.Count == 0 || runtimeActs.Any(r => playerTeam.MainWeapon.ActList.Any(p => r.Id == p.Id)))
            runtimeActs[0] = playerTeam.MainWeapon.ActList[0];
    }


    public int RGetDamage()
    {
        return Random.Range(minValue, maxValue);
    }
}
