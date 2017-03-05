using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObject : Entity
{
    public virtual void create( int index )
    {
        
    }

    public virtual void scroll( float offset )
    {
        transform.Translate( 0, -offset, 0 );
    }

    public bool checkOutBounds()
    {
        bounds = EntityUtil.findBounds( gameObject );
        if( 0 < bounds.center.y )
            return false;

        return !GameMgr.ScreenBounds.Intersects( bounds );
    }
}
