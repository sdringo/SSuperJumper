using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMgr : SingletonObject<PopupMgr>
{
    public GameObject popupMenu;
    public GameObject popupTutorial;
    public GameObject popupContinue;
    public GameObject popupItem;
    public GameObject popupScore;
    public GameObject popupSkin;

    public void showTutorial()
    {
        Instantiate( popupTutorial, transform, false );
    }

    public void showMenu()
    {
        Instantiate( popupMenu, transform, false );
    }

    public void showContinue()
    {
        Instantiate( popupContinue, transform, false );
    }

    public void showScore()
    {
        Instantiate( popupScore, transform, false );
    }

    public void showSkin()
    {
        Instantiate( popupSkin, transform, false );
    }
}
