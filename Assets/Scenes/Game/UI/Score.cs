using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score : Entity
{
    public Text score = null;

    private Player player = null;

    public override void initialize()
    {
        base.initialize();

        player = FindObjectOfType<Player>();
        player.onValueChange += onChanged;
    }

    private void onChanged()
    {
        if( score )
            score.text = string.Format( "{0}", (int)player.Distance );
	}
}
