using UnityEngine;

[CreateAssetMenu]
public class LevelInitData : ScriptableObject
{
    public Vector2Int gameFieldShape;
    public Vector2Int playerSpawnPoint;
    public GameObject cellPrefab;
}