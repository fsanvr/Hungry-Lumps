public class MoveComponent
{
    private int _moveSpeed;
    private MovePattern _movePattern;
    private int _costMove;
    private int _costMaxAdditional;
    private int _costUpgradeCost;

    public int GetSpeed()
    {
        return _moveSpeed;
    }

    public MovePattern GetPattern()
    {
        return _movePattern;
    }
    
    public int GetCostOfMove()
    {
        return _costMove - _costMaxAdditional;
    }
}