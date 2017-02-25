using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlatform : Entity
{
    private Vector3 initPos;
    private bool isScroll = false;

    public override void initialize()
    {
        base.initialize();

        initPos = transform.position;
        isScroll = true;

        GameMgr.instance.onGameOver += reset;
        GameMgr.instance.onScroll += scroll;
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
