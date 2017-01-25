using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : Entity
{
    public float scrollSpeed = 0.01f;

    private GameObject bg1 = null;
    private GameObject bg2 = null;
    private Bounds bgBounds;

    private Bounds scrollBounds;
    private bool scroll = false;

    private Player player = null;

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
    }

    public override void update()
    {
        base.update();

        if( scroll ) {
            bg1.transform.Translate( 0, -scrollSpeed, 0 );
            bg2.transform.Translate( 0, -scrollSpeed, 0 );

            if( bg1.transform.position.y < -bgBounds.size.y )
                bg1.transform.Translate( 0, bgBounds.size.y * 2, 0 );

            if( bg2.transform.position.y < -bgBounds.size.y )
                bg2.transform.Translate( 0, bgBounds.size.y * 2, 0 );
        }
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

    public void onScroll( bool scroll )
    {
        this.scroll = scroll;
    }

    public void onGameOver()
    {
        if( player )
            player.ready();

        if( bg1 )
            bg1.transform.position = Vector3.zero;

        if( bg2 )
            bg2.transform.position = new Vector3( 0, bgBounds.size.y, 0 );
    }
}
