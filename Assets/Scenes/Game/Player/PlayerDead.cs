using UnityEngine;

public class PlayerDead : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        owner.Velocity = owner.Gravity;
        owner.onPlayerDead();

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Dead" );
    }

    public override void onUpdate( Player owner )
    {
        base.onUpdate( owner );

        if( owner.transform.position.y < GameMgr.OutBounds.min.y ) {
            owner.Gravity = Vector3.zero;
            owner.Velocity = Vector3.zero;
        }
    }
}
