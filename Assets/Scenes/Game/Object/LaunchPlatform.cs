using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlatform : Entity
{
    private Vector3 initPos;
    private bool scroll = false;

    public override void initialize()
    {
        base.initialize();

        initPos = transform.position;
        scroll = true;
    }

    public void onScroll( float distance )
    {
        if( scroll ) {
            transform.Translate( 0, -distance, 0 );
            if( transform.position.y < GameMgr.OutBounds.min.y )
                scroll = false;
        }
    }

    public void onReset()
    {
        transform.position = initPos;
        scroll = true;
    }
}
