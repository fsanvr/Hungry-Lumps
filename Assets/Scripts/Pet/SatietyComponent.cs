using System;

[Serializable]
public class SatietyComponent
{
    public int satietyBase;
    public int satietyMaxAdditional;
    public int satietyUpgradeCost;

    public int GetUpgradeCost()
    {
        return satietyUpgradeCost;
    }

    public int GetSatiety()
    {
        return satietyBase; // + satietyMaxAdditional;
    }
}