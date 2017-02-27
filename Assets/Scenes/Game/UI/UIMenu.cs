using UnityEngine;
using UnityEngine.UI;

public class UIMenu : Entity
{
    public GameObject objMenu;
    public GameObject objResume;
    public GameObject objPause;

    private GameMgr gameMgr = null;
    private bool inGame = false;

    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        gameMgr.onGameStart += gameStart;
        gameMgr.onGameOver += gameMenu;

        gameMenu();
    }

    public void onMenu()
    {
        if( inGame ) {
            if( gameMgr.isPaused ) {
                gameMgr.gameResume();

                if( objResume )
                    objResume.SetActive( false );

                if( objPause )
                    objPause.SetActive( true );
            } else {
                gameMgr.gamePause();

                if( objResume )
                    objResume.SetActive( true );

                if( objPause )
                    objPause.SetActive( false );
            }
        } else {
            gameMgr.showUI( "Prefabs/Popup/Menu" );
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
}
