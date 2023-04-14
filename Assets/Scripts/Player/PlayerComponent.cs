using UnityEngine;

public class PlayerComponent : InitializableBehaviour
{
    private Pet _pet;
    
    protected override void MyInit(LevelData data)
    {
        var startPosition = data.Grid.StartPosition;
        var playerSpawnPoint = new Vector3(startPosition.x, startPosition.y, 0);
        transform.position = playerSpawnPoint;
        _pet = data.pet; //TODO: use it
    }
}