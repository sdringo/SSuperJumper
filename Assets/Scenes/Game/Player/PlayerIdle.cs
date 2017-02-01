using UnityEngine;

public class PlayerIdle : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        owner.Gravity = Vector3.zero;

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Idle" );
    }

    public override void onClick()
    {
        base.onClick();

        owner.jump( Vector3.up * owner.jumpSpeed * 2 );
    }
}
