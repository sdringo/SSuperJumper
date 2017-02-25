using UnityEngine;
using UnityEngine.UI;

public class PopupContinue : Entity
{
    public Text textCount;
    public GameObject buttonWide;

    public override void initialize()
    {
        base.initialize();

        int life = GameMgr.instance.Life;

        textCount.text = life.ToString();
        textCount.color = 0 < life ? new Color32( 236, 0, 140, 255 ) : new Color32( 149, 149, 149, 255 );

        buttonWide.SetActive( 0 >= life );
    }

    public void onYes()
    {
        GameMgr.instance.gameRestart();

        Destroy( gameObject );
    }

    public void onNo()
    {
        PopupMgr.instance.showScore();

        Destroy( gameObject );
    }

    public void onShowAd()
    {
        Destroy( gameObject );
    }
}
