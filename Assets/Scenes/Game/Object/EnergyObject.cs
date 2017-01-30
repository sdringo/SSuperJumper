using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObject : BaseObject
{
    public float Amout = 10;

    public override void hit( Player player )
    {
        player.ENShield += Amout;

        onOutBounds( this );
    }
}
