﻿using System;
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

    public float jumpSpeed = 3.3f;
    public float downSpeed = 1.5f;
    public float minSpeed = -5.0f;

    private Bounds _bounds;
    private float _maxHeight = 0.0f;

    private Status _status = Status.IDLE;
    private Vector3 _power = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    public float Distance { get; set; }

    private bool _charged = false;

    public void ready()
    {
        _status = Status.IDLE;

        Vector3 size = Vector2.zero;
        size.y = Camera.main.orthographicSize * 2.0f;
        size.x = size.y * Screen.width / Screen.height;

        _bounds = new Bounds( Vector3.zero, size );
        _maxHeight = _bounds.max.y * 0.2f;

        transform.position = new Vector3( 0, _bounds.min.y * 0.7f, 0 );
    }

    public override void update()
    {
        base.update();

        if( _status != Status.IDLE ) {
            _velocity += Vector3.down * downSpeed * Time.deltaTime;
            _velocity.y = Mathf.Max( _velocity.y, minSpeed );

            transform.Translate( _velocity * Time.deltaTime );

            if( transform.position.y > _maxHeight ) {
                onScroll( transform.position.y - _maxHeight );
                transform.position = new Vector3( 0, _maxHeight, 0 );
            }
        }

        if( _charged )
            _power += Vector3.up * jumpSpeed * Time.deltaTime;

        switch( _status ) {
            case Status.JUMP: {
                    if( _velocity.y < 0 ) {
                        _status = Status.DOWN;
                    }
                }
                break;

            case Status.DOWN: {
                    if( transform.position.y < _bounds.min.y ) {
                        _velocity.Set( 0, 0, 0 );
                        _charged = false;
                        onGameOver();
                    }
                }
                break;
        }
    }

    public void onApplyItem( BaseObject item )
    {
        item.remove();
    }

    public void onTouchDown()
    {
        if( _status == Status.IDLE || _status == Status.DOWN ) {
            _charged = true;
        }
    }

    public void onTouchUp()
    {
        if( _charged ) {
            _status = Status.JUMP;
            _velocity = _power;
            _power = Vector3.zero;
            _charged = false;
        }
    }
}
