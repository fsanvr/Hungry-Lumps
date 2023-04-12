using UnityEngine;

public class BlockableBehaviour : MonoBehaviour
{
    [SerializeField] private bool isBlocked;

    public void Block()
    {
        isBlocked = true;
    }

    public void Unblock()
    {
        isBlocked = false;
    }

    public bool IsBlocked()
    {
        return isBlocked;
    }
}