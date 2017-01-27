using System;
using UnityEngine;

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
    public Vector3 Velocity { get; set; }
    public Vector3 Gravity { get; set; }
    public float Distance { get; set; }
    
    protected PlayerState prev = null;
    protected PlayerState curr = null;

    
    private float _maxHeight = 0.0f;

    private bool touchBegan = false;
    private bool touchEnd = false;
    private float touchTime = 0.0f;
    private float holdThreshold = 0.3f;

    public override void update()
    {
        base.update();

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

    public void checkItem()
    {
        RaycastHit2D hit = Physics2D.Raycast( transform.position, Vector2.zero );
        if( hit.collider ) {
            BaseObject item = hit.collider.GetComponent<BaseObject>();
            if( item )
                item.remove();
        }
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
