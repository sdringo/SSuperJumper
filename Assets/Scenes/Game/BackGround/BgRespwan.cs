using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BgRespwan : Entity
{
    public GameObject prefabPlanet;
    public GameObject[] prefabComets;
    public float cometTime;
    public GameObject[] prefabStars;
    public float starRefresh;

    private GameMgr gameMgr = null;
    private Sequence actions = null;
    private BgPlanet[] planets = new BgPlanet[2];
    private List<BgStar> stars = new List<BgStar>();
    private List<BgStar> temps = new List<BgStar>();
    private float distance = 0.0f;

    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        gameMgr.onScroll += scroll;
        gameMgr.onGameOver += clear;

        createComet();

        GameObject go = Instantiate<GameObject>( prefabPlanet, gameMgr.transform, false );
        planets[0] = go.GetComponent<BgPlanet>();

        for( int i = 0; i < 10; ++i ) {
            go = Instantiate<GameObject>( prefabStars[i % prefabStars.Length], gameMgr.transform, false );
            stars.Add( go.GetComponent<BgStar>() );
        }
    }

    private void clear()
    {
        for( int i = 0; i < 2; ++i ) {
            if( !planets[i] )
                continue;

            Destroy( planets[i].gameObject );
            planets[i] = null;
        }

        foreach( BgStar star in stars ) {
            Destroy( star.gameObject );
        }

        stars.Clear();
        temps.Clear();

        if( null != actions ) {
            actions.Kill();
            actions = null;
        }   

        distance = 0.0f;
    }

    private void scroll( float offset )
    {
        createPlanets();

        for( int i = 0; i < 2; ++i ) {
            if( !planets[i] )
                continue;

            planets[i].scroll( offset );
            if( planets[i].checkOutBounds() ) {
                Destroy( planets[i].gameObject );
                planets[i] = null;
            }
        }

        distance += offset;
        if( starRefresh < distance ) {
            distance = 0.0f;
            createStar();
        }

        foreach( BgStar star in stars ) {
            star.scroll( offset );

            if( star.checkOutBounds() )
                temps.Add( star );
        }

        foreach( BgStar star in temps ) {
            Destroy( star.gameObject );
            stars.Remove( star );
        }

        temps.Clear();
    }

    private void createPlanets()
    {
        if( !planets[0] ) {
            GameObject go = Instantiate<GameObject>( prefabPlanet, gameMgr.transform, false );
            planets[0] = go.GetComponent<BgPlanet>();
            planets[0].create( 1 );

            return;
        }

        if( planets[0].transform.position.y < 0 && !planets[1] ) {
            GameObject go = Instantiate<GameObject>( prefabPlanet, gameMgr.transform, false );
            planets[1] = go.GetComponent<BgPlanet>();
            planets[1].create( -1 );
        }
    }

    private void createStar()
    {
        int index = (int)Well512.Next( (uint)prefabStars.Length );

        GameObject go = Instantiate<GameObject>( prefabStars[index], gameMgr.transform, false );
        BgStar star = go.GetComponent<BgStar>();
        star.create( 0 );

        stars.Add( star );
    }

    private void createComet()
    {
        actions = DOTween.Sequence();
        actions.AppendInterval( cometTime );
        actions.AppendCallback( () => {
            createComet();
        } );

        int index = (int)Well512.Next( (uint)prefabComets.Length );

        Instantiate<GameObject>( prefabComets[index], gameMgr.transform, false ).GetComponent<BgComet>().create( 0 );
    }
}
