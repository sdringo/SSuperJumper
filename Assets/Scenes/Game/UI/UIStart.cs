using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIStart : Entity
{
    public Text tap;

    private GameMgr gameMgr = null;

    public override void initialize()
    {
        base.initialize();

        if( tap ) {
            Sequence blink = DOTween.Sequence();
            blink.Append( tap.DOFade( 0, 0 ) );
            blink.AppendInterval( 0.5f );
            blink.Append( tap.DOFade( 1, 0 ) );
            blink.SetLoops( -1, LoopType.Yoyo );
        }
    }

    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        gameMgr.onGameStart += hide;
        gameMgr.onGameOver += show;
    }

    public void start()
    {
        if( gameMgr )
            gameMgr.gameStart();
    }

    public void show()
    {
        gameObject.SetActive( true );
    }

    public void hide()
    {
        gameObject.SetActive( false );
    }

    public void showSkin()
    {
        gameMgr.showUI( "Prefabs/Popup/Skin" );
    }

    public void showTutorial()
    {
        gameMgr.showUI( "Prefabs/Popup/Tutorial" );
    }
}
