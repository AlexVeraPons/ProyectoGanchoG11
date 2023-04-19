using UnityEngine;
using UnityEngine.InputSystem;

public class HookDirection : MonoBehaviour
{
    private static Mover _mover;
    private Vector2 _input;
    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }
    public enum Direction
    {
        up,
        upRight,
        right,
        downRight,
        down,
        downLeft,
        left,
        upLeft
    }

    public Vector2 GetDirectionVector()
    {
        switch (GetDirection())
        {
            case Direction.up:
                return Vector2.up;
            case Direction.upRight:
                return new Vector2(1, 1);
            case Direction.right:
                return Vector2.right;
            case Direction.downRight:
                return new Vector2(1, -1);
            case Direction.down:
                return Vector2.down;
            case Direction.downLeft:
                return new Vector2(-1, -1);
            case Direction.left:
                return Vector2.left;
            case Direction.upLeft:
                return new Vector2(-1, 1);
            default:
                return Vector2.zero;
        }
    }
private Direction GetDirection()
{
    // If the _mover object is facing right.
    if (_mover.IsFacingRight)
    {
        // If moving up and right.
        if (_input.y > 0 && _input.x > 0)
        {
            return Direction.upRight;
        }
        // If moving down and right.
        else if (_input.y < 0 && _input.x > 0)
        {
            return Direction.downRight;
        }
        // If moving up only.
        else if (_input.y > 0)
        {
            return Direction.up;
        }
        // If moving down only.
        else if (_input.y < 0)
        {
            return Direction.down;
        }
        // If moving right only.
        else
        {
            return Direction.right;
        }
    }
    // If the _mover object is facing left.
    else
    {
        // If moving up and left.
        if (_input.y > 0 && _input.x < 0)
        {
            return Direction.upLeft;
        }
        // If moving down and left.
        else if (_input.y < 0 && _input.x < 0)
        {
            return Direction.downLeft;
        }
        // If moving up only.
        else if (_input.y > 0)
        {
            return Direction.up;
        }
        // If moving down only.
        else if (_input.y < 0)
        {
            return Direction.down;
        }
        // If moving left only.
        else
        {
            return Direction.left;
        }
    }
}

    public void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    public void DirectionalInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

}
