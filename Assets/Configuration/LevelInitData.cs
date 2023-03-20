using UnityEngine;

[CreateAssetMenu]
public class LevelInitData : ScriptableObject
{
    [Header("Game Field")]
    [SerializeField] public Vector2Int gameFieldShape;
    [SerializeField] public GameObject cellPrefab;
    
    [Header("Player")]
    [SerializeField] public Vector2Int playerSpawnPoint;
    [SerializeField] public int minSatiety;
    [SerializeField] public int maxSatiety;
    [SerializeField] public int startSatiety;
    [SerializeField] public int costOfMove;
}