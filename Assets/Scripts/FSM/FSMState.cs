using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState<T>
{
    public abstract void onEnter( T owner );

    public abstract void onExit( T owner );

    public abstract void onUpdate( T owner );
}
