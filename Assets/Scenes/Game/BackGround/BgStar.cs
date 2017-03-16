using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgStar : BgObject
{
    private float scale = 1.0f;

    public override void initialize()
    {
        base.initialize();

        float x = GameMgr.ScreenBounds.min.x + GameMgr.ScreenBounds.size.x * Well512.Rand();
        float y = GameMgr.ScreenBounds.max.y * Well512.Rand();
        transform.position = new Vector3( x, y, 0 );

        scale = Mathf.Max( 0.3f, Mathf.Min( Well512.Rand(), 0.8f ) );
    }

    public override void create( int index )
    {
        float x = GameMgr.ScreenBounds.min.x + GameMgr.ScreenBounds.size.x * Well512.Rand();
        float y = GameMgr.ScreenBounds.max.y * 1.1f;
        transform.position = new Vector3( x, y, 0 );
    }

    public override void scroll( float offset )
    {
        transform.Translate( 0, -offset * scale, 0 );
    }
}
