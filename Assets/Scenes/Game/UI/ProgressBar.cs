using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : Entity
{
    public GameObject jump = null;
    public GameObject shield = null;
    public GameObject super = null;

    private Image imgJump = null;
    private Image imgShield = null;
    private Image imgSuper = null;

    private RectTransform rectJump = null;
    private RectTransform rectSheild = null;

    private float width = 0.0f;

    private Player player = null;

    public override void initialize()
    {
        base.initialize();

        player = FindObjectOfType<Player>();
        player.onValueChange += onChanged;

        imgJump = jump.GetComponent<Image>();
        imgShield = shield.GetComponent<Image>();
        imgSuper = super.GetComponent<Image>();
        width = imgJump.preferredWidth;

        rectJump = jump.transform.GetChild( 0 ).GetComponent<RectTransform>();
        rectSheild = shield.transform.GetChild( 0 ).GetComponent<RectTransform>();
    }

    public void onChanged()
    {
        if( imgJump )
            imgJump.fillAmount = player.JumpEN / player.maxEn;
        
        if( rectJump )
            rectJump.anchoredPosition = new Vector2( -width * ( 1.0f - imgJump.fillAmount ), 0 );

        if( imgShield )
            imgShield.fillAmount = player.ShieldEN / player.maxEn;

        if( rectSheild )
            rectSheild.anchoredPosition = new Vector2( width * ( 1.0f - imgShield.fillAmount ), 0 );
    }
}
