using Game.Battle;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Passives/None", fileName = "None")]
public class None : Passives
{
    public override void Apply(Triger triger, Combtant self, Combtant target, Parts part)
    {
        throw new NotImplementedException();
    }

    public override void Excute(Triger triger, Combtant self, Combtant target, Parts part)
    {
        Debug.Log("¿€µø¡ﬂ");
    }

    public override void Remove(Combtant self, Combtant target, Parts part)
    {
        throw new NotImplementedException();
    }
}
