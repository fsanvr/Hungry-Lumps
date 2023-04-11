using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] private InputSystem input;
    private GameField _gameField;
    private PlayerComponent _playerComponent;

    [SerializeField] private float moveDelayInSeconds = 3.5f;
    private float _currentDelay = 0.0f;
    
    private void Start()
    {
        input.MouseClicked.AddListener(OnMouseClicked);
        _gameField = GameObject.Find("GameField").GetComponent<GameField>();
        _playerComponent = this.GetComponent<PlayerComponent>();
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
            if (_gameField.IsNeighbour(_playerComponent.transform.position, cell) &&
                PlayerCanMove())
            {
                MovePlayerTo(cell);
            }
        }
    }
    
    private bool IsCell(GameObject go)
    {
        return go.GetComponent<Cell>() != null;
    }
    
    private bool PlayerCanMove()
    {
        return _currentDelay <= 0.0f;
    }

    private void MovePlayerTo(Cell cell)
    {
        _currentDelay = moveDelayInSeconds;
        _playerComponent.MoveTo(cell);
    }
}