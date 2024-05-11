using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QbState : PlayerState
{
    public QbState(Animator _anim, string _animBoolName) : base(_anim, _animBoolName)
    {
        anim = _anim;
        animBoolName = _animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
