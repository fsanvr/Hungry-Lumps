using UnityEngine;
[CreateAssetMenu]
public class MoveData : ScriptableObject
{
    public MovePatternType type;
    public int maxSteps;
    
    public int moveCostBase;
    public int moveCostMin;
    public int moveCostUpgradeSteps;
    public int moveCostUpgradeStepCurrent = 0;
    
    public float moveDurationBase;
    public float moveDurationMin;
    
    public int moveDurationUpgradeSteps;
    public int moveDurationUpgradeStepCurrent = 0;
    
    public float GetMoveDuration()
    {
        return moveDurationBase
                     - (moveDurationBase - moveDurationMin)
                     / moveDurationUpgradeSteps
                     * moveDurationUpgradeStepCurrent;
    }
    
    public int GetMoveCost()
    {
        return (int)(moveCostBase
                     - (moveCostBase - moveCostMin)
                     / moveCostUpgradeSteps
                     * moveCostUpgradeStepCurrent
            );
    }
}