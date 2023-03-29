using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public readonly MouseClickEvent MouseClicked = new MouseClickEvent();

    private void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (IsMouseInput())
        {
            var screenPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            var clicked = GetClicked(screenPosition);
            if (clicked)
            { 
                MouseClicked.Invoke(clicked);
            }
        }
    }
    
    private static bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0);
    }
    
    private Collider2D GetClicked(Vector2 clickPosition)
    {
        var direction = Vector2.zero;
        var hit = Physics2D.Raycast(clickPosition, direction);
        if (hit)
        {
            return hit.collider;
        }

        return null;
    }
}