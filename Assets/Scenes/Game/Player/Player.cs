using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public enum Status
    {
        IDLE,
        JUMP,
        DOWN,
    }

    public Action<float> onScroll;
    public Action onValueChange;
    public Action onGameOver;

    public float JumpSpeed = 3.3f;
    public float DownSpeed = 1.5f;
    public float MinSpeed = -5.0f;
    public float ReqShield = 10;
    public float ReqJump = 10;
    public float MaxEn = 1000;

    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; set; }

    public float Distance { get { return distance; } set { distance = value; if( null != onValueChange ) onValueChange(); } }
    public float ShieldEN { get { return shieldEn; } set { shieldEn = value; if( null != onValueChange ) onValueChange(); } }
    public float JumpEN { get { return jumpEn; } set { jumpEn = value; if( null != onValueChange ) onValueChange(); } }
    public bool Shield { get; set; }

    protected PlayerState prev = null;
    protected PlayerState curr = null;

    private float distance = 0.0f;
    private float shieldEn = 0.0f;
    private float jumpEn = 0.0f;

    private bool touchBegan = false;
    private bool touchEnd = false;
    private float touchTime = 0.0f;
    private float holdThreshold = 0.25f;

    public override void updateFixed()
    {
        base.updateFixed();

        processTouch();

        if( null != curr )
            curr.onUpdate( this );

        Velocity += Gravity * Time.deltaTime;
        Velocity = new Vector3( 0, Mathf.Max( Velocity.y, MinSpeed ), 0 );

        transform.Translate( Velocity * Time.deltaTime );

        if( transform.position.y > 0 ) {
            onScroll( transform.position.y );
            transform.position = Vector3.zero;
        }
    }

    private void processTouch()
    {
        if( touchBegan ) {
            touchTime += Time.deltaTime;

            if( holdThreshold < touchTime ) {
                touchBegan = false;

                if( null != curr )
                    curr.onTouchBegan();
            }
        }

        if( touchEnd ) {
            if( !touchBegan ) {
                if( null != curr )
                    curr.onTouchEnd();
            } else {
                if( null != curr )
                    curr.onClick();
            }

            touchBegan = false;
            touchEnd = false;
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
        ShieldEN = 300;
        JumpEN = 300;

        transform.position = new Vector3( 0, GameMgr.ScreenBounds.min.y * 0.5f, 0 );

        changeState( new PlayerIdle() );
    }

    public void jump( Vector3 power )
    {
        Gravity = Vector3.down * DownSpeed;
        Velocity = power;

        changeState( new PlayerJump() );
    }

    public void dead()
    {
        changeState( new PlayerDead() );
    }

    public void onTouchBegan()
    {
        touchBegan = true;
        touchTime = 0.0f;
    }

    public void onTouchEnd()
    {
        touchEnd = true;
    }
}
