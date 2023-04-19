using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }
}


public class PlayerStateMachine : StateMachine
{
    public float _speed { get; private set; }
    public float _acceleration { get; private set; }

    public float _deceleration { get; private set; }

    public Rigidbody2D _rigidbody2D { get; private set; }


    private void OnEnable() {
        
    }
    private void Start() 
    {
        Initialize(new Grounded(this));
    }

    
}
