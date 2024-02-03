using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBStateMachine : StateMachine
{
    public QBStateMachine(Animator anim) : base(anim)
    {
        preSnapState = new PlayerPreSnapState(anim, "IsPreSnap");
        dropBackState = new QBDropBackState(anim, "IsDroppingBack");
        base.Awake();
    }

    // MARK: - States
    #region States
    public PlayerPreSnapState preSnapState { get; private set; }
    public QBDropBackState dropBackState { get; private set; }
    #endregion
}
