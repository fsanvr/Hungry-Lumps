using UnityEngine;

public class Cell : MonoBehaviour
{
    private FoodComponent _foodComponent = null;
    private ObjectComponent _objectComponent = null;
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
        _foodComponent.Destroy();
        _foodComponent = null;
    }
    
    public bool ContainsObject()
    {
        return _objectComponent is not null;
    }
    
    public void SetObject(ObjectData obj)
    {
        if (ContainsObject())
        {
            ClearObject();
        }
        _objectComponent = ObjectComponent.Instantiate(this.gameObject, obj);
    }

    public void ClearObject()
    {
        _objectComponent.Destroy();
        _objectComponent = null;
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
        _bonusComponent.Destroy();
        _foodComponent = null;
    }

    public bool IsPassable()
    {
        return !ContainsObject() || _objectComponent.Passable;
    }
    
    public bool IsNotPassable()
    {
        return !IsPassable();
    }
}
