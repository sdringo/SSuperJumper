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

    private Player player = null;

    public override void initialize()
    {
        base.initialize();

        GameMgr.instance.onGameStart += show;
        GameMgr.instance.onGameOver += hide;
        GameMgr.instance.onScroll += scroll;
        GameMgr.instance.onGameRestart += reset;

        player = GameObject.FindWithTag( "Player" ).GetComponent<Player>();

        if( progressEn )
            progressEn.anchoredPosition = new Vector2( 0, 20 );

        if( life )
            life.text = GameMgr.instance.Life.ToString();

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
        life.text = GameMgr.instance.Life.ToString();
    }
}
