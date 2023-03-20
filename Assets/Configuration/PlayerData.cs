using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] public Cell currentCell;
    [SerializeField] public Vector2 position;
    [SerializeField] public int satiety;
}