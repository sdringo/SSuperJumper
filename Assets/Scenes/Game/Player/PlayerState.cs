using UnityEngine;

public class PlayerState : FSMState<Player>
{
    protected Player owner = null;
    protected Vector3 power = Vector3.zero;
    protected bool charge = false;

    public override void onEnter( Player owner )
    {
        Debug.Log( GetType().Name + " : onEnter" );

        this.owner = owner;
    }

    public override void onExit( Player owner )
    {
        Debug.Log( GetType().Name + " : onExit" );
    }

    public override void onUpdate( Player owner )
    {
        if( charge )
            power += Vector3.up * owner.jumpSpeed * Time.deltaTime;
    }

    public virtual void onClick()
    {
        Debug.Log( GetType().Name + " : onClick" );
    }

    public virtual void onTouchBegan()
    {
        Debug.Log( GetType().Name + " : onTouchBegan" );

        power = Vector3.zero;
        charge = true;
    }

    public virtual void onTouchEnd()
    {
        Debug.Log( GetType().Name + " : onTouchEnd" );

        charge = false;
    }
}
