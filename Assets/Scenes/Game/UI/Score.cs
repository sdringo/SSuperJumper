using UnityEngine;
using UnityEngine.UI;

public class Score : Entity
{
    public Text score = null;

    private Player player = null;

    public override void initialize()
    {
        base.initialize();

        GameMgr mgr = GameObject.FindWithTag( "GameController" ).GetComponent<GameMgr>();
        mgr.onScroll += scroll;

        player = GameObject.FindWithTag( "Player" ).GetComponent<Player>();
    }

    private void scroll( float distance )
    {
        if( score )
            score.text = string.Format( "{0}", (int)player.Distance );
    }
}
