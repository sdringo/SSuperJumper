﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuStart : Entity
{
    public Text tap;

    public override void initialize()
    {
        base.initialize();

        GameMgr gameMgr = GameObject.FindWithTag( "GameController" ).GetComponent<GameMgr>();
        gameMgr.onGameStart += hide;
        gameMgr.onGameOver += show;

        if( tap ) {
            Sequence blink = DOTween.Sequence();
            blink.Append( tap.DOFade( 0, 0 ) );
            blink.AppendInterval( 0.5f );
            blink.Append( tap.DOFade( 1, 0 ) );
            blink.SetLoops( -1, LoopType.Yoyo );
        }
    }

    public void show()
    {
        gameObject.SetActive( true );
    }

    public void hide()
    {
        gameObject.SetActive( false );
    }
}