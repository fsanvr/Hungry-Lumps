public class SatietyComponent
{
    private int _satietyBase;
    private int _satietyMaxAdditional;
    private int _satietyUpgradeCost;

    public int GetUpgradeCost()
    {
        return _satietyUpgradeCost;
    }

    public int GetSatiety()
    {
        return _satietyBase + _satietyMaxAdditional;
    }
}