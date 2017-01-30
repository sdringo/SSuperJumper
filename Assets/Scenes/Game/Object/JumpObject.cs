using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : BaseObject
{
    public float Amout = 10;

    public override void hit( Player player )
    {
        player.ENJump += Amout;

        onOutBounds( this );
    }
}
