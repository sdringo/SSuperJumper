using UnityEngine;

public class PlayerDead : PlayerState
{
    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        owner.Gravity = Vector3.zero;
        owner.Velocity = Vector3.zero;
    }

    public override void onClick()
    {
        base.onClick();

        owner.onGameOver();
    }
}
