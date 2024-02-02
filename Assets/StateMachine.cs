using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public PlayerState currentState;
    public Animator anim;

    public StateMachine(PlayerState currentState)
    {
        this.currentState = currentState;
    }

    public StateMachine(Animator anim)
    {
        this.anim = anim;
    }

    public virtual void Awake()
    {
    }

    public void Initialize(PlayerState _initialState)
    {
        currentState = _initialState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
