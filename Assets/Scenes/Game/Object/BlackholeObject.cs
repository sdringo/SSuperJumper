using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeObject : BaseObject
{
    public override void hit( Player player )
    {
        onOutBounds( this );
    }
}
