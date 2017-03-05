using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Splash : Entity
{
    public override void initialize()
    {
        base.initialize();

        Sequence actions = DOTween.Sequence();
        actions.AppendInterval( 2.0f );
        actions.AppendCallback( () => {
            SceneManager.LoadSceneAsync( "Game", LoadSceneMode.Single );
        } );
    }
}
