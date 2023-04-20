using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    public float DirectionalInput => _directionalInput;
    private float _directionalInput;

    public bool FacingRight => _facingRight;
    private bool _facingRight = true;

    public float _speed { get; private set; }
    public float _acceleration { get; private set; }

    public float _deceleration { get; private set; }

    public Rigidbody2D _rigidbody2D { get; private set; }

    public CollisionDetector _collisionDetection { get; private set; }

    private void Start()
    {
        Initialize(new Grounded(this));

    }

    public override void Update()
    {
        base.Update();
        FlipLogic();
    }

    private void FlipLogic()
    {
         // If the input is moving the player right and the player is facing left...
        if (_directionalInput > 0 && !_facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (_directionalInput < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    

    private void OnMove(InputAction.CallbackContext context)
    {
        _directionalInput = context.ReadValue<Vector2>().x;
    }
}
