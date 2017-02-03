using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : Entity
{
    public SpriteRenderer bgSrc;
    public SpriteRenderer bgDst;
    public List<Sprite> sprites;

    public float section = 100.0f;

    private float total = 0.0f;
    private int curr = 0;
    private int prev = 0;
    private int index = 1;

    public override void initialize()
    {
        base.initialize();

        GameMgr gameMgr = FindObjectOfType<GameMgr>();
        gameMgr.onScroll += scroll;
        gameMgr.onGameOver += gameOver;
    }

    private void scroll( float distance )
    {
        total += distance;

        prev = curr;
        curr = (int)(total / section);

        if( prev != curr )
            changeBg();

        float ratio = (total % section) / section;
        bgSrc.color = new Color( 1, 1, 1, 1.0f - ratio );
        bgDst.color = new Color( 1, 1, 1, ratio );
    }

    private void gameOver()
    {
        total = 0;
        curr = 0;
        prev = 0;
        index = 1;

        bgSrc.sprite = sprites[0];
        bgSrc.color = Color.white;
        bgDst.sprite = sprites[1];
        bgDst.color = Color.white;
    }

    private void changeBg()
    {
        bgSrc.sprite = bgDst.sprite;

        index++;
        if( index > sprites.Count )
            index = 0;

        bgDst.sprite = sprites[index];
    }
}
