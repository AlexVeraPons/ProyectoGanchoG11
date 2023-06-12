using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    private void OnEnable()
    {
        NextWaveOnHit.BossTired += EnterIdleState;
        NextWaveOnHit.BossHit += ExitIdleState;
    }

    private void OnDisable()
    {
        NextWaveOnHit.BossTired -= EnterIdleState;
        NextWaveOnHit.BossHit -= ExitIdleState;
    }

    private void Start()
    {
        Initialize(new BossInitialState(this));
    }

    private void EnterIdleState()
    {
        // first we create a new state as boss state that is a copy of the current state
        BossState bossState = (BossState)CurrentState;

        Debug.Log("EnterIdleState" + " From: " + bossState);

        // then get the next state
        BossState nextState = bossState.NextState;

        // then createthe idle state with the next state as the state to change into
        BossIdleState idleState = new BossIdleState(this, nextState);
        CurrentState  = idleState;
    }

    private void ExitIdleState()
    {
        Debug.Log("ExitIdleState" + CurrentState is BossIdleState);
        if (CurrentState is BossIdleState)
        {
            BossIdleState idleState = (BossIdleState)CurrentState;
            idleState.Change();
        }
    }
}

public class BossState : State
{
    public static Action OnCollected;

    public BossState NextState;

    public BossState(StateMachine stateMachine)
        : base(stateMachine) { }

    private void GoToIdleState()
    {
        _stateMachine.ChangeState(new BossIdleState(_stateMachine, NextState));
    }
}

public class BossIdleState : BossState
{
    BossState _stateToChangeInto;

    public BossIdleState(StateMachine stateMachine, BossState stateToChangeInto)
        : base(stateMachine)
    {
        _stateToChangeInto = stateToChangeInto;
    }

    internal void Change()
    {
        Debug.Log("Exit Idle State");
        OnCollected?.Invoke();
        _stateMachine.ChangeState(_stateToChangeInto);
    }
}

public class BossInitialState : BossState
{
    public BossInitialState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossSecondState(_stateMachine);
    }
}

public class BossSecondState : BossState
{
    public BossSecondState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossThirdState(_stateMachine);
    }
}

public class BossThirdState : BossState
{
    public BossThirdState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossFourthState(_stateMachine);
    }
}

public class BossFourthState : BossState
{
    public BossFourthState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossFifthState(_stateMachine);
    }
}

public class BossFifthState : BossState
{
    public BossFifthState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossDoneState(_stateMachine);
    }
}

public class BossDoneState : BossState
{
    public BossDoneState(StateMachine stateMachine)
        : base(stateMachine) { }
}
