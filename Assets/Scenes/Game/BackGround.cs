using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : Entity
{
    public SpriteRenderer bgSrc;
    public SpriteRenderer bgDst;
    public List<Sprite> sprites;

    public float section = 100.0f;
    public float change = 10.0f;

    private float total = 0.0f;
    private bool changing = false;
    private int index = 1;

    public override void initialize()
    {
        base.initialize();

        total = section - change;

        GameMgr.instance.onScroll += scroll;
        GameMgr.instance.onGameOver += gameOver;
    }

    private void scroll( float distance )
    {
        total += distance;

        if( section - change < total % section ) {
            changing = true;

            float ratio = ( section - total % section ) / change;
            bgSrc.color = new Color( 1, 1, 1, ratio );
            bgDst.color = new Color( 1, 1, 1, 1.0f - ratio );
        } else {
            if( changing ) {
                changeBg();
                changing = false;
            }
        }
    }

    private void gameOver()
    {
        total = section - change;
        index = 1;

        bgSrc.sprite = sprites[0];
        bgSrc.color = Color.white;
        bgDst.sprite = sprites[1];
        bgDst.color = Color.white;
    }

    private void changeBg()
    {
        bgSrc.sprite = bgDst.sprite;
        bgSrc.color = Color.white;

        index++;
        if( index > sprites.Count )
            index = 1;

        bgDst.sprite = sprites[index];
        bgDst.color = Color.white;
    }
}
