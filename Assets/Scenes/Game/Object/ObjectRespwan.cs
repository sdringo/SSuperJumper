using System.Collections.Generic;
using UnityEngine;

public class ObjectRespwan : Entity
{
    public List<BaseObject> list = new List<BaseObject>();
    public uint respwanMin = 8;
    public uint respwanMax = 8;

    private GameMgr gameMgr = null;
    private List<BaseObject> objects = new List<BaseObject>();

    private float total = 0.0f;
    private float last = 0;
    private Vector3 pos = Vector3.zero;

    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        list.Sort( delegate ( BaseObject a, BaseObject b ) {
            if( a.percent < b.percent )
                return -1;
            if( a.percent > b.percent )
                return 1;
            else
                return 0;
        } );

        total = 0.0f;
        last = Well512.Next( respwanMin, respwanMax );
        pos.y = Camera.main.orthographicSize * 1.1f;

        gameMgr.onScroll += scroll;
        gameMgr.onGameOver += reset;
    }

    public void reset()
    {
        foreach( BaseObject obj in objects ) {
            gameMgr.onScroll -= obj.scroll;

            DestroyObject( obj.gameObject );
        }

        objects.Clear();

        total = 0.0f;
        last = 0;
    }

    private void scroll( float offset )
    {
        total += offset;
        if( last < total ) {
            last = total + Well512.Next( respwanMin, respwanMax );

            float r = Well512.Next( 10000 ) / (float)10000;
            float cumulative = 0.0f;

            foreach( BaseObject row in list ) {
                cumulative += row.percent;

                if( r <= cumulative ) {
                    BaseObject obj = Instantiate<BaseObject>( row, pos, Quaternion.identity );
                    obj.onOutBounds = remove;
                    gameMgr.onScroll += obj.scroll;

                    objects.Add( obj );

                    break;
                }   
            }
        }
    }

    private void remove( BaseObject obj )
    {
        gameMgr.onScroll -= obj.scroll;

        objects.Remove( obj );

        GameObject.DestroyObject( obj.gameObject );
    }
}
