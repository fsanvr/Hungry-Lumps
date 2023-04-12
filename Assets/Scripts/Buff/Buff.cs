using System;
using UnityEngine.Serialization;

[Serializable]
public class Buff
{
    public Func<float, float> Apply { get; private set; }
    public string ApplyToName { get; private set; }
    
    public string SystemName { get => ApplyToName.Split(".")[0]; }
    public string PropertyName { get => ApplyToName.Split(".")[1]; }

    public Buff(Func<float, float> func, string name)
    {
        Apply = func;
        ApplyToName = name;
    }
    
    public float ApplyByName(float value, string name)
    {
        if (ApplyToName.Equals(name))
        {
            return Apply(value);
        }

        return value;
    }
}