using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuGame : Entity
{
    public RectTransform progressEn;
    public RectTransform imgDanger;

    public override void initialize()
    {
        base.initialize();

        GameMgr gameMgr = GameObject.FindWithTag( "GameController" ).GetComponent<GameMgr>();
        gameMgr.onGameStart += show;
        gameMgr.onGameOver += hide;

        if( progressEn )
            progressEn.anchoredPosition = new Vector2( 0, 20 );

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
}
