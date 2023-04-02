using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInputSystem : MonoBehaviour
{
    public readonly UIMouseClickEvent MouseClicked = new UIMouseClickEvent();
    private GraphicRaycaster _raycaster;
    private PointerEventData _eventData;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (IsMouseInput())
        {
            var results = Raycast();
            if (results.Count != 0)
            {
                var clicked = results[0].gameObject;
                MouseClicked.Invoke(clicked);
            }
        }
    }
    
    private static bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    private List<RaycastResult> Raycast()
    {
        _eventData = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };
        var results = new List<RaycastResult>();
        _raycaster.Raycast(_eventData, results);
        return results;
    }
}