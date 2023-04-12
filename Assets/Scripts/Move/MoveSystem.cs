using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSystem : InitializableBehaviour
{
    [SerializeField] private InputSystem input;
    [SerializeField] private SatietySystem satietySystem;
    [SerializeField] private PlayerComponent playerComponent;
    [SerializeField] private GridSystem gridSystem;

    [SerializeField] private float moveDelayInSeconds = 3.5f;
    private float _currentDelay = 0.0f;

    private float _moveCost;

    [SerializeField] private BuffableSystemsDictionary dictionary;

    protected override void MyInit(LevelData data)
    {
        input.MouseClicked.AddListener(OnMouseClicked);
        _moveCost = 2.0f;
        //_moveCost = data.Pet.MoveComponent.GetCostOfMove();
    }

    private void OnDestroy()
    {
        input.MouseClicked.RemoveListener(OnMouseClicked);
    }
    
    private void Update()
    {
        ProcessDelay();
    }

    private void ProcessDelay()
    {
        if (_currentDelay >= 0.0f)
        {
            _currentDelay -= Time.deltaTime;
        }
    }

    private void OnMouseClicked(Collider2D clicked)
    {
        if (IsCell(clicked.gameObject))
        {
            var cell = clicked.GetComponent<Cell>();
            if (gridSystem.IsNeighbour(playerComponent.transform.position, cell) &&
                cell.IsPassable() &&
                PlayerCanMove())
            {
                MovePlayerTo(cell);
            }
        }
    }
    
    private static bool IsCell(GameObject go)
    {
        return go.GetComponent<Cell>() != null;
    }
    
    private bool PlayerCanMove()
    {
        return _currentDelay <= 0.0f && satietySystem.CurrentSatiety >= _moveCost;
    }

    private void MovePlayerTo(Cell cell)
    {
        if (cell.ContainsFood())
        {
            satietySystem.FillSatiety();
            cell.ClearFood();
        }
        else
        {
            satietySystem.DecreaseSatiety(_moveCost);
        }

        if (cell.ContainsBonus())
        {
            ProcessBuff(cell.GetBonus());
            cell.ClearBonus();
        }
        _currentDelay = moveDelayInSeconds;
        playerComponent.transform.position = cell.position;
    }

    private void ProcessBuff(Buff buff)
    {
        var system = dictionary.GetValueOrDefault(buff.SystemName, null);
        if (system)
        {
            ((Buffable)system).AddBuff(buff);
        }
    }
}