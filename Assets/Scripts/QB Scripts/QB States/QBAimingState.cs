using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBAimingState : QbState
{
    public QBAimingState(QbController _qb, Animator _anim, string _animBoolName) : base(_qb,_anim, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool(animBoolName, true);

        // TODO: - Here we need to get the aiming direction (the mouse position at least) and update is facingRight
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = qb.rb.position;
        Vector2 facingDirection = mousePosition - playerPosition;

        if (qb.isFacingLeft && facingDirection.x < 0) {
            qb.Flip();
        } else if (!qb.isFacingLeft && facingDirection.x > 0)
        {
            qb.Flip();
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool(animBoolName, false);
    }

    public override void Update()
    {
        base.Update();
    }
}
