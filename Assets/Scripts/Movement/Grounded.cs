using System;
using UnityEngine;

public class Grounded : State
{
    private LifeComponent _life;
    private float _currentVelocity;
    private float _speed;
    private float _acceleration;
    private float _deceleration;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private CollisionDetector _collisionDetector;
    private bool _isGrounded => _collisionDetector.IsGrounded();
    private float _input => ((PlayerStateMachine)_stateMachine).DirectionalInput();

    public Grounded(StateMachine stateMachine)
        : base(stateMachine)
    {
        _life = ((PlayerStateMachine)stateMachine).LifeComponent;
        _speed = ((PlayerStateMachine)stateMachine).Speed;
        _acceleration = ((PlayerStateMachine)stateMachine).Acceleration;
        _deceleration = ((PlayerStateMachine)stateMachine).Deceleration;
        _rigidbody2D = ((PlayerStateMachine)stateMachine).RigidBody2D;
        _collisionDetector = ((PlayerStateMachine)stateMachine).CollisionDetector;
        _animator = ((PlayerStateMachine)stateMachine).Animator;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
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

    public override void Update()
    {
        base.Update();
        _animator.SetBool("isGrounded", true);

        if(Mathf.Abs(_input) < 0.1f)
        {
            _animator.SetBool("isIdle", true);
        }
        else
        {
            _animator.SetBool("isIdle", false);
        }

        ExitLogicUpdate();
    }

    public override void ExitLogicUpdate()
    {
        base.ExitLogicUpdate();
        if (!_isGrounded)
        {
            _animator.SetBool("isGrounded", false);
            _stateMachine.ChangeState(new Airborn(_stateMachine));
        }
    }
}
