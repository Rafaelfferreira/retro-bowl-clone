using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBDropBackState : QbState
{
    public QBDropBackState(QbController _qb, Animator _anim, string _animBoolName) : base(_qb,_anim, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.UpdateGameState(GameState.QBWithBall);
    }
}
