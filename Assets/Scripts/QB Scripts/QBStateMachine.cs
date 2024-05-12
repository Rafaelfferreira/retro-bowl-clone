using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBStateMachine : StateMachine
{
    protected QbController qb;
    // MARK: - States
    #region States
    public PlayerPreSnapState preSnapState { get; private set; }
    public QBDropBackState dropBackState { get; private set; }
    public QBAimingState aimingState { get; private set; }
    public QBIdleState idleState { get; private set; }
    #endregion

    public QBStateMachine(QbController _qb, Animator anim) : base(anim)
    {
        qb = _qb;
        preSnapState = new PlayerPreSnapState(anim, "IsPreSnap");
        dropBackState = new QBDropBackState(qb, anim, "IsDroppingBack");
        aimingState = new QBAimingState(qb, anim, "IsAiming");
        idleState = new QBIdleState(qb, anim, "IsIdle");

        base.Awake();
    }
}
