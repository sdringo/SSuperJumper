using UnityEngine;

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

        if( owner.transform.position.y < GameMgr.ScreenBounds.min.y )
            owner.dead();

        if( charge ) {
            owner.JumpEN -= owner.reqJump * Time.deltaTime;
            owner.JumpEN = Mathf.Max( 0, owner.JumpEN );
            if( 0 >= owner.JumpEN )
                owner.jump( power );
        }
    }

    public override void onCharge()
    {
        base.onCharge();

        if( 0 < owner.JumpEN ) {
            Animator ani = owner.GetComponent<Animator>();
            ani.SetTrigger( "Charge" );
        }
    }

    public override void onTouchUp()
    {
        base.onTouchUp();

        if( 0 < power.y )
            owner.jump( power * 2.0f );
    }

    public override void onChargeCancel()
    {
        base.onChargeCancel();

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Down" );
    }
}