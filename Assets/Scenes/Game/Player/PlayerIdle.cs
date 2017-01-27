using UnityEngine;

public class PlayerIdle : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        owner.Gravity = Vector3.zero;
    }

    public override void onTouchEnd()
    {
        base.onTouchEnd();

        owner.jump( power );
    }
}
