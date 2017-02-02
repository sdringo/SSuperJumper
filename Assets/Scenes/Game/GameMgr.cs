using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameMgr : Entity
{
    public Action onGameStart;
    public Action onGameResume;
    public Action onGamePause;
    public Action onGameOver;

    public static Bounds ScreenBounds { get; set; }
    public static Bounds OutBounds { get; set; }

    public float respwanDistance = 10.0f;
    public List<BaseObject> respwans = new List<BaseObject>();

    private GameObject bg = null;
    private Bounds bgBounds;

    private Player player = null;
    private float score = 0.0f;

    private List<BaseObject> objects = new List<BaseObject>();
    private int lastRespwan = 0;
    private Vector3 respwarnPos = Vector3.zero;

    public override void initialize()
    {
        base.initialize();

        DOTween.Init( false, true, LogBehaviour.ErrorsOnly );

        Vector3 size = Vector2.zero;
        size.y = Camera.main.orthographicSize * 2.0f;
        size.x = size.y * Screen.width / Screen.height;
        ScreenBounds = new Bounds( Vector3.zero, size );
        OutBounds = new Bounds( Vector3.zero, size * 1.5f );

        bg = transform.FindChild( "BgSrc" ).gameObject;
        bgBounds = bg.GetComponent<SpriteRenderer>().bounds;

        player = FindObjectOfType<Player>();
        player.onScroll += onScroll;
        player.onPlayerDead += onPlayerDead;
        player.ready();
        
        LaunchPlatform platform = gameObject.GetComponentInChildren<LaunchPlatform>();
        player.onScroll += platform.onScroll;
        player.onPlayerDead += platform.onReset;

        onGameStart += player.start;
        onGamePause += player.pause;
        onGameResume += player.resume;

        score = 0.0f;
        lastRespwan = 0;
        respwarnPos.y = Camera.main.orthographicSize * 1.1f;
    }

    public override void update()
    {
        base.update();

        if( Input.GetKey( KeyCode.Escape ) ) {
            Application.Quit();
        }
    }

    public void gameStart()
    {
        onGameStart();
    }

    public void gamePause()
    {
        pause();

        onGamePause();
    }

    public void gameResume()
    {
        resume();

        onGameResume();
    }

    public void onScroll( float distance )
    {
        score += distance;

        int current = (int)( score / respwanDistance );
        if( lastRespwan < current ) {
            lastRespwan = current;

            int index = UnityEngine.Random.Range( 0, respwans.Count );

            BaseObject obj = Instantiate<BaseObject>( respwans[index] );
            obj.transform.Translate( respwarnPos );
            obj.onOutBounds = onOutBounds;
            player.onScroll += obj.onScroll;

            objects.Add( obj );
        }

        //bg1.transform.Translate( 0, -distance * 0.1f, 0 );
        //bg2.transform.Translate( 0, -distance * 0.1f, 0 );

        if( bg.transform.position.y < -bgBounds.size.y ) {
            float offset = bgBounds.size.y + bg.transform.position.y;
            bg.transform.position = new Vector3( 0, bgBounds.size.y + offset, 0 );
        }

        //if( bg2.transform.position.y < -bgBounds.size.y ) {
        //    float offset = bgBounds.size.y + bg2.transform.position.y;
        //    bg2.transform.position = new Vector3( 0, bgBounds.size.y + offset, 0 );
        //}
    }

    public void onPlayerDead()
    {
        onGameOver();

        if( player )
            player.ready();

        //if( bg2 )
        //    bg2.transform.position = new Vector3( 0, bgBounds.size.y, 0 );

        foreach( BaseObject obj in objects ) {
            if( player )
                player.onScroll -= obj.onScroll;

            GameObject.DestroyObject( obj.gameObject );
        }

        objects.Clear();

        score = 0.0f;
        lastRespwan = 0;
    }

    public void onOutBounds( BaseObject obj )
    {
        if( player )
            player.onScroll -= obj.onScroll;

        objects.Remove( obj );

        GameObject.DestroyObject( obj.gameObject );
    }
}
