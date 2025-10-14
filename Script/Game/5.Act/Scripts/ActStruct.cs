using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public enum ActTag { None, Suppress, Survival, Utility }

public struct ActComponent: IBufferElementData
{
    public FixedString64Bytes displayName;
    public int id;

    public ActTag tag;

    public int signal;
    public int deleteSignal;

    public int minValue;
    public int maxValue;

    public int skillLevel;
    public int chance;  
}
