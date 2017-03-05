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

    public float jumpSpeed = 3.3f;
    public float downSpeed = 1.5f;
    public float maxSpeed = 10.0f;
    public float minSpeed = -5.0f;
    public float reqShield = 10;
    public float reqJump = 10;
    public float maxEn = 1000;
    public float superTime = 5.0f;

    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; set; }
    public float Distance { get; set; }
    public float ShieldEN { get { return shieldEn; } set { shieldEn = value; if( null != onShieldEnChange ) onShieldEnChange(); } }
    public float JumpEN { get { return jumpEn; } set { jumpEn = value; if( null != onJumpEnChange ) onJumpEnChange(); } }
    public bool Shield { get; set; }

    protected PlayerState prev = null;
    protected PlayerState curr = null;

    private BaseObject hittedItem = null;
    private float shieldEn = 0.0f;
    private float jumpEn = 0.0f;
    private float prevMax = 0.0f;

    private bool touchBegan = false;
    private bool touchEnd = false;
    private float touchTime = 0.0f;
    private float holdThreshold = 0.15f;

    public override void initialize()
    {
        base.initialize();
    }

    public override void updateFixed()
    {
        base.updateFixed();

        processTouch();

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

    private void OnTriggerEnter2D( Collider2D other )
    {
        if( other.name.Contains( "Shield" ) || other.name.Contains( "Jump" ) ) {
            hittedItem = other.GetComponent<BaseObject>();
        }
    }

    private void OnTriggerExit2D( Collider2D other )
    {
        if( !hittedItem )
            return;

        if( hittedItem.name.Equals( other.name ) ) {
            hittedItem = null;
        }   
    }

    private void processTouch()
    {
        if( touchBegan ) {
            touchTime += Time.fixedDeltaTime;

            if( holdThreshold < touchTime ) {
                touchBegan = false;

                if( null != curr )
                    curr.onTouchBegan();
            }
        }

        if( touchEnd ) {
            if( holdThreshold < touchTime ) {
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

    public void checkItem()
    {
        if( hittedItem ) {
            hittedItem.hit( this );

            if( JumpEN + ShieldEN > maxEn )
                superJumpBegin();
        }
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

    public void superJumpBegin()
    {
        Gravity = Vector2.zero;
        Velocity = Vector2.up * maxSpeed * 1.3f;
        Shield = true;

        prevMax = maxSpeed;
        maxSpeed = prevMax * 2;

        changeState( new PlayerSuper() );

        onSuperJumpBegin();

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval( superTime );
        seq.AppendCallback( () => {
            Gravity = Vector2.down * downSpeed;
        } );
    }

    public void superJumpEnd()
    {
        maxSpeed = prevMax;

        Shield = false;
        JumpEN = JumpEN * 0.75f;
        ShieldEN = ShieldEN * 0.75f;

        changeState( new PlayerDown() );

        onSuperJumpEnd();
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
        touchBegan = false;
        touchEnd = true;
    }
}
