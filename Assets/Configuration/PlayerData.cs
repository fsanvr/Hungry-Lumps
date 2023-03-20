using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] public int satiety;
    [SerializeField] public int costOfMove;
}