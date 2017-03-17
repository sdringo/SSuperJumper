﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObject : BaseObject
{
    public float Amout = 10;

    public override void apply( Player player )
    {
        player.ShieldEN += Amout;

        onOutBounds( this );
    }
}
