using UnityEngine;
using UnityEngine.UI;

public class PopupContinue : Entity
{
    public Text textCount;
    public GameObject buttonWide;

    private GameMgr gameMgr = null;

    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        int life = gameMgr.Life;

        textCount.text = life.ToString();
        textCount.color = 0 < life ? new Color32( 236, 0, 140, 255 ) : new Color32( 149, 149, 149, 255 );

        buttonWide.SetActive( 0 >= life );
    }

    public void onYes()
    {
        if( gameMgr )
            gameMgr.gameRestart();

        Destroy( gameObject );
    }

    public void onNo()
    {
        if( gameMgr )
            gameMgr.showUI( "Prefabs/Popup/Score" ).GetComponent<PopupScore>().setup( gameMgr );

        Destroy( gameObject );
    }

    public void onShowAd()
    {
        Destroy( gameObject );
    }
}
