using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public virtual State CurrentState { get; private set; }

    public virtual  void dw(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public  virtual void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public virtual  void FixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }
    public virtual void Update()
    {
        CurrentState.LogicUpdate();
    }

}


