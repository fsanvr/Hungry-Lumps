using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData data;

    public void MoveTo(Vector2 position)
    {
        this.transform.position = position;
        data.position = position;
    }
}