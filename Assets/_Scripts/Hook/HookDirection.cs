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
        if (_mover.IsFacingRight)
        {
            if (_input.y > 0 && _input.x > 0)
            {
                return Direction.upRight;
            }
            else if (_input.y < 0 && _input.x > 0)
            {
                return Direction.downRight;
            }
            else if (_input.y > 0)
            {
                return Direction.up;
            }
            else if (_input.y < 0)
            {
                return Direction.down;
            }
            else
            {
                return Direction.right;
            }

        }
        else
        {
            if (_input.y > 0 && _input.x < 0)
            {
                return Direction.upLeft;
            }
            else if (_input.y < 0 && _input.x < 0)
            {
                return Direction.downLeft;
            }
            else if (_input.y > 0)
            {
                return Direction.up;
            }
            else if (_input.y < 0)
            {
                return Direction.down;
            }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GetDirection());
        }
    }

    public void DirectionalInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

}
