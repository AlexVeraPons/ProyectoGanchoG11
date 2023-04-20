using UnityEngine;

public class Hooked : State
{
    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    private float _originalGravityScale;

    private bool _isGrounded => _collisionDetector.IsGrounded();
    public Hooked(StateMachine stateMachine) : base(stateMachine)
    {
        _rigidbody2D = ((PlayerStateMachine)stateMachine)._rigidbody2D;
        _collisionDetector = ((PlayerStateMachine)stateMachine)._collisionDetector;
    }

    public override void Enter()
    {
        base.Enter();
        _originalGravityScale = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        _rigidbody2D.gravityScale = _originalGravityScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ExitLogicUpdate();
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
