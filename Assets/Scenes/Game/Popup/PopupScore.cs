using UnityEngine;
using UnityEngine.UI;

public class PopupScore : Entity
{
    public RectTransform icon;
    public RectTransform score;

    public Text textScore;
    public Text textBest;

    private bool isBest = false;

    public override void initialize()
    {
        base.initialize();

        if( isBest ) {
            icon.anchoredPosition = new Vector2( -70, -260 );
            score.anchoredPosition = new Vector2( -160, -267 );
        } else {
            icon.anchoredPosition = new Vector2( 20, -260 );
            score.anchoredPosition = new Vector2( -65, -267 );
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
        GameMgr.instance.gameOver();

        Destroy( this.gameObject );
    }
}
