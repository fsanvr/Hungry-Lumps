using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData data;

    public void MoveTo(Cell cell)
    {
        if (PossibleToMove())
        {
            Move(cell.position);
            Eat(cell);
            Debug.Log(data.satiety);
        }

        if (NotPossibleToMove())
        {
            CantMove();
        }
    }

    private bool PossibleToMove()
    {
        return data.satiety - data.costOfMove >= 0;
    }
    
    private bool NotPossibleToMove()
    {
        return !PossibleToMove();
    }

    private void Eat(Cell cell)
    {
        data.satiety += cell.GetFood();
        cell.ClearFood();
    }

    public void Move(Vector2 position)
    {
        data.satiety -= data.costOfMove;
        transform.position = position;
    }

    public void CantMove()
    {
        Debug.Log("Can't move");
    }
}