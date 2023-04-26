using System;
using System.Linq;
using UnityEngine;

public class LineMovePattern : MovePattern
{
    private readonly float _moveDelayInSeconds;
    private float _currentDelay;
    
    private bool _isMouseClamped;
    private readonly int _maxSteps;

    public LineMovePattern(InputSystem inputSystem, GridSystem gridSystem, PlayerComponent playerComponent, MoveData data) 
        : base(inputSystem, gridSystem, playerComponent)
    {
        _moveDelayInSeconds = data.GetMoveDuration();
        MoveCost = data.GetMoveCost();
        _maxSteps = data.maxSteps;
        if (Input)
        {
            Input.MouseClicked.AddListener(OnMouseClicked);
            Input.MouseReleased.AddListener(OnMouseReleased);
        }
    }
    
    ~LineMovePattern()
    {
        if (Input)
        {
            Input.MouseClicked.RemoveListener(OnMouseClicked);
            Input.MouseReleased.RemoveListener(OnMouseReleased);
        }
    }

    public override void Update()
    {
        ProcessDelay();
        ProcessLine();
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

        var screenPosition = Camera.main.ScreenPointToRay(Input.GetMousePosition()).origin;
        var cell = GetCell(screenPosition);
        if (cell && CellToMove.Count() != 0 && IsInOneLine(cell))
        {
            var cells = GridSystem.GetCellBetween(CellToMove.Last().position, cell.position);
            cells.Remove(cells.First());
            
            foreach (var currentCell in cells.TakeWhile(currentCell => !CellToMove.Contains(cell) &&
                                                                       currentCell.IsPassable() && 
                                                                       IsEnoughSteps()))
            {
                CellToMove.Add(currentCell);
            }
        }
    }

    private void OnMouseClicked(Collider2D clicked)
    {
        if (!IsCell(clicked.gameObject) || _currentDelay > 0.0f)
        {
            return;
        }
        
        var cell = clicked.GetComponent<Cell>();
        var playerCell = GetPlayerCell();
        
        CellToMove.Clear();
        CellToMove.Add(playerCell);
        _isMouseClamped = true;
        
        if (PlayerCanMove() && IsInOneLine(cell))
        {
            var cells = GridSystem.GetCellBetween(playerCell.position, cell.position);
            cells.Remove(cells.First());
            foreach (var currentCell in cells.TakeWhile(currentCell => currentCell.IsPassable() && IsEnoughSteps()))
            {
                CellToMove.Add(currentCell);
            }
        }
    }
    
    private void OnMouseReleased(Collider2D clicked)
    {
        _isMouseClamped = false;
        
        if (CellToMove.Count < 2)
        {
            return;
        }
        
        PlayerMoved.Invoke(CellToMove.Last());
        CellToMove.Clear();
        _currentDelay = _moveDelayInSeconds;
    }

    private bool PlayerCanMove()
    {
        return _currentDelay <= 0.0f;
    }

    private bool IsInOneLine(Cell cell)
    {
        var epsilon = 0.01f;
        var isInX = CellToMove.Aggregate(
            true, 
            (current, currentCell) => current & Math.Abs(cell.position.x - currentCell.position.x) < epsilon
        );
        
        var isInY = CellToMove.Aggregate(
            true, 
            (current, currentCell) => current & Math.Abs(cell.position.y - currentCell.position.y) < epsilon
        );

        return isInX ^ isInY;
    }

    private bool IsEnoughSteps()
    {
        return CellToMove.Count - 1 < _maxSteps;
    }
}