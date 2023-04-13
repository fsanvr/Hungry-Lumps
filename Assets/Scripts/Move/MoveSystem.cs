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
        _movePattern = new MovePattern(input, satietySystem, gridSystem, playerComponent);
        _movePattern.PlayerMoved.AddListener(MovePlayerTo);
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
        var cells = _movePattern.GetCellToDraw();
        pathDrawer.Draw(cells);
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
            satietySystem.DecreaseSatiety(_movePattern.MoveCost);
        }

        if (cell.ContainsBonus())
        {
            ProcessBuff(cell.GetBonus());
            cell.ClearBonus();
        }
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