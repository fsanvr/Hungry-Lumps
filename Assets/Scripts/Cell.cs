using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2 position;
    public bool isFinish;
    private int _food;

    public void SetFood(int value)
    {
        _food = value;
    }

    public int GetFood()
    {
        return _food;
    }

    public void ClearFood()
    {
        _food = 0;
        var foodComponent = transform.GetChild(0).GetComponent<SpriteRenderer>();
        foodComponent.sprite = null;
    }
}
