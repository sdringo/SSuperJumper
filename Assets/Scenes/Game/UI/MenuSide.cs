using UnityEngine;
using UnityEngine.UI;

public class MenuSide : Entity
{
    public GameObject objMenu;
    public GameObject objResume;
    public GameObject objPause;

    private bool inGame = false;

    public override void initialize()
    {
        base.initialize();

        GameMgr gameMgr = GameObject.FindWithTag( "GameController" ).GetComponent<GameMgr>();
        gameMgr.onGameStart += gameStart;
        gameMgr.onGameOver += gameMenu;

        gameMenu();
    }

    public void onMenu()
    {
        if( inGame ) {
            GameMgr gameMgr = GameObject.FindWithTag( "GameController" ).GetComponent<GameMgr>();
            if( gameMgr.isPaused ) {
                gameMgr.gameResume();
                gameResume();
            } else {
                gameMgr.gamePause();
                gamePause();
            }
        } else {
            
        }
    }

    public void gameMenu()
    {
        inGame = false;

        if( objMenu )
            objMenu.SetActive( true );

        if( objResume )
            objResume.SetActive( false );

        if( objPause )
            objPause.SetActive( false );
    }

    public void gameStart()
    {
        inGame = true;

        if( objMenu )
            objMenu.SetActive( false );

        if( objResume )
            objResume.SetActive( false );

        if( objPause )
            objPause.SetActive( true );
    }

    public void gameResume()
    {
        if( objResume )
            objResume.SetActive( false );

        if( objPause )
            objPause.SetActive( true );
    }

    public void gamePause()
    {
        if( objResume )
            objResume.SetActive( true );

        if( objPause )
            objPause.SetActive( false );
    }
}
