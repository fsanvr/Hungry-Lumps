using JetBrains.Annotations;
using UnityEngine;

public class InputSystem : BlockableBehaviour
{
    public readonly MouseClickEvent MouseClicked = new MouseClickEvent();
    public readonly MouseClickEvent MouseReleased = new MouseClickEvent();

    private bool _isMouseClamped;
    private const string MouseKeyName = "MouseLeftButton";

    public bool IsMouseClicked()
    {
        return Input.GetButtonDown(MouseKeyName);
    }

    public bool IsMouseClamped()
    {
        return _isMouseClamped;
    }
    
    public bool IsMouseReleased()
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
        if (IsBlocked())
        {
            return;
        }
        
        var screenPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        
        if (IsMouseClicked())
        {
            _isMouseClamped = true;
            var clicked = GetClicked(screenPosition);
            if (clicked)
            { 
                MouseClicked.Invoke(clicked);
            }
        }

        if (IsMouseReleased())
        {
            _isMouseClamped = false;
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