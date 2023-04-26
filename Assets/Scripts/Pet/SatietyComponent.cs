using System;

[Serializable]
public class SatietyComponent
{
    public int satietyBase;
    public int satietyMax;
    
    public int upgradeSteps;
    public int upgradeStepCurrent = 0;
    private Func<int, int> SatietyUpgradeCost 
        => step => (int)Math.Exp(Math.Pow(step, 0.8f));

    public void Upgrade()
    {
        if (IsMaxUpgradeStep())
        {
            return;
        }

        upgradeStepCurrent++;
    }
    
    public int GetUpgradeCost()
    {
        return SatietyUpgradeCost(upgradeStepCurrent);
    }

    public bool IsMaxUpgradeStep()
    {
        return upgradeStepCurrent == upgradeSteps;
    }

    public int GetSatiety()
    {
        return satietyBase + (satietyMax - satietyBase) / upgradeSteps * upgradeStepCurrent;
    }
}