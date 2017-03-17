using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : Entity
{
    public enum Status
    {
        IDLE,
        JUMP,
        DOWN,
    }

    public Action<float> onScroll;
    public Action onJumpEnChange;
    public Action onShieldEnChange;
    public Action onSuperJumpBegin;
    public Action onSuperJumpEnd;
    public Action onPlayerDead;
    public Action onWarpBegin;
    public Action onWarpEnd;
    public Action onBlackHoleBegin;
    public Action onBlackHoleEnd;

    public float jumpSpeed = 8;
    public float downSpeed = 5;
    public float maxSpeed = 13;
    public float minSpeed = -10;
    public float reqShield = 135;
    public float reqJump = 115;
    public float maxEn = 10000;
    public float superTime = 5;

    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; set; }
    public float Distance { get; set; }
    public float ShieldEN { get { return shieldEn; } set { shieldEn = value; if( null != onShieldEnChange ) onShieldEnChange(); } }
    public float JumpEN { get { return jumpEn; } set { jumpEn = value; if( null != onJumpEnChange ) onJumpEnChange(); } }
    public bool Shield { get; set; }

    protected PlayerState prev = null;
    protected PlayerState curr = null;

    private BaseObject hitted = null;
    private float shieldEn = 0.0f;
    private float jumpEn = 0.0f;

    //private bool touchBegan = false;
    //private bool touchEnd = false;
    //private float touchTime = 0.0f;
    //private float holdThreshold = 0.125f;

    public override void updateFixed()
    {
        base.updateFixed();

        //processTouch();

        if( null != curr )
            curr.onUpdate( this );

        Velocity += Gravity * Time.deltaTime;
        Velocity = new Vector3( 0, Mathf.Min( Mathf.Max( Velocity.y, minSpeed ), maxSpeed ), 0 );

        transform.Translate( Velocity * Time.deltaTime );

        if( transform.position.y > 0 ) {
            Distance += transform.position.y;
            onScroll( transform.position.y );
            transform.position = Vector3.zero;
        }
    }

    private BaseObject raycastItem()
    {
        Collider2D[] result = Physics2D.OverlapCircleAll( transform.position, 0.63f );
        foreach( Collider2D collider in result ) {
            if( collider.name.StartsWith( "Item" ) )
                return collider.GetComponent<BaseObject>();
        }

        return null;
    }

    public void onTouchBegan()
    {
        hitted = raycastItem();

        if( null != curr )
            curr.onTouchDown();
    }

    public void onTouchEnd()
    {
        if( hitted ) {
            BaseObject result = raycastItem();

            if( null != result && hitted.name.Equals( result.name ) ) {
                curr.onAcquireItem( hitted );

                if( JumpEN + ShieldEN > maxEn )
                    changeState( new PlayerSuper() );
                else
                    curr.onChargeCancel();
            } else {
                curr.onTouchUp();
            }

            hitted = null;
        } else {
            curr.onTouchUp();
        }
    }

    public void changeState( PlayerState state )
    {
        if( null == state )
            return;

        if( curr == state )
            return;

        prev = curr;

        if( null != curr )
            curr.onExit( this );

        curr = state;
        curr.onEnter( this );
    }

    public void ready()
    {
        ShieldEN = maxEn * 0.3f;
        JumpEN = maxEn * 0.5f;
        Distance = 0;

        transform.position = new Vector3( 0, GameMgr.ScreenBounds.min.y * 0.5f, 0 );

        changeState( new PlayerIdle() );
    }

    public void start()
    {
        jump( Vector3.up * jumpSpeed * 2 );
    }

    public void jump( Vector3 power )
    {
        Gravity = Vector3.down * downSpeed;
        Velocity = power;

        changeState( new PlayerJump() );
    }

    public void blackhole()
    {

    }

    public void dead()
    {
        changeState( new PlayerDead() );
    }
}
