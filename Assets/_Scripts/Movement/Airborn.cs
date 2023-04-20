using UnityEngine;

public class Airborn : State
{
    private Rigidbody2D _rigidbody2D;
    private CollisionDetector _collisionDetector;
    private bool _isTouchingWall => _collisionDetector.IsTouchingInfront();
    private bool _isGrounded => _collisionDetector.IsGrounded();


    public Airborn(StateMachine stateMachine)
        : base(stateMachine)
    {
        _rigidbody2D = ((PlayerStateMachine)stateMachine)._rigidbody2D;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ExitLogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
        // waiting for decision on how to handle airborn movement
    }

    public override void ExitLogicUpdate()
    {
        base.ExitLogicUpdate();
        if (_isGrounded)
        {
            _stateMachine.ChangeState(new Grounded(_stateMachine));
        }
        
    }
}
