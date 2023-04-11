using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private PlayerData playerData;
    public readonly SatietyUpdateEvent SatietyChanged = new SatietyUpdateEvent();
    private int _minSatiety;
    private int _maxSatiety;
    private int _satiety;
    private int _costOfMove;

    private Pet _pet;

    public void Init(LevelData data)
    {
        var pet = data.Pet;
        _pet = pet; //TODO: use it
        _minSatiety = 0;
        _maxSatiety = 100;
        _satiety = 20;
        _costOfMove = 5;
        
        SatietyChanged.Invoke(NormalizedSatiety());
    }

    private float NormalizedSatiety()
    {
        return _satiety * 1.0f / _maxSatiety;
    }

    public void MoveTo(Cell cell)
    {
        if (PossibleToMove() && cell.IsPassable())
        {
            Move(cell.position);
            if (cell.ContainsFood())
            {
                Eat(cell);
            }
            Changed();
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
    }

    private void Changed()
    {
        SatietyChanged.Invoke(NormalizedSatiety());
    }

    private void FinishLevel()
    {
        Debug.Log("Finish!");
        playerData.satiety += _satiety;
        levelSystem.EnableNewLevel();
    }
}