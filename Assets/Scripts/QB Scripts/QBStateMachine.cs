using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBStateMachine : StateMachine
{
    // MARK: - States
    #region States
    public PlayerPreSnapState preSnapState { get; private set; }
    public QBDropBackState dropBackState { get; private set; }
    public QBAimingState aimingState { get; private set; }
    public QBIdleState idleState { get; private set; }
    #endregion

    public QBStateMachine(Animator anim) : base(anim)
    {
        preSnapState = new PlayerPreSnapState(anim, "IsPreSnap");
        dropBackState = new QBDropBackState(anim, "IsDroppingBack");
        aimingState = new QBAimingState(anim, "IsAiming");
        idleState = new QBIdleState(anim, "IsIdle");

        base.Awake();
    }
}
