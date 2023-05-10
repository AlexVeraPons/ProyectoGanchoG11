using System;
using System.Collections;
using UnityEngine;

public class Airborn : State
{
    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    private bool _isTouchingWall => _collisionDetector.IsTouchingInfront();
    private bool _isGrounded => _collisionDetector.IsGrounded();

    private LifeComponent _life;
    private float _currentVelocity;
    private float _speed;
    private float _acceleration;
    private float _deceleration;
    private float _airResistance;
    private float _timer = 0f;
    private bool _delayDone = false;
    private float _input => ((PlayerStateMachine)_stateMachine).DirectionalInput();

    public Airborn(StateMachine stateMachine)
        : base(stateMachine)
    {
        _rigidbody2D = ((PlayerStateMachine)stateMachine).RigidBody2D;
        _collisionDetector = ((PlayerStateMachine)stateMachine).CollisionDetector;
        _life = ((PlayerStateMachine)stateMachine).LifeComponent;
        _speed = ((PlayerStateMachine)stateMachine).Speed;
        _acceleration = ((PlayerStateMachine)stateMachine).Acceleration;
        _deceleration = ((PlayerStateMachine)stateMachine).Deceleration;
        _airResistance = ((PlayerStateMachine)stateMachine).AirResistance;
        _currentVelocity = _rigidbody2D.velocity.x;
    }

    public override void Enter()
    {
        base.Enter();

    }

    private void Delay()
    {
        if (_delayDone)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= 0.02f)
        {
            _delayDone = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        ExitLogicUpdate();
        Delay();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        MovementUpdate();
        WallCollisionUpdate();
    }

    private void WallCollisionUpdate()
    {
        if (_isTouchingWall)
        {
            ResetVelocity();
        }
    }

    private void ResetVelocity()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }

    private void MovementUpdate()
    {
        if (_delayDone == false)
        {
            return;
        }

        bool Hasinputted = false;

        if (Mathf.Abs(_input) > 0.1f)
        {
            _currentVelocity = Mathf.Lerp(
                _currentVelocity,
                _input * _speed,
                _acceleration * Time.deltaTime
            );

            Hasinputted = true;
            ResetTimer();
        }

        if (Hasinputted == false)
        {
            return;
        }

        _rigidbody2D.velocity = new Vector2(
            _currentVelocity / _airResistance,
            _rigidbody2D.velocity.y
        );
    }

    private void ResetTimer()
    {
        _timer = 0f;
        _delayDone = false;
    }

    public override void ExitLogicUpdate()
    {
        base.ExitLogicUpdate();
        if (_isGrounded)
        {
            AudioManager._instance.PlaySingleSound(SingleSound.PlayerGround);
            _stateMachine.ChangeState(new Grounded(_stateMachine));
        }
    }
}
