using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject root;
    private List<GameObject> _views;
    private void Start()
    {
        _views = new List<GameObject>();
        
        foreach (Transform child in root.transform)
        {
            _views.Add(child.gameObject);
            child.gameObject.SetActive(child.GetComponent<MenuView>().isBase);
        }
    }

    public void ActiveOnly(GameObject targetView)
    {
        foreach (var view in _views)
        {
            view.SetActive(false);
        }
        targetView.SetActive(true);
    }
}
