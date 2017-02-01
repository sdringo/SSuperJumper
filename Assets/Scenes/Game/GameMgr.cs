using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Entity
{
    public static Bounds ScreenBounds { get; set; }

    public float respwanDistance = 10.0f;
    public List<BaseObject> respwans = new List<BaseObject>();

    private GameObject bg = null;
    private GameObject flaform = null;
    private Bounds bgBounds;

    private Player player = null;
    private float score = 0.0f;

    private List<BaseObject> objects = new List<BaseObject>();
    private int lastRespwan = 0;
    private Vector3 respwarnPos = Vector3.zero;

    public override void initialize()
    {
        base.initialize();

        Vector3 size = Vector2.zero;
        size.y = Camera.main.orthographicSize * 2.0f;
        size.x = size.y * Screen.width / Screen.height;
        ScreenBounds = new Bounds( Vector3.zero, size );

        bg = transform.FindChild( "Bg" ).gameObject;
        flaform = transform.FindChild( "LaunchPlatform" ).gameObject;
        bgBounds = bg.GetComponent<SpriteRenderer>().bounds;

        player = FindObjectOfType<Player>();
        player.ready();
        player.onScroll = onScroll;
        player.onGameOver = onGameOver;

        score = 0.0f;
        lastRespwan = 0;
        respwarnPos.y = Camera.main.orthographicSize * 1.1f;
    }

    public override void update()
    {
        base.update();

        if( Input.GetKey( KeyCode.Escape ) ) {
            Application.Quit();
        }
    }

    public void onScroll( float distance )
    {
        score += distance;

        int current = (int)( score / respwanDistance );
        if( lastRespwan < current ) {
            lastRespwan = current;

            int index = Random.Range( 0, respwans.Count );

            BaseObject obj = Instantiate<BaseObject>( respwans[index] );
            obj.transform.Translate( respwarnPos );
            obj.onOutBounds = onOutBounds;
            player.onScroll += obj.onScroll;

            objects.Add( obj );
        }

        flaform.transform.Translate( 0, -distance, 0 );
        if( flaform.transform.position.y < bgBounds.min.y - bgBounds.size.y )
            flaform.SetActive( false );

        //bg1.transform.Translate( 0, -distance * 0.1f, 0 );
        //bg2.transform.Translate( 0, -distance * 0.1f, 0 );

        if( bg.transform.position.y < -bgBounds.size.y ) {
            float offset = bgBounds.size.y + bg.transform.position.y;
            bg.transform.position = new Vector3( 0, bgBounds.size.y + offset, 0 );
        }

        //if( bg2.transform.position.y < -bgBounds.size.y ) {
        //    float offset = bgBounds.size.y + bg2.transform.position.y;
        //    bg2.transform.position = new Vector3( 0, bgBounds.size.y + offset, 0 );
        //}
    }

    public void onGameOver()
    {
        if( player )
            player.ready();

        if( flaform ) {
            flaform.SetActive( true );
            flaform.transform.position = new Vector3( 0, ScreenBounds.min.y, 0 );
        }

        //if( bg2 )
        //    bg2.transform.position = new Vector3( 0, bgBounds.size.y, 0 );

        foreach( BaseObject obj in objects ) {
            if( player )
                player.onScroll -= obj.onScroll;

            GameObject.DestroyObject( obj.gameObject );
        }

        objects.Clear();

        score = 0.0f;
        lastRespwan = 0;
    }

    public void onOutBounds( BaseObject obj )
    {
        if( player )
            player.onScroll -= obj.onScroll;

        objects.Remove( obj );

        GameObject.DestroyObject( obj.gameObject );
    }
}
