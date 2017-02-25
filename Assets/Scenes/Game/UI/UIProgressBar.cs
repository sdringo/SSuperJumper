using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIProgressBar : Entity
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
        player.onJumpEnChange += jumpEnChange;
        player.onShieldEnChange += shieldEnChange;
        player.onSuperJumpBegin += superJumpBegin;
        player.onSuperJumpEnd += superJumpEnd;

        imgJump = jump.GetComponent<Image>();
        imgJump.fillAmount = player.JumpEN / player.maxEn;
        imgShield = shield.GetComponent<Image>();
        imgShield.fillAmount = player.ShieldEN / player.maxEn;
        imgSuper = super.GetComponent<Image>();
        width = imgJump.preferredWidth;

        rectJump = jump.transform.GetChild( 0 ).GetComponent<RectTransform>();
        rectJump.anchoredPosition = new Vector2( -width * ( 1.0f - imgJump.fillAmount ), 0 );
        rectSheild = shield.transform.GetChild( 0 ).GetComponent<RectTransform>();
        rectSheild.anchoredPosition = new Vector2( width * ( 1.0f - imgShield.fillAmount ), 0 );
    }

    public void jumpEnChange()
    {
        if( imgJump )
            imgJump.DOFillAmount( player.JumpEN / player.maxEn, 0.1f );

        if( rectJump )
            rectJump.DOAnchorPosX( -width * ( 1.0f - player.JumpEN / player.maxEn ), 0.1f );
    }

    public void shieldEnChange()
    {
        if( imgShield )
            imgShield.DOFillAmount( player.ShieldEN / player.maxEn, 0.1f );

        if( rectSheild )
            rectSheild.DOAnchorPosX( width * ( 1.0f - player.ShieldEN / player.maxEn ), 0.1f );
    }

    public void superJumpBegin()
    {
        if( jump )
            jump.SetActive( false );

        if( shield )
            shield.SetActive( false );

        if( super )
            super.SetActive( true );
    }

    public void superJumpEnd()
    {
        if( jump )
            jump.SetActive( true );

        if( shield )
            shield.SetActive( true );

        if( super )
            super.SetActive( false );
    }
}
