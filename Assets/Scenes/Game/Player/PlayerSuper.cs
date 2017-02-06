using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSuper : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Super" );
    }

    public override void onUpdate( Player owner )
    {
        base.onUpdate( owner );

        if( 0 > owner.Velocity.y )
            owner.superJumpEnd();
    }
}
