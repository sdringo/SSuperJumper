using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : BaseObject
{
    public float Amout = 10;

    public override void apply( Player player )
    {
        player.JumpEN += Amout;

        onOutBounds( this );
    }
}
