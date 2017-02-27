using System;
using UnityEngine;

public class BaseObject : Entity
{
    public Action<BaseObject> onOutBounds;

    public float percent = 0.1f;

    public override void initialize()
    {
        base.initialize();
    }

    public void scroll( float distance )
    {
        transform.Translate( 0, -distance, 0 );

        if( !GameMgr.OutBounds.Contains( transform.position ) )
            onOutBounds( this );
    }

    public virtual void hit( Player player )
    {
        
    }
}
