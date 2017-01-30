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
    public Action onGameOver;

    public float JumpSpeed = 3.3f;
    public float DownSpeed = 1.5f;
    public float MinSpeed = -5.0f;
    public float ReqShield = 10;
    public float ReqJump = 10;
    public float MaxEn = 1000;

    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; set; }

    public float Distance { get { return distance; } set { distance = value; setStatus(); } }
    public float ENShield { get { return enShield; } set { enShield = value; setStatus(); } }
    public float ENJump { get { return enJump; } set { enJump = value; setStatus(); } }
    public bool Shield { get; set; }

    protected PlayerState prev = null;
    protected PlayerState curr = null;

    private float _maxHeight = 0.0f;
    private float distance = 0.0f;
    private float enShield = 0.0f;
    private float enJump = 0.0f;

    private bool touchBegan = false;
    private bool touchEnd = false;
    private float touchTime = 0.0f;
    private float holdThreshold = 0.25f;

    private Image imgShield = null;
    private Image imgJump = null;

    public override void initialize()
    {
        base.initialize();

        imgShield = GameObject.Find( "bar_shield" ).GetComponent<Image>();
        imgJump = GameObject.Find( "bar_jump" ).GetComponent<Image>();

        ENShield = 300;
        ENJump = 300;
    }

    public override void updateFixed()
    {
        base.updateFixed();

        processTouch();

        if( null != curr )
            curr.onUpdate( this );

        Velocity += Gravity * Time.deltaTime;
        Velocity = new Vector3( 0, Mathf.Max( Velocity.y, MinSpeed ), 0 );

        transform.Translate( Velocity * Time.deltaTime );

        if( transform.position.y > _maxHeight ) {
            onScroll( transform.position.y - _maxHeight );
            transform.position = new Vector3( 0, _maxHeight, 0 );
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

    private void setStatus()
    {
        if( imgShield )
            imgShield.fillAmount = enShield / MaxEn;

        if( imgJump )
            imgJump.fillAmount = enJump / MaxEn;
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
        ENShield = 300;
        ENJump = 300;

        _maxHeight = GameController.ScreenBounds.max.y * 0.2f;

        transform.position = new Vector3( 0, GameController.ScreenBounds.min.y * 0.7f, 0 );

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
