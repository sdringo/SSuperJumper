using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : Entity
{
    public float respwanDistance = 10.0f;
    public EnergyObject objectEnergy = null;
    public EnemyObject objectEnemy = null;
    public JumpObject objectJump = null;

    public static Bounds ScreenBounds { get; set; }

    private GameObject bg1 = null;
    private GameObject bg2 = null;
    private Bounds bgBounds;

    private Player player = null;
    private float score = 0.0f;
    private int lastRespwan = 0;
    private Vector3 respwarnPos = Vector3.zero;

    public override void initialize()
    {
        base.initialize();

        Vector3 size = Vector2.zero;
        size.y = Camera.main.orthographicSize * 2.0f;
        size.x = size.y * Screen.width / Screen.height;
        ScreenBounds = new Bounds( Vector3.zero, size );

        bg1 = transform.GetChild( 0 ).gameObject;
        bg2 = transform.GetChild( 1 ).gameObject;

        bgBounds = bg1.GetComponent<SpriteRenderer>().bounds;
        bg2.transform.Translate( 0, bgBounds.size.y, 0 );

        player = GameObject.FindObjectOfType<Player>();
        player.ready();
        player.onScroll = onScroll;
        player.onGameOver = onGameOver;

        score = 0.0f;
        lastRespwan = 0;
        respwarnPos.y = Camera.main.orthographicSize * 1.1f;
    }

    public void onScroll( float distance )
    {
        score += distance;

        int current = (int)( score / respwanDistance );
        if( lastRespwan < current ) {
            lastRespwan = current;

            BaseObject obj = Instantiate<EnergyObject>( objectEnergy );
            obj.transform.Translate( respwarnPos );
            obj.onOutBounds = onOutBounds;
            player.onScroll += obj.onScroll;
        }

        bg1.transform.Translate( 0, -distance * 0.1f, 0 );
        bg2.transform.Translate( 0, -distance * 0.1f, 0 );

        if( bg1.transform.position.y < -bgBounds.size.y ) {
            float offset = bgBounds.size.y + bg1.transform.position.y;
            bg1.transform.position = new Vector3( 0, bgBounds.size.y + offset, 0 );
        }

        if( bg2.transform.position.y < -bgBounds.size.y ) {
            float offset = bgBounds.size.y + bg2.transform.position.y;
            bg2.transform.position = new Vector3( 0, bgBounds.size.y + offset, 0 );
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

        score = 0.0f;
    }

    public void onOutBounds( BaseObject obj )
    {
        if( player )
            player.onScroll -= obj.onScroll;

        GameObject.DestroyImmediate( obj.gameObject );
    }
}
