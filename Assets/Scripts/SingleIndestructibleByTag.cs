using UnityEngine;

public class SingleIndestructibleByTag : MonoBehaviour
{
    [SerializeField] private string _tag = "MainTheme";
    private void Awake()
    {
        var objects = GameObject.FindGameObjectsWithTag(_tag);
        if (objects.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}