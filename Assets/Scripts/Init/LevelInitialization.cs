using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    
    [SerializeField] private GameField gameField;
    [SerializeField] private PlayerComponent player;
    private void Start()
    {
        InitPlayer();
        InitGameField();
    }

    private void InitPlayer()
    {
        var grid = levelData.Grid;
        var playerSpawnPoint = new Vector3(grid.StartPosition.x, grid.StartPosition.y, 0);
        player.transform.position = playerSpawnPoint;
        player.Init(levelData);
    }

    private void InitGameField()
    {
        gameField.InitField(levelData);
    }
}