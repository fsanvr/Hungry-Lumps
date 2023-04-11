using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public GenerateGrid Grid;
    public Pet Pet;
    public GameObject CellPrefab;
    public Sprite Background;
}