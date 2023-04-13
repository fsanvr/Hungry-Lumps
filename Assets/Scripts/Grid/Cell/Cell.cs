using UnityEngine;

public class Cell : MonoBehaviour
{
    private FoodComponent _foodComponent = null;
    private WallComponent _wallComponent = null;
    private BonusComponent _bonusComponent = null;
    //[SerializeField] private Vector2 position;
    
    public Vector2 position;

    public bool ContainsFood()
    {
        return _foodComponent is not null;
    }
    
    public void SetFood(FoodData food)
    {
        if (ContainsFood())
        {
            ClearFood();
        }
        _foodComponent = FoodComponent.Instantiate(this.gameObject, food);
    }
    
    public int GetFood()
    {
        return Mathf.CeilToInt(_foodComponent.Prize * 20);
    }

    public void ClearFood()
    {
        if (_foodComponent is null)
        {
            return;
        }
        _foodComponent.Destroy();
        _foodComponent = null;
    }
    
    public bool ContainsWall()
    {
        return _wallComponent is not null;
    }
    
    public void SetWall(WallData obj)
    {
        if (ContainsWall())
        {
            ClearWall();
        }
        _wallComponent = WallComponent.Instantiate(this.gameObject, obj);
    }

    public void ClearWall()
    {
        if (_wallComponent is null)
        {
            return;
        }
        _wallComponent.Destroy();
        _wallComponent = null;
    }
    
    public bool ContainsBonus()
    {
        return _bonusComponent is not null;
    }
    
    public void SetBonus(BonusData bonus)
    {
        if (ContainsBonus())
        {
            ClearBonus();
        }
        _bonusComponent = BonusComponent.Instantiate(this.gameObject, bonus);
    }
    
    public Buff GetBonus()
    {
        return _bonusComponent.Buff;
    }

    public void ClearBonus()
    {
        if (_bonusComponent is null)
        {
            return;
        }
        _bonusComponent.Destroy();
        _bonusComponent = null;
    }

    public bool IsPassable()
    {
        return !ContainsWall() || _wallComponent.Passable;
    }
    
    public bool IsNotPassable()
    {
        return !IsPassable();
    }
}
