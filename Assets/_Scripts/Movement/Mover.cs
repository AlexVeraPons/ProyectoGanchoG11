using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _acceleration = 0.1f;

    [SerializeField]
    private float _deceleration = 0.1f;
    
    [SerializeField]
    private MoveToPosition _moveToPosition;
    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    public bool IsFacingRight => _facingRight;
    private bool _facingRight = true;
    private bool _isGrounded => _collisionDetector.IsGrounded();
    private float _input;
    private float _velocity;
    private bool _isHooked = false;
    private bool _isTouchingWall => _collisionDetector.IsTouchingInfront();

    private void OnEnable() {
        _moveToPosition.OnOverridingMovement += SetIsHooked;
        _moveToPosition.OnStopOverridingMovement += UnsetIsHooked;
    }

    private void OnDisable() {
        _moveToPosition.OnOverridingMovement -= SetIsHooked;
        _moveToPosition.OnStopOverridingMovement -= UnsetIsHooked;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    private void FixedUpdate()
    {
        if (CanMove())
        {
            MovementUpdate();
        }

        ShouldFlip();

        if (_isTouchingWall && !_isGrounded)
        {
            ResetVelocity();
        }
    }

    private void MovementUpdate()
    {
        _velocity = Mathf.Lerp(_velocity, _input * _speed, _acceleration * Time.deltaTime);

        if (Mathf.Abs(_input) < 0.1f)
        {
            _velocity = Mathf.Lerp(_velocity, 0, _deceleration * Time.deltaTime);
        }

        _rigidbody2D.velocity = new Vector2(_velocity, _rigidbody2D.velocity.y);
    }

    private void ShouldFlip()
    {
        // If the input is moving the player right and the player is facing left...
        if (_velocity > 0 && !_facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (_velocity < 0 && _facingRight)
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

    private void ResetVelocity()
    {
        _velocity = 0;
    }

    private bool CanMove()
    {
        return _isGrounded && !_isHooked;
    }

    public void DirectionalInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>().x;
    }

    private void SetIsHooked()
    {
        _isHooked = true;
    }
    private void UnsetIsHooked()
    {
        _isHooked = false;
    }

}
