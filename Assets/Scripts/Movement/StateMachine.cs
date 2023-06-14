using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public virtual State CurrentState { get;  set; }

    public virtual  void Initialize(State startingState)
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
        CurrentState.FixedUpdate();
    }
    public virtual void Update()
    {
        CurrentState.Update();
    }

    public virtual void LateUpdate()
    {
        CurrentState.LateUpdate();
    }
 
}


