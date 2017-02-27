using System.Collections.Generic;
using UnityEngine;

public class PopupTutorial : Entity
{
    public List<GameObject> pages = new List<GameObject>();

    private int index = 0;

    public override void initialize()
    {
        base.initialize();

        index = 0;
    }

    public void onPrev()
    {
        pages[index].SetActive( false );

        index--;

        pages[index].SetActive( true );
    }

    public void onNext()
    {
        pages[index].SetActive( false );

        index++;

        pages[index].SetActive( true );
    }

    public void onOk()
    {
        Destroy( gameObject );
    }
}
