using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : Entity
{
    public GameObject textSoundOn;
    public GameObject textSoundOff;
    public GameObject textMusicOn;
    public GameObject textMusicOff;

    private bool soundOn = false;
    private bool musicOn = false;
    
    public override void initialize()
    {
        base.initialize();

        soundOn = true;
        musicOn = true;

        textSoundOn.SetActive( soundOn );
        textSoundOff.SetActive( !soundOn );
        textMusicOn.SetActive( musicOn );
        textMusicOff.SetActive( !musicOn );
    }

    public void onSound()
    {
        soundOn = !soundOn;

        textSoundOn.SetActive( soundOn );
        textSoundOff.SetActive( !soundOn );
    }

    public void onMusic()
    {
        musicOn = !musicOn;

        textMusicOn.SetActive( musicOn );
        textMusicOff.SetActive( !musicOn );
    }

    public void onRank()
    {

    }
    
    public void onSNS()
    {

    }

    public void onAbout()
    {

    }

    public void onHelp()
    {

    }

    public void onReturn()
    {
        Destroy( gameObject );
    }
}
