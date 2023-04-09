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
            var menuComponent = child.GetComponent<MenuView>();
            if (menuComponent)
            {
                _views.Add(child.gameObject);
                child.gameObject.SetActive(menuComponent.isBase);
            }
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
