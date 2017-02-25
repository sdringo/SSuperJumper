using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameMgr : SingletonObject<GameMgr>
{
    public Action onGameStart;
    public Action onGameResume;
    public Action onGamePause;
    public Action onGameOver;
    public Action onGameRestart;
    public Action<float> onScroll;

    public static Bounds ScreenBounds { get; set; }
    public static Bounds OutBounds { get; set; }

    public float respwanDistance = 10.0f;
    public List<BaseObject> respwans = new List<BaseObject>();

    public float Score { get; set; }
    public int Life { get; set; }

    private Player player = null;

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

        player = FindObjectOfType<Player>();
        player.onScroll += scroll;
        player.onPlayerDead += playerDead;
        player.ready();
        
        onGameStart += player.start;
        onGamePause += player.pause;
        onGameResume += player.resume;

        Score = 0.0f;
        Life = 2;
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

    public void gameOver()
    {
        resume();

        if( player )
            player.ready();

        foreach( BaseObject obj in objects ) {
            onScroll -= obj.scroll;

            GameObject.DestroyObject( obj.gameObject );
        }

        objects.Clear();
        lastRespwan = 0;

        Score = 0.0f;
        Life = 2;

        onGameResume();
        onGameOver();
    }

    public void gameRestart()
    {
        resume();

        Life--;

        if( player ) {
            player.ready();
            player.start();
        }

        onGameResume();
        onGameRestart();
    }

    private void scroll( float distance )
    {
        onScroll( distance );

        Score += distance;

        int current = (int)( Score / respwanDistance );
        if( lastRespwan < current ) {
            lastRespwan = current;

            int index = UnityEngine.Random.Range( 0, respwans.Count );

            BaseObject obj = Instantiate<BaseObject>( respwans[index] );
            obj.transform.Translate( respwarnPos );
            obj.onOutBounds = onOutBounds;
            onScroll += obj.scroll;

            objects.Add( obj );
        }
    }

    private void playerDead()
    {
        pause();

        onGamePause();

        PopupMgr.instance.showContinue();
    }

    public void onOutBounds( BaseObject obj )
    {
        onScroll -= obj.scroll;

        objects.Remove( obj );

        GameObject.DestroyObject( obj.gameObject );
    }
}
