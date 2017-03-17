using UnityEngine;
using DG.Tweening;

public class PlayerSuper : PlayerState
{
    private float prevMax = 0.0f;

    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Super" );

        prevMax = owner.maxSpeed;

        owner.Shield = true;
        owner.Gravity = Vector2.zero;
        owner.Velocity = Vector2.up * owner.maxSpeed * 1.3f;
        owner.maxSpeed = prevMax * 2;
        owner.onSuperJumpBegin();

        DOTween.Sequence().AppendInterval( owner.superTime ).AppendCallback( () => {
            owner.Gravity = Vector2.down * owner.downSpeed;
        } );
    }

    public override void onUpdate( Player owner )
    {
        base.onUpdate( owner );

        if( 0 > owner.Velocity.y ) {
            owner.Shield = false;
            owner.maxSpeed = prevMax;
            owner.JumpEN = owner.JumpEN * 0.7f;
            owner.ShieldEN = owner.ShieldEN * 0.7f;
            owner.changeState( new PlayerDown() );
            owner.onSuperJumpEnd();
        }
    }

    public override void onAcquireItem( BaseObject item )
    {
        
    }
}
