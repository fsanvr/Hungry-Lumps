using UnityEngine;

[CreateAssetMenu]
public class LevelInitData : ScriptableObject
{
    [SerializeField] public Vector2Int gameFieldShape;
    [SerializeField] public Vector2Int playerSpawnPoint;
    [SerializeField] public GameObject cellPrefab;
    [SerializeField] public int startSatiety;
    [SerializeField] public int costOfMove;
}