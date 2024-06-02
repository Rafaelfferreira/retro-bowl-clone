using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public StateMachineState currentState;
    public Animator anim;

    public StateMachine(StateMachineState currentState)
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

    public void Initialize(StateMachineState _initialState)
    {
        currentState = _initialState;
        currentState.Enter();
    }

    public void ChangeState(StateMachineState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
