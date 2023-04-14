using UnityEngine;

[CreateAssetMenu]
public class MoveData : ScriptableObject
{
    public MovePatternType type;
    public float moveCost;
    public int maxSteps;
}