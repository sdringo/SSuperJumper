using UnityEngine;
using UnityEngine.UI;

public class PopupScore : Entity
{
    public RectTransform icon;
    public RectTransform score;

    public Text textScore;
    public Text textBest;

    private GameMgr gameMgr = null;

    public void setup( GameMgr mgr )
    {
        gameMgr = mgr;
        if( !gameMgr )
            return;

        textScore.text = string.Format( "{0}", (int)gameMgr.Score );

        bool isBest = gameMgr.BestScore < gameMgr.Score;
        if( isBest ) {
            gameMgr.BestScore = gameMgr.Score;

            icon.anchoredPosition = new Vector2( -160, -267 );
            score.anchoredPosition = new Vector2( -70, -260 );
        } else {
            icon.anchoredPosition = new Vector2( -65, -267 );
            score.anchoredPosition = new Vector2( 20, -260 );

            textBest.text = gameMgr.BestScore.ToString();
        }
    }

    public void onRank()
    {

    }

    public void onViewAd()
    {

    }

    public void onReturn()
    {
        if( gameMgr )
            gameMgr.gameOver();

        Destroy( this.gameObject );
    }
}
