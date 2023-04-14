using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public GenerateGrid Grid;
    public Pet pet;
    public GameObject cellPrefab;
    public Sprite background;
}