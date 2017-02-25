using UnityEngine;
using UnityEngine.UI;

public class UIMenu : Entity
{
    public GameObject objMenu;
    public GameObject objResume;
    public GameObject objPause;

    private bool inGame = false;

    public override void initialize()
    {
        base.initialize();

        GameMgr.instance.onGameStart += gameStart;
        GameMgr.instance.onGameOver += gameMenu;

        gameMenu();
    }

    public void onMenu()
    {
        if( inGame ) {
            if( GameMgr.instance.isPaused ) {
                GameMgr.instance.gameResume();
                gameResume();
            } else {
                GameMgr.instance.gamePause();
                gamePause();
            }
        } else {
            PopupMgr.instance.showMenu();
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
