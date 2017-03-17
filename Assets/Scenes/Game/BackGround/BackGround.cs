using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackGround : Entity
{
    public SpriteRenderer bgSrc;
    public SpriteRenderer bgDst;
    public List<Sprite> sprites;

    private float distance;
    private float target;
    private float transition;
    private float section;
    private bool first;
    private int index;

    public void setup( GameMgr mgr )
    {
        if( !mgr )
            return;

        distance = 0;
        target = 10;
        transition = 0;
        section = target - transition;
        first = true;

        mgr.onScroll += scroll;
        mgr.onGameOver += reset;

        GetComponent<BgRespwan>().setup( mgr );
    }

    private void reset()
    {
        distance = 0;
        target = 10;
        transition = 0;
        section = target - transition;
        first = true;

        bgSrc.sprite = sprites[0];
        bgSrc.color = Color.white;
        bgDst.sprite = sprites[1];
        bgDst.color = Color.white;
    }

    private void scroll( float offset )
    {
        if( first ) {
            distance += offset;

            if( distance > transition )
                bgSrc.color = new Color( 1, 1, 1, 1.0f - Mathf.Min( 1.0f, ( distance - transition ) / section ) );

            if( distance > target ) {
                index = 2;

                bgSrc.sprite = bgDst.sprite;
                bgSrc.color = Color.white;
                bgDst.sprite = sprites[index];
                bgDst.color = Color.white;

                first = false;
            }
        }
    }

    public void warp( float duration )
    {
        int loop = sprites.Count - 1;

        DOTween.Sequence().Append( bgSrc.DOColor( new Color( 1, 1, 1, 0 ), duration / loop ) ).AppendCallback( () => {
            bgSrc.sprite = bgDst.sprite;
            bgSrc.color = Color.white;

            index = ++index == sprites.Count ? 1 : index;

            bgDst.sprite = sprites[index];
            bgDst.color = Color.white;
        } ).SetLoops( loop );
    }
}
