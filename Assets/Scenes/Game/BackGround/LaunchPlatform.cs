using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlatform : Entity
{
    private Vector3 initPos;
    private bool isScroll = false;

    public void setup( GameMgr mgr )
    {
        if( !mgr )
            return;

        initPos = transform.position;
        isScroll = true;

        mgr.onGameOver += reset;
        mgr.onScroll += scroll;
    }

    public void scroll( float distance )
    {
        if( isScroll ) {
            transform.Translate( 0, -distance, 0 );
            if( transform.position.y < GameMgr.OutBounds.min.y )
                isScroll = false;
        }
    }

    public void reset()
    {
        transform.position = initPos;
        isScroll = true;
    }
}
