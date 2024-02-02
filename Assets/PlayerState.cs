using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
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
    public virtual void Enter()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {

    }
}
