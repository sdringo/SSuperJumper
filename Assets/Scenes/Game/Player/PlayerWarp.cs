using UnityEngine;
using DG.Tweening;

public class PlayerWarp : PlayerState
{
    private float distance = 0.0f;
    private float time = 0.0f;
    private float value = 0.0f;
    private float prev = 0.0f;
    private float curr = 0.0f;

    public override void onEnter( Player owner )
    {
        base.onEnter( owner );

        distance = Well512.Next( 20, 50 );
        time = distance / owner.jumpSpeed;
        prev = 0;
        curr = 0;
        value = 0;

        if( 0.8f < Well512.Rand() ) {
            distance = -distance * 0.5f;
            time = Mathf.Abs( distance / owner.jumpSpeed );
        }

        owner.Velocity = Vector3.zero;
        owner.Gravity = Vector3.zero;
        owner.transform.position = Vector3.zero;
        owner.Shield = true;

        Animator ani = owner.GetComponent<Animator>();
        ani.SetTrigger( "Super" );

        DOTween.To( () => value, x => value = x, distance, time ).OnUpdate( () => {
            prev = curr;
            curr = value;

            float offset = curr - prev;
            owner.Distance += offset;
            owner.onScroll( offset );
        } ).OnComplete( () => {
            owner.Shield = false;
            owner.Gravity = Vector2.down * owner.downSpeed;
            owner.changeState( new PlayerDown() );
        } );

        BackGround bg = GameObject.FindObjectOfType<BackGround>();
        if( bg )
            bg.warp( time );
    }

    public override void onAcquireItem( BaseObject item )
    {

    }
}
