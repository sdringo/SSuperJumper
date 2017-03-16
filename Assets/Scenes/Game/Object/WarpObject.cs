using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpObject : BaseObject
{
    public override void hit( Player player )
    {
        player.changeState( new PlayerWarp() );

        onOutBounds( this );
    }
}
