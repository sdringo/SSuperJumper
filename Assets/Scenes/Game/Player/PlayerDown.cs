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

        if( owner.transform.position.y < GameController.ScreenBounds.min.y )
            owner.dead();

        if( charge ) {
            owner.ENJump -= owner.ReqJump * Time.deltaTime;
            owner.ENJump = Mathf.Max( 0, owner.ENJump );
            if( 0 >= owner.ENJump )
                owner.jump( power );
        }
    }

    public override void onTouchBegan()
    {
        if( 0 < owner.ENJump ) {
            base.onTouchBegan();

            Animator ani = owner.GetComponent<Animator>();
            ani.SetTrigger( "Charge" );
        }
    }

    public override void onTouchEnd()
    {
        if( charge ) {
            base.onTouchEnd();

            owner.jump( power * 1.2f );
        }   
    }
}