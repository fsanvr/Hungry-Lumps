using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    private GameField _gameField;
    private Player _player;

    private void Start()
    {
        _gameField = GameObject.Find("GameField").GetComponent<GameField>();
        _player = this.GetComponent<Player>();
    }

    private void Update()
    {
        ProcessMove();
    }

    private void ProcessMove()
    {
        if (IsMouseInput())
        {
            var screenPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            var clicked = GetClickedObject(screenPosition);
            if (clicked && IsCell(clicked))
            {
                var cell = clicked.GetComponent<Cell>();
                if (_gameField.IsNeighbour(_player.transform.position, cell))
                {
                    MovePlayerTo(cell);
                }
            }
        }
    }

    private bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    private GameObject GetClickedObject(Vector2 clickPosition)
    {
        var direction = Vector2.zero;
        var hit = Physics2D.Raycast(clickPosition, direction);
        if (hit)
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    private bool IsCell(GameObject go)
    {
        return go.GetComponent<Cell>() != null;
    }

    private void MovePlayerTo(Cell cell)
    {
        _player.MoveTo(cell);
    }
}