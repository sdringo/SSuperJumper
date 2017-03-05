using UnityEngine;
using DG.Tweening;

public class AppMgr : SingletonObject<AppMgr>
{
    public override void initialize()
    {
        base.initialize();

        DOTween.Init( false, true, LogBehaviour.ErrorsOnly );
    }

    public override void update()
    {
        base.update();

        if( Input.GetKey( KeyCode.Escape ) ) {
            Application.Quit();
        }
    }
}
