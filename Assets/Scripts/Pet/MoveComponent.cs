using System;
using UnityEngine;

[Serializable]
public class MoveComponent
{
    [SerializeField] public MoveData moveData;
    
    private Func<int, int> MoveSpeedUpgradeCost 
        => step => (int)Math.Exp(Math.Pow(step, 0.6f));
    private Func<int, int> MoveCostUpgradeCost 
        => step => (int)Math.Exp(Math.Pow(step, 0.6f));

    public void UpgradeMoveSpeed()
    {
        if (IsMoveSpeedMaxUpgradeStep())
        {
            return;
        }
    
        moveData.moveDurationUpgradeStepCurrent++;
    }
    
    public void UpgradeMoveCost()
    {
        if (IsMoveCostMaxUpgradeStep())
        {
            return;
        }
    
        moveData.moveCostUpgradeStepCurrent++;
    }
    
    public bool IsMoveSpeedMaxUpgradeStep()
    {
        return moveData.moveDurationUpgradeStepCurrent == moveData.moveDurationUpgradeSteps;
    }
    
    public bool IsMoveCostMaxUpgradeStep()
    {
        return moveData.moveCostUpgradeStepCurrent == moveData.moveCostUpgradeSteps;
    }
    
    public int GetMoveSpeedUpgradeCost()
    {
        return MoveSpeedUpgradeCost(moveData.moveDurationUpgradeStepCurrent);
    }

    public int GetMoveCostUpgradeCost()
    {
        return MoveCostUpgradeCost(moveData.moveCostUpgradeStepCurrent);
    }
    
    public float GetMoveDuration()
    {
        return moveData.GetMoveDuration();
    }
    
    public int GetMoveCost()
    {
        return moveData.GetMoveCost();
    }
}