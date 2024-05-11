using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBDropBackState : QbState
{
    public QBDropBackState(Animator _anim, string _animBoolName) : base(_anim, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool(animBoolName, true);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool(animBoolName, false);
    }

    public override void Update()
    {
        base.Update();
    }
}
