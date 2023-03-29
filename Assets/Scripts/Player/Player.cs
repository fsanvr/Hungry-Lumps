using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public readonly SatietyUpdateEvent SatietyChanged = new SatietyUpdateEvent();
    private int _minSatiety;
    private int _maxSatiety;
    private int _satiety;
    private int _costOfMove;

    private void Start()
    {
        var map = InitData.GetFoodMap(0);
        _minSatiety = map.MinSatiety;
        _maxSatiety = map.MaxSatiety;
        _satiety = map.StartSatiety;
        _costOfMove = map.CostOfMove;
    }

    public void MoveTo(Cell cell)
    {
        if (PossibleToMove())
        {
            Move(cell.position);
            Eat(cell);
            if (cell.isFinish)
            {
                FinishLevel();
            }
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
        ChangeSatiety(+cell.GetFood());
        cell.ClearFood();
    }

    private void Move(Vector2 position)
    {
        ChangeSatiety(-_costOfMove);
        transform.position = position;
    }

    private void CantMove()
    {
        Debug.Log("Lose(");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ChangeSatiety(int delta)
    {
        _satiety += delta;
        _satiety = _satiety < _minSatiety ? _minSatiety : _satiety;
        _satiety = _satiety > _maxSatiety ? _maxSatiety : _satiety;
        SatietyChanged.Invoke(_satiety * 1.0f / _maxSatiety);
    }

    private void FinishLevel()
    {
        Debug.Log("Finish!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}