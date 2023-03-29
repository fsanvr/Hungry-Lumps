using System;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] private InputSystem input;
    private GameField _gameField;
    private Player _player;

    private void Start()
    {
        input.MouseClicked.AddListener(OnMouseClicked);
        _gameField = GameObject.Find("GameField").GetComponent<GameField>();
        _player = this.GetComponent<Player>();
    }

    private void OnDestroy()
    {
        input.MouseClicked.RemoveListener(OnMouseClicked);
    }

    private void OnMouseClicked(Collider2D clicked)
    {
        if (IsCell(clicked.gameObject))
        {
            var cell = clicked.GetComponent<Cell>();
            if (_gameField.IsNeighbour(_player.transform.position, cell))
            {
                MovePlayerTo(cell);
            }
        }
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