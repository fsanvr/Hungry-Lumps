using UnityEngine;

public class PlayerInitialization : MonoBehaviour
{
    private void Start()
    {
        var map = InitData.GetFoodMap(0);
        var playerSpawnPoint = new Vector3(map.StartCell.x, map.StartCell.y, 0);
        var player = GameObject.FindWithTag("Player");
        player.transform.position = playerSpawnPoint;
    }
}