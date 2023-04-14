using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class MovePattern
{
    public PlayerMoveEvent PlayerMoved { get; } = new PlayerMoveEvent();
    public List<Cell> CellToMove { get; } = new List<Cell>();
    public float MoveCost { get; protected set;  } = 3;

    protected readonly InputSystem Input;
    protected readonly GridSystem GridSystem;
    
    private readonly PlayerComponent _playerComponent;
    
    public abstract void Update();

    protected MovePattern(
        InputSystem inputSystem,
        GridSystem gridSystem,
        PlayerComponent playerComponent
    )
    {
        Input = inputSystem;
        GridSystem = gridSystem;
        _playerComponent = playerComponent;
    }
    
    protected static bool IsCell(GameObject go)
    {
        return go.GetComponent<Cell>() != null;
    }
    
    protected Cell GetPlayerCell()
    {
        return GridSystem.GetCell(_playerComponent.transform.position);
    }
    
    [CanBeNull]
    protected Cell GetCell(Vector2 clickPosition)
    {
        var collider = Raycast(clickPosition);
        return collider && IsCell(collider.gameObject) ? collider.GetComponent<Cell>() : null;
    }
    
    private Collider2D Raycast(Vector2 clickPosition)
    {
        var direction = Vector2.zero;
        var hit = Physics2D.Raycast(clickPosition, direction);
        return hit? hit.collider : null;
    }
}