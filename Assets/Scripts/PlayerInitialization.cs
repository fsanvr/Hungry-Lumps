using UnityEngine;

public class PlayerInitialization : MonoBehaviour
{
    public Configuration configuration;
    public LevelInitData levelInitData;

    private void Start()
    {
        var playerSpawnPoint = new Vector3(levelInitData.playerSpawnPoint.x, levelInitData.playerSpawnPoint.y, 0);
        var player = Instantiate(configuration.currentPlayerPrefab, playerSpawnPoint, Quaternion.identity);
    }
}