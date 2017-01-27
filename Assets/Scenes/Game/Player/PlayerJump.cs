using UnityEngine;

public class PlayerJump : PlayerState
{
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