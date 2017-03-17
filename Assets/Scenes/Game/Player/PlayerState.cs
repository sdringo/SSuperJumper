using UnityEngine;

public class PlayerState : FSMState<Player>
{
    protected Player owner = null;
    protected Vector3 power = Vector3.zero;
    protected bool touched = false;
    protected bool charge = false;
    protected float time = 0.0f;

    public override void onEnter( Player owner )
    {
        //Debug.Log( GetType().Name + " : onEnter" );

        this.owner = owner;
    }

    public override void onExit( Player owner )
    {
        //Debug.Log( GetType().Name + " : onExit" );
    }

    public override void onUpdate( Player owner )
    {
        if( touched ) {
            time += Time.fixedDeltaTime;
            if( time > 0.1f )
                onCharge();
        }

        if( charge )
            power += Vector3.up * owner.jumpSpeed * Time.fixedDeltaTime;
    }

    public virtual void onTouchDown()
    {
        //Debug.Log( GetType().Name + " : onTouchDown" );

        touched = true;
        time = 0.0f;
    }

    public virtual void onTouchUp()
    {
        //Debug.Log( GetType().Name + " : onTouchUp" );

        charge = false;
        touched = false;
    }

    public virtual void onCharge()
    {
        //Debug.Log( GetType().Name + " : onCharge" );

        power = Vector3.zero;
        charge = true;
        touched = false;
    }

    public virtual void onChargeCancel()
    {
        //Debug.Log( GetType().Name + " : onChargeCancel" );

        power = Vector3.zero;
        charge = false;
        touched = false;
    }

    public virtual void onAcquireItem( BaseObject item )
    {
        //Debug.Log( GetType().Name + " : onAcquireItem" );

        item.apply( owner );
    }
}
