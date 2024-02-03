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
        Debug.Log("ENTER DO PRE SNAP STATE");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Debug.Log("UPDATE DO PRE SNAP STATE");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DO PRE SNAP STATE");
        anim.SetBool("IsPreSnap", false);
    }
}
