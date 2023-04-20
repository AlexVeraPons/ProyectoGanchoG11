using System;
using UnityEngine;

public class Grounded : State
{
    private float _currentVelocity;
    private float _speed;
    private float _acceleration;
    private float _deceleration;
    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    private bool _isGrounded => _collisionDetector.IsGrounded();
    private float _input => ((PlayerStateMachine)_stateMachine).DirectionalInput;

    public Grounded(StateMachine stateMachine)
        : base(stateMachine)
    {
        _speed = ((PlayerStateMachine)stateMachine)._speed;
        _acceleration = ((PlayerStateMachine)stateMachine)._acceleration;
        _deceleration = ((PlayerStateMachine)stateMachine)._deceleration;
        _rigidbody2D = ((PlayerStateMachine)stateMachine)._rigidbody2D;
        _collisionDetector = ((PlayerStateMachine)stateMachine)._collisionDetector;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        GroundMovementUpate();
    }

    private void GroundMovementUpate()
    {
        _currentVelocity = Mathf.Lerp(
            _currentVelocity,
            _input * _speed,
            _acceleration * Time.deltaTime
        );

        if (Mathf.Abs(_input) < 0.1f)
        {
            _currentVelocity = Mathf.Lerp(_currentVelocity, 0, _deceleration * Time.deltaTime);
        }

        _rigidbody2D.velocity = new Vector2(_currentVelocity, _rigidbody2D.velocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ExitLogicUpdate();
    }

    public override void ExitLogicUpdate()
    {
        base.ExitLogicUpdate();
        if (!_isGrounded)
        {
            _stateMachine.ChangeState(new Airborn(_stateMachine));
        }
    }
}
