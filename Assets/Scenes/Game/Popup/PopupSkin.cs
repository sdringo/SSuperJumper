using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSkin : Entity
{
    public Image skinImage;
    public Text skinName;
    public Text skinStatus;

    public void onPrev()
    {

    }

    public void onNext()
    {

    }

    public void onOk()
    {
        Destroy( this.gameObject );
    }

    public void onCancel()
    {
        Destroy( this.gameObject );
    }
}
