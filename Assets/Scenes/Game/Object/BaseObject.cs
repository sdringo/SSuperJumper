using System;
using UnityEngine;

public class BaseObject : Entity
{
    public Action<BaseObject> onOutBounds;

    private Bounds screenBounds;

    public override void initialize()
    {
        base.initialize();

        Vector3 size = Vector2.zero;
        size.y = Camera.main.orthographicSize * 2.0f;
        size.x = size.y * Screen.width / Screen.height;

        screenBounds = new Bounds( Vector3.zero, size );
        screenBounds.Expand( Camera.main.orthographicSize * 0.5f );
    }

    public void onScroll( float distance )
    {
        transform.Translate( 0, -distance, 0 );

        if( !screenBounds.Contains( transform.position ) )
            onOutBounds( this );
    }

    public virtual void hit( Player player )
    {
        
    }
}
