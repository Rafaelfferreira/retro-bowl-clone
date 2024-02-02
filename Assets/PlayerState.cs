using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    private string animBoolName;

    // MARK: - Initializer
    public PlayerState(string _animBoolName)
    {
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
