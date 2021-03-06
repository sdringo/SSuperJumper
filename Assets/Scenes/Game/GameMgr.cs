﻿using System;
using UnityEngine;
using DG.Tweening;

public class GameMgr : Entity
{
    public GameObject canvas;

    public static Bounds ScreenBounds { get; set; }
    public static Bounds OutBounds { get; set; }

    public Action onGameStart;
    public Action onGameResume;
    public Action onGamePause;
    public Action onGameOver;
    public Action onGameRestart;
    public Action<float> onScroll;
    public Action<Player> onSkinChanged;

    public Player Player { get; set; }
    public float Score { get; set; }
    public float BestScore { get; set; }
    public int Life { get; set; }
    
    private ObjectRespwan respwaner = null;

    public override void initialize()
    {
        base.initialize();
        
        Vector3 size = Vector2.zero;
        size.y = Camera.main.orthographicSize * 2.0f;
        size.x = size.y * Screen.width / Screen.height;
        ScreenBounds = new Bounds( Vector3.zero, size );
        OutBounds = new Bounds( Vector3.zero, size * 1.5f );

        Score = 0.0f;
        Life = 2;

        playerChange();

        respwaner = GetComponent<ObjectRespwan>();
        respwaner.setup( this );

        createPrefab( "Prefabs/Bg/BackGround" ).GetComponent<BackGround>().setup( this );
        createPrefab( "Prefabs/Bg/LaunchPlatform" ).GetComponent<LaunchPlatform>().setup( this );

        showUI( "Prefabs/UI/UIStart" ).GetComponent<UIStart>().setup( this );
        showUI( "Prefabs/UI/UIGame" ).GetComponent<UIGame>().setup( this );
        showUI( "Prefabs/UI/UIMenu" ).GetComponent<UIMenu>().setup( this );
        showUI( "Prefabs/UI/UICount" );
    }

    public void onTouchBegan()
    {
        if( Player )
            Player.onTouchBegan();
    }

    public void onTouchEnd()
    {
        if( Player )
            Player.onTouchEnd();
    }

    public GameObject showUI( string path )
    {
        return showUI( Resources.Load<GameObject>( path ) );
    }

    public GameObject showUI( GameObject prefab )
    {
        return Instantiate( prefab, canvas.transform, false );
    }

    public GameObject createPrefab( string path )
    {
        return createPrefab( Resources.Load<GameObject>( path ) );
    }

    public GameObject createPrefab( GameObject prefab )
    {
        return Instantiate( prefab, gameObject.transform, false );
    }

    public void gameStart()
    {
        if( Player )
            Player.start();

        onGameStart();
    }

    public void gamePause()
    {
        pause();

        if( Player )
            Player.pause();
    }

    public void gameResume()
    {
        resume();

        if( Player )
            Player.resume();
    }

    public void gameOver()
    {
        if( Player )
            Player.ready();

        Score = 0.0f;
        Life = 2;

        onGameOver();
    }

    public void gameRestart()
    {
        Life--;

        if( Player ) {
            Player.ready();
            Player.start();
        }

        onGameRestart();
    }

    private void scroll( float offset )
    {
        Score += offset;

        if( Score > 10 )
            respwaner.generate();

        onScroll( offset );
    }

    private void playerChange()
    {
        if( Player )
            DestroyObject( Player.gameObject );

        Player = Instantiate( Resources.Load<GameObject>( "Prefabs/Player/Jay" ) ).GetComponent<Player>();
        Player.onScroll += scroll;
        Player.onPlayerDead += playerDead;
        Player.ready();
        attach( Player.gameObject );

        if( null != onSkinChanged )
            onSkinChanged( Player );
    }

    private void playerDead()
    {
        showUI( "Prefabs/Popup/Continue" ).GetComponent<PopupContinue>().setup( this );
    }
}
