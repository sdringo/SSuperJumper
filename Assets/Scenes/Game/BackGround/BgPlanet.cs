using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPlanet : BgObject
{
    public override void create( int index )
    {
        float scale = (float)( 3 + -index + Well512.Next( 5 ) ) * 0.1f;

        transform.localScale = new Vector3( scale, scale, 1 );

        bounds = EntityUtil.findBounds( gameObject );

        float x = Well512.Next( (uint)(GameMgr.ScreenBounds.max.x * 0.3f), (uint)( GameMgr.ScreenBounds.max.x ) ) * index;
        float y = GameMgr.ScreenBounds.max.y + bounds.size.y * 0.5f;
        transform.localPosition = new Vector3( x, y, 0 );
    }

    public override void scroll( float offset )
    {
        transform.Translate( 0, -offset * 0.1f, 0 );
    }
}