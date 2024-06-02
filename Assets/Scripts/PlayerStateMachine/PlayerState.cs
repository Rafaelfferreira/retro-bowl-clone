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
}
