﻿using UnityEngine;

public class PlayerDown : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Down" );
    }

    public override void onUpdate( Player owner )
    {
        base.onUpdate( owner );

        if( owner.transform.position.y < GameController.ScreenBounds.min.y )
            owner.dead();
    }

    public override void onTouchEnd()
    {
        base.onTouchEnd();

        owner.jump( power );
    }
}