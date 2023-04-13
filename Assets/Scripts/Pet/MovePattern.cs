using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class MovePattern
{
    public readonly PlayerMoveEvent PlayerMoved = new PlayerMoveEvent();
    private readonly InputSystem _input;
    private readonly SatietySystem _satietySystem;
    private GridSystem _gridSystem;
    private PlayerComponent _playerComponent;
    
    private float _moveDelayInSeconds = 0.2f;
    private float _currentDelay = 0.0f;

    public float MoveCost { get; private set; } = 5;
    
    private readonly List<Cell> _cells = new List<Cell>();
    private bool _isMouseClamped;
    private int _maxSteps = 2;

    public MovePattern(InputSystem inputSystem, SatietySystem satietySystem, GridSystem gridSystem, PlayerComponent playerComponent)
    {
        _input = inputSystem;
        _satietySystem = satietySystem;
        _gridSystem = gridSystem;
        _playerComponent = playerComponent;
        if (_input)
        {
            _input.MouseClicked.AddListener(OnMouseClicked);
            _input.MouseReleased.AddListener(OnMouseReleased);
        }
    }
    
    ~MovePattern()
    {
        if (_input)
        {
            _input.MouseClicked.RemoveListener(OnMouseClicked);
            _input.MouseReleased.RemoveListener(OnMouseReleased);
        }
    }

    public void Update()
    {
        ProcessDelay();
        ProcessLine();
    }
    
    public List<Cell> GetCellToDraw()
    {
        return _cells;
    }
    
    private void ProcessDelay()
    {
        if (_currentDelay >= 0.0f)
        {
            _currentDelay -= Time.deltaTime;
        }
    }
    
    private void ProcessLine()
    {
        if (!_isMouseClamped)
        {
            return;
        }

        var screenPosition = Camera.main.ScreenPointToRay(_input.GetMousePosition()).origin;
        var cell = GetCell(screenPosition);
        if (cell && !_cells.Contains(cell) && IsInOneLine(cell) && IsEnoughSteps())
        {
            _cells.Add(cell);
        }
    }
    
    [CanBeNull]
    private Cell GetCell(Vector2 clickPosition)
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

    private void OnMouseClicked(Collider2D clicked)
    {
        if (!IsCell(clicked.gameObject))
        {
            return;
        }
        
        _cells.Clear();
        var cell = clicked.GetComponent<Cell>();
        var playerCell = _gridSystem.GetCell(_playerComponent.transform.position);
        _cells.Add(playerCell);
        _isMouseClamped = true;
        
        if (PlayerCanMove() && IsInOneLine(cell))
        {
            _cells.Clear();
            var cells = _gridSystem.GetCellBetween(playerCell.position, cell.position);
            Debug.Log(cells.Count);
            foreach (var currentCell in cells.TakeWhile(currentCell => currentCell.IsPassable() && IsEnoughSteps()))
            {
                _cells.Add(currentCell);
            }
        }
    }
    
    private void OnMouseReleased(Collider2D clicked)
    {
        if (_cells.Count < 2)
        {
            return;
        }
        PlayerMoved.Invoke(_cells.Last());
        _cells.Clear();
        _isMouseClamped = false;
        _currentDelay = _moveDelayInSeconds;
    }
    
    private static bool IsCell(GameObject go)
    {
        return go.GetComponent<Cell>() != null;
    }
    
    private bool PlayerCanMove()
    {
        return _currentDelay <= 0.0f && _satietySystem.CurrentSatiety >= MoveCost;
    }

    private bool IsInOneLine(Cell cell)
    {
        var epsilon = 0.01f;
        var isInX = _cells.Aggregate(
            true, 
            (current, currentCell) => current & Math.Abs(cell.position.x - currentCell.position.x) < epsilon
        );
        
        var isInY = _cells.Aggregate(
            true, 
            (current, currentCell) => current & Math.Abs(cell.position.y - currentCell.position.y) < epsilon
        );

        return isInX ^ isInY;
    }

    private bool IsEnoughSteps()
    {
        return _cells.Count - 1 < _maxSteps;
    }
}