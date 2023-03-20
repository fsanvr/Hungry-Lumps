using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LevelInitData data;
    private int _satiety;
    private int _costOfMove;

    private void Start()
    {
        _satiety = data.startSatiety;
        _costOfMove = data.costOfMove;
    }

    public void MoveTo(Cell cell)
    {
        if (PossibleToMove())
        {
            Move(cell.position);
            Eat(cell);
            Debug.Log(_satiety);
        }

        if (NotPossibleToMove())
        {
            CantMove();
        }
    }

    private bool PossibleToMove()
    {
        return _satiety - _costOfMove >= 0;
    }
    
    private bool NotPossibleToMove()
    {
        return !PossibleToMove();
    }

    private void Eat(Cell cell)
    {
        _satiety += cell.GetFood();
        cell.ClearFood();
    }

    public void Move(Vector2 position)
    {
        _satiety -= _costOfMove;
        transform.position = position;
    }

    public void CantMove()
    {
        Debug.Log("Can't move");
    }
}