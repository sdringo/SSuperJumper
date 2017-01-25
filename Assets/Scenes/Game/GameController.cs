using UnityEngine;
using System.Collections.Generic;

public class GameController : Entity
{
    private GameObject bg1 = null;
    private GameObject bg2 = null;
    private Bounds bgBounds;

    private Player player = null;
    private List<Object> objects = new List<Object>();

    private float distance = 0.0f;

    public override void initialize()
    {
        base.initialize();

        bg1 = transform.GetChild( 0 ).gameObject;
        bg2 = transform.GetChild( 1 ).gameObject;

        bgBounds = bg1.GetComponent<SpriteRenderer>().bounds;
        bg2.transform.Translate( 0, bgBounds.size.y, 0 );

        player = GameObject.FindObjectOfType<Player>();
        player.ready();
        player.onScroll = onScroll;
        player.onGameOver = onGameOver;

        distance = 0.0f;
    }

    public void onTouchDown()
    {
        if( player )
            player.onTouchDown();
    }

    public void onTouchUp()
    {
        if( player )
            player.onTouchUp();
    }

    public void onScroll()
    {
        float moved = player.Distance;

        bg1.transform.Translate( 0, -moved * 0.1f, 0 );
        bg2.transform.Translate( 0, -moved * 0.1f, 0 );

        if( bg1.transform.position.y < -bgBounds.size.y ) {
            float offset = bgBounds.size.y + bg1.transform.position.y;
            bg1.transform.position = new Vector3( 0, bgBounds.size.y * 2 + offset, 0 );
        }

        if( bg2.transform.position.y < -bgBounds.size.y ) {
            float offset = bgBounds.size.y + bg2.transform.position.y;
            bg2.transform.position = new Vector3( 0, bgBounds.size.y * 2 + offset, 0 );
        }
    }

    public void onGameOver()
    {
        if( player )
            player.ready();

        if( bg1 )
            bg1.transform.position = Vector3.zero;

        if( bg2 )
            bg2.transform.position = new Vector3( 0, bgBounds.size.y, 0 );

        distance = 0.0f;
    }
}
