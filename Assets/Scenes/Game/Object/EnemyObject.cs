using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : BaseObject
{
    private void OnTriggerEnter2D( Collider2D other )
    {
        if( other.name.Equals( "Player" ) ) {
            onOutBounds( this );

            Player player = other.GetComponent<Player>();
            if( !player.Shield )
                player.dead();
        }
    }
}
