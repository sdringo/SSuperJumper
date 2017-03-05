using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BgComet : BgObject
{
    public override void create( int index )
    {
        float x = GameMgr.ScreenBounds.min.x + GameMgr.ScreenBounds.size.x * Well512.Rand();
        float y = GameMgr.ScreenBounds.min.y + GameMgr.ScreenBounds.size.y * Well512.Rand();
        float scale = 1 + (float)( Well512.Next( 4 ) ) * 0.1f;

        transform.position = new Vector3( x, y, 0 );
        transform.localScale = new Vector3( scale, scale, 1 );

        Sequence actions = DOTween.Sequence();
        actions.AppendInterval( GetComponent<Animator>().GetCurrentAnimatorStateInfo( 0 ).length );
        actions.AppendCallback( () => {
            DestroyObject( gameObject );
        } );
    }
}
