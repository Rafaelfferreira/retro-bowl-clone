using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBIdleState : QbState
{
    public QBIdleState(QbController _qb, Animator _anim, string _animBoolName) : base(_qb,_anim, _animBoolName)
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
