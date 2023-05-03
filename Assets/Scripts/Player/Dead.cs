using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{
    LifeComponent _life;

    public Dead(StateMachine stateMachine) : base(stateMachine)
    {
        _life = ((PlayerStateMachine)stateMachine).LifeComponent;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        ExitLogicUpdate();
    }

    public override void ExitLogicUpdate()
    {
        base.ExitLogicUpdate();
    }
}
