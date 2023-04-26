using QuickGraph;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public GenerateGrid grid;
    public AdjacencyGraph<int, Edge<int>> graph;
    public Pet pet;
    
    public GameObject cellPrefab;
    public GameObject foodPrefab;
    public GameObject objectPrefab;
    public GameObject bonusPrefab;
    
    public SpritesData spritesData;
}