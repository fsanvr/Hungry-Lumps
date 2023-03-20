using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2 position;
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
    }
}
