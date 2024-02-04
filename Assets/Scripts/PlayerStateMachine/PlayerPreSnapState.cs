using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreSnapState : PlayerState
{
    public PlayerPreSnapState(Animator _anim, string _animBoolName) : base(_anim, _animBoolName)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        anim.SetBool("IsPreSnap", true);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("IsPreSnap", false);
    }
}
