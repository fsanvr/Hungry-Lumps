using JetBrains.Annotations;
using UnityEngine;

public class InputSystem : BlockableBehaviour
{
    public readonly MouseClickEvent MouseClicked = new MouseClickEvent();
    public readonly MouseClickEvent MouseReleased = new MouseClickEvent();
    private const string MouseKeyName = "MouseLeftButton";

    private static bool IsMouseClicked()
    {
        return Input.GetButtonDown(MouseKeyName);
    }

    private static bool IsMouseReleased()
    {
        return Input.GetButtonUp(MouseKeyName);
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    private void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (IsBlocked() || Camera.main is null)
        {
            return;
        }

        var screenPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        
        if (IsMouseClicked())
        {
            var clicked = GetClicked(screenPosition);
            if (clicked)
            { 
                MouseClicked.Invoke(clicked);
            }
        }

        if (IsMouseReleased())
        {
            var clicked = GetClicked(screenPosition);
            MouseReleased.Invoke(clicked);
        }
    }
    
    [CanBeNull]
    private Collider2D GetClicked(Vector2 clickPosition)
    {
        var direction = Vector2.zero;
        var hit = Physics2D.Raycast(clickPosition, direction);
        return hit? hit.collider : null;
    }
}