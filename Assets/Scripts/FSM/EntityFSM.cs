using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFSM<T> : Entity
{
    protected T owner;
    protected FSMState<T> prev;
    protected FSMState<T> curr;

    public override void update()
    {
        base.update();

        if( null != curr )
            curr.onUpdate( owner );
    }

    public void changeState( FSMState<T> state )
    {
        if( null == state )
            return;

        if( curr == state )
            return;

        prev = curr;

        if( null != curr )
            curr.onExit( owner );

        curr = state;
        curr.onEnter( owner );
    }
}
