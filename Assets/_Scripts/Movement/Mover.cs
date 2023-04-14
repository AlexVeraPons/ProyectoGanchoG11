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

    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    private bool _isGrounded => _collisionDetector.IsGrounded();
    private float _input;
    private float _velocity;

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

    public void DirectionalInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>().x;
    }

    private bool CanMove()
    {
        return _isGrounded;
    }
}
