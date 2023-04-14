using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : InitializableBehaviour
{
    [SerializeField] private InputSystem input;
    [SerializeField] private SatietySystem satietySystem;
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private PlayerComponent playerComponent;

    [SerializeField] private BuffableSystemsDictionary dictionary; //TODO: выделить в систему
    
    [SerializeField] public PathDrawer pathDrawer;

    private MovePattern _movePattern;

    protected override void MyInit(LevelData data)
    {
        _movePattern = InitPattern(data.pet.moveComponent.moveData);
        _movePattern.PlayerMoved.AddListener(MovePlayerTo);
    }

    private MovePattern InitPattern(MoveData data)
    {
        return data.type switch
        {
            MovePatternType.One => new LineMovePattern(input, gridSystem, playerComponent, data),
            MovePatternType.Line => new LineMovePattern(input, gridSystem, playerComponent, data),
            _ => new LineMovePattern(input, gridSystem, playerComponent, data)
        };
    }

    private void OnDestroy()
    {
        if (_movePattern is not null)
        {
            _movePattern.PlayerMoved.RemoveListener(MovePlayerTo);
        }
    }

    private void Update()
    {
        _movePattern.Update();
        UpdatePath();
    }

    private void UpdatePath()
    {
        var cells = _movePattern.CellToMove;
        pathDrawer.Draw(cells);
    }

    private void MovePlayerTo(Cell cell)
    {
        if (!PlayerCanMove())
        {
            return;
        }
        
        if (cell.ContainsFood())
        {
            satietySystem.FillSatiety();
            cell.ClearFood();
        }
        else
        {
            satietySystem.DecreaseSatiety(_movePattern.MoveCost);
        }

        if (cell.ContainsBonus())
        {
            ProcessBuff(cell.GetBonus());
            cell.ClearBonus();
        }
        playerComponent.transform.position = cell.position;
    }

    private bool PlayerCanMove()
    {
        return satietySystem.CurrentSatiety >= _movePattern.MoveCost;
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