using UnityEngine;

public class LevelInitialization : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    
    [SerializeField] private GameField gameField;
    private void Start()
    {
        InitPlayer();
        InitGameField();
    }

    private void InitPlayer()
    {
        var map = levelData.EnabledLevelMap;
        var playerSpawnPoint = new Vector3(map.StartCell.x, map.StartCell.y, 0);
        var player = GameObject.FindWithTag("Player");
        player.transform.position = playerSpawnPoint;
    }

    private void InitGameField()
    {
        gameField.InitField(levelData.EnabledLevelMap);
    }
}