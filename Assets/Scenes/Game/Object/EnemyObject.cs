using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : BaseObject
{
    public override void hit( Player player )
    {
        if( player.Shield )
            onOutBounds( this );
        else
            player.onGameOver();
    }
}
