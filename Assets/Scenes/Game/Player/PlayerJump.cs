using UnityEngine;

public class PlayerJump : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Jump" );
    }

    public override void onUpdate( Player owner )
    {
        base.onUpdate( owner );

        if( 0 > owner.Velocity.y )
            owner.changeState( new PlayerDown() );
    }

    public override void onClick()
    {
        base.onClick();
    }
}