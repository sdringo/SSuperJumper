using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGame : Entity
{
    public RectTransform progressEn;
    public RectTransform imgDanger;
    public Text life = null;
    public Text score = null;

    private GameMgr gameMgr = null;
    private Player player = null;
    
    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        gameMgr.onGameStart += show;
        gameMgr.onGameOver += hide;
        gameMgr.onScroll += scroll;
        gameMgr.onGameRestart += reset;

        player = gameMgr.Player;

        GetComponent<UIProgressBar>().setup( gameMgr );
        GetComponent<UIProgressBar>().skinChange( player );

        if( progressEn )
            progressEn.anchoredPosition = new Vector2( 0, 20 );

        if( life )
            life.text = gameMgr.Life.ToString();

        gameObject.SetActive( false );
    }

    public void show()
    {
        gameObject.SetActive( true );

        if( progressEn )
            progressEn.DOAnchorPosY( -20, 1.0f );
    }

    public void hide()
    {
        if( progressEn )
            progressEn.anchoredPosition = new Vector2( 0, 20 );

        gameObject.SetActive( false );
    }

    private void scroll( float distance )
    {
        if( score )
            score.text = string.Format( "{0}", (int)player.Distance );
    }

    private void reset()
    {
        life.text = gameMgr.Life.ToString();
    }
}
