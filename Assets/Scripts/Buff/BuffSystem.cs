using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : InitializableBehaviour
{
    [SerializeField] private BuffableSystemsDictionary dictionary;

    public void ProcessBuff(Buff buff)
    {
        var system = dictionary.GetValueOrDefault(buff.SystemName, null);
        if (system)
        {
            ((Buffable)system).AddBuff(buff);
        }
    }
    protected override void MyInit(LevelData data)
    {
    }
}