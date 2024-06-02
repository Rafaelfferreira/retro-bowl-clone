using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState: StateMachineState
{
    protected Animator anim;
    protected string animBoolName;

    // MARK: - Initializer
    public PlayerState(Animator _anim, string _animBoolName)
    {
        anim = _anim;
        animBoolName = _animBoolName;
    }

    // MARK: - Life cycle methods
    public override void Enter()
    {
        base.Enter();
        anim.SetBool(animBoolName, true);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool(animBoolName, false);
    }
}
