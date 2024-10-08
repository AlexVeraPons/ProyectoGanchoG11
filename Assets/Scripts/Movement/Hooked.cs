using UnityEngine;

public class Hooked : State
{
    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    private float _originalGravityScale;
    private bool _isHooked;

    private bool _isGrounded => _collisionDetector.IsGrounded();
    public Hooked(StateMachine stateMachine) : base(stateMachine)
    {
        _rigidbody2D = ((PlayerStateMachine)stateMachine).RigidBody2D;
        _collisionDetector = ((PlayerStateMachine)stateMachine).CollisionDetector;
    }

    public override void Enter()
    {
        base.Enter();
        _originalGravityScale = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0;
        _isHooked = true;
    }

    public override void Exit()
    {
        base.Exit();
        _rigidbody2D.gravityScale = _originalGravityScale;
    }

    public override void Update()
    {
        base.Update();
        if(_isHooked == false)
        {
            ExitLogicUpdate();
        }
    }

    public override void ExitLogicUpdate()
    {
        base.ExitLogicUpdate();

        
        if (_isGrounded)
        {
            _stateMachine.ChangeState(new Grounded(_stateMachine));
        }
        else
        {
            _stateMachine.ChangeState(new Airborn(_stateMachine));
        }
    }
}
