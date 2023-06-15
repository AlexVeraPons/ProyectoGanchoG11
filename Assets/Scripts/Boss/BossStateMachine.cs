using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    [SerializeField]
    private Sprite[] _textures = new Sprite[4];
    private Sprite _currentTexture;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public Animator AnimatorComponent => _animator;

    [SerializeField]
    GameObject _particle;

    [SerializeField]
    private BackgroundSpawner _cannons;

    [SerializeField]
    private AnimateFondo _background;

    private void OnEnable()
    {
        NextWaveOnHit.BossTired += EnterIdleState;
        NextWaveOnHit.BossHit += BossHit;
        LifeComponent.OnDeath += ResetBoss;
    }

    private void OnDisable()
    {
        NextWaveOnHit.BossTired -= EnterIdleState;
        NextWaveOnHit.BossHit -= BossHit;
        LifeComponent.OnDeath -= ResetBoss;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        var spriterenderTarget = GetComponentInChildren<AnimationBody>();
        _spriteRenderer = spriterenderTarget.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentTexture = _textures[0];
        _spriteRenderer.sprite = _currentTexture;
        _animator.SetBool("isTired", true);

        Initialize(new BossInitialState(this));
    }

    public void SetCurrentTexture(int index)
    {
        _currentTexture = _textures[index];
        _spriteRenderer.sprite = _currentTexture;
    }

    public void SetRoomToBossState() {
        _background.ChangeToBossBackground();
        _cannons.EnterBossStage();
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    private void ResetBoss()
    {
        _animator.SetTrigger("restart");
        ChangeState(new BossInitialState(this));
    }

    private void EnterIdleState()
    {
        // first we create a new state as boss state that is a copy of the current state
        BossState bossState = (BossState)CurrentState;

        // then get the next state
        BossState nextState = bossState.NextState;

        // then createthe idle state with the next state as the state to change into
        BossIdleState idleState = new BossIdleState(this, nextState);
        CurrentState = idleState;

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

    private void BossHit()
    {
        _animator.SetTrigger("isHit");
        AudioManager._instance.PlaySingleSound(SingleSound.MissileCrash);
        Instantiate(_particle, this.transform.position, this.transform.rotation);
        ExitIdleState();
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
    private BossStateMachine _bossStateMachine;

    public BossIdleState(StateMachine stateMachine, BossState stateToChangeInto)
        : base(stateMachine)
    {
        _bossStateMachine = (BossStateMachine)stateMachine;
        NextState = stateToChangeInto;
    }

    internal void Change()
    {
        OnCollected?.Invoke();
        _stateMachine.ChangeState(NextState);
    }

    internal void Exit()
    {
        _bossStateMachine.AnimatorComponent.SetBool("isTired", false);
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
        NextState = new BossDoneState(_stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        _stateMachine.GetComponent<BossStateMachine>().SetCurrentTexture(3);
    }
}

public class BossDoneState : BossState
{
    private BossStateMachine _bossStateMachine;

    public BossDoneState(StateMachine stateMachine)
        : base(stateMachine)
    {
        _bossStateMachine = (BossStateMachine)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _bossStateMachine.GetComponent<BossStateMachine>().AnimatorComponent.SetTrigger("isDead");
    }
}
