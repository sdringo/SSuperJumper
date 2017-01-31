﻿using UnityEngine;

public class PlayerJump : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Jump" );

        owner.Shield = false;
    }

    public override void onUpdate( Player owner )
    {
        base.onUpdate( owner );

        if( 0 > owner.Velocity.y )
            owner.changeState( new PlayerDown() );

        if( owner.Shield ) {
            owner.ShieldEN -= owner.ReqShield * Time.deltaTime;
            owner.ShieldEN = Mathf.Max( 0, owner.ShieldEN );
            if( 0 >= owner.ShieldEN ) {
                owner.Shield = false;

                Animator ani = owner.GetComponent<Animator>();
                ani.SetTrigger( "Jump" );
            }   
        }
    }

    public override void onTouchBegan()
    {
        base.onTouchBegan();

        if( 0 < owner.ShieldEN ) {
            owner.Shield = true;

            Animator ani = owner.GetComponent<Animator>();
            ani.SetTrigger( "Shield" );
        }
    }

    public override void onTouchEnd()
    {
        base.onTouchEnd();

        if( owner.Shield ) {
            Animator ani = owner.GetComponent<Animator>();
            ani.SetTrigger( "Jump" );
        }

        owner.Shield = false;
    }

    public override void onClick()
    {
        base.onClick();

        RaycastHit2D[] hits = Physics2D.RaycastAll( owner.transform.position, Vector2.zero );
        foreach( RaycastHit2D hit in hits ) {
            if( hit.collider ) {
                BaseObject item = hit.collider.GetComponent<BaseObject>();
                if( item )
                    item.hit( owner );
            }
        }
    }
}