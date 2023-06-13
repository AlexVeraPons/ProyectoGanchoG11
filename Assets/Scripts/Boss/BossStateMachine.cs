using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    [SerializeField] private Sprite[] _textures = new Sprite[5];
    private Sprite _currentTexture;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

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
    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }


    private void Start()
    {
        _currentTexture = _textures[0];
        _spriteRenderer.sprite = _currentTexture;
        _animator.SetBool("isTired", false);

        Initialize(new BossInitialState(this));
    }

    public void SetCurrentTexture(int index)
    {
        _currentTexture = _textures[index];
        _spriteRenderer.sprite = _currentTexture;
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

        _animator.SetBool("isTired", true);
    }

    private void ExitIdleState()
    {
        if (CurrentState is BossIdleState)
        {
            BossIdleState idleState = (BossIdleState)CurrentState;
            idleState.Change();
        }

        _animator.SetBool("isTired", false);
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

    public override void Enter()
    {
        base.Enter();
        _stateMachine.GetComponent<BossStateMachine>().SetCurrentTexture(0);
    }
}

public class BossSecondState : BossState
{
    public BossSecondState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossThirdState(_stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        _stateMachine.GetComponent<BossStateMachine>().SetCurrentTexture(1);
    }
}

public class BossThirdState : BossState
{
    public BossThirdState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossFourthState(_stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        _stateMachine.GetComponent<BossStateMachine>().SetCurrentTexture(2);
    }
}

public class BossFourthState : BossState
{
    public BossFourthState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossFifthState(_stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        _stateMachine.GetComponent<BossStateMachine>().SetCurrentTexture(3);
    }
}

public class BossFifthState : BossState
{
    public BossFifthState(StateMachine stateMachine)
        : base(stateMachine)
    {
        NextState = new BossDoneState(_stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        _stateMachine.GetComponent<BossStateMachine>().SetCurrentTexture(4);
    }
}

public class BossDoneState : BossState
{
    public BossDoneState(StateMachine stateMachine)
        : base(stateMachine) { }
}
