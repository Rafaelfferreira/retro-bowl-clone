using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBDropBackState : PlayerState
{
    public QBDropBackState(Animator _anim, string _animBoolName) : base(_anim, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER DODROP BACK");
        Debug.Log("ANIM BOOL NAME: " + animBoolName);
        anim.SetBool(animBoolName, true);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DODROP BACK");
        anim.SetBool(animBoolName, false);
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("UPDATE DODROP BACK");
    }
}
