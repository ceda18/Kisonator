using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [Header("Start and end animation")]
    public bool pop;
    public bool fade;
    public bool fadeInAlpha;
    public bool fadeTMP;
    public bool moveY;

    [Header("Animate In")]
    public bool animateIn = false;
    public float animateInDuration = 0.5f;
    public float animateInDelay = 0;
    [Header("Animate Out")]
    public bool animateOutSettingsAreSameAsAnimateInSettings = true;
    public bool animateOut = false;
    public float animateOutDuration = 0.5f;
    public float animateOutDelay = 0;

    [Header("Constant animations")]
    [Header("Popping")]
    public bool popping;
    public float poppingIntensity;
    public float poppingTime;
    [Header("Up Down")]
    public bool upDown;
    public float upIntensity;
    public float upTime;
    [Header("Left Right")]
    public bool right;
    public float rightIntensity;
    public float rightTime;
    [Header("Rotate")]
    public bool rotate;
    public float rotateIntensity;
    public float rotateTime;


    private void Awake()
    {
        if (animateOutSettingsAreSameAsAnimateInSettings)
        {
            animateIn = animateOut;
            animateInDuration = animateOutDuration;
            animateInDelay = animateOutDelay;
        }
        AnimateIn();
        // Sacekaj da se uvodne animacije zavrse i onda kreni sa konstantnim animacijama
        // LeanTween.delayedCall(animateInDuration + animateInDelay, delegate () { AnimateConstantly(); });
        AnimateConstantly();
    }

    public static void SecureAnimationControllerComponent(GameObject gameObject)
    {
        if (gameObject.GetComponent<Animation>() == null)
            gameObject.AddComponent<Animation>();
    }

    public void AnimateIn()
    {
        if (pop)
            PopUp();
        if (fade)
            FadeIn();
        if (fadeInAlpha)
            FadeInAlpha();
        if (fadeTMP)
            FadeInTMP();
        if (moveY)
            MoveYIn();
    }

    public void AnimateConstantly()
    {
        LeanTween.delayedCall(animateInDuration + animateInDelay, delegate ()
        {
            if (popping)
                Popping();
            if (upDown)
                Up();
            if (right)
                Right();
        });
        if (rotate)
            Rotate();
    }

    public void AnimateOut()
    {
        LeanTween.cancel(gameObject);
        if (pop)
            PopOut();
        if (fade)
            FadeOut();
        if (fadeTMP)
            FadeOutTMP();
        if (moveY)
            MoveYOut();
    }

    #region Pop

    public void PopUp()
    {
        if (animateIn)
        {
            gameObject.transform.localScale = Vector2.zero;
            LeanTween.scale(gameObject, Vector2.one, animateInDuration).setFrom(Vector2.zero).setEaseOutBack().setDelay(animateInDelay);
        }
    }

    public void PopOut()
    {
        if (animateOut)
            LeanTween.scale(gameObject, Vector2.zero, animateInDuration).setFrom(Vector2.one).setEaseInBack().setDelay(animateInDelay);
    }

    #endregion

    #region Fade

    public void FadeInAlpha()
    {
        if (animateIn)
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
                gameObject.AddComponent<CanvasGroup>();
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, animateInDuration).setEaseInOutSine().setDelay(animateInDelay);
        }
    }

    public void FadeIn()
    {
        if (animateIn)
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
                gameObject.AddComponent<CanvasGroup>();
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1, animateInDuration).setEaseInOutSine().setDelay(animateInDelay);
        }
    }

    public void FadeOut()
    {
        if (animateOut)
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
                gameObject.AddComponent<CanvasGroup>();
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, animateOutDuration).setEaseInOutSine().setDelay(animateOutDelay);
        }
    }

    #endregion

    #region FadeTMP

    public void FadeInTMP()
    {
        if (animateIn)
        {
            if (gameObject.GetComponent<TextMeshProUGUI>() == null)
                Debug.Log("No TextMeshProUGUI found!");
            gameObject.GetComponent<TextMeshProUGUI>().color = new Vector4(gameObject.GetComponent<TextMeshProUGUI>().color.r, gameObject.GetComponent<TextMeshProUGUI>().color.g, gameObject.GetComponent<TextMeshProUGUI>().color.b, 0);

            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1, animateInDuration).setEaseInOutSine().setDelay(animateInDelay);
        }
    }

    public void FadeOutTMP()
    {
        if (animateOut)
        {
            if (gameObject.GetComponent<TextMeshProUGUI>() == null)
                Debug.Log("No TextMeshProUGUI found!");

            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, animateOutDuration).setEaseInOutSine().setDelay(animateOutDelay);
        }
    }

    #endregion

    #region MoveY

    public void MoveYIn()
    {
        if (animateIn)
        {
            LeanTween.moveY(gameObject, gameObject.transform.localPosition.y, animateInDuration).setFrom(gameObject.transform.position.y - 100).setEaseInOutSine().setDelay(animateInDelay);
        }
    }

    public void MoveYOut()
    {
        if (animateOut)
        {
            LeanTween.moveY(gameObject, gameObject.transform.position.y - 100, animateOutDuration * 1).setEaseInOutSine().setDelay(animateOutDelay);
        }
    }

    #endregion



    #region Popping

    public void Popping()
    {
        LeanTween.scale(gameObject, (poppingIntensity * Vector2.one), poppingTime).setFrom(gameObject.transform.localScale).setEaseInOutCubic().setLoopPingPong();
    }

    #endregion

    #region UpDown

    public void Up()
    {
        LeanTween.moveY(gameObject, gameObject.transform.position.y + upIntensity, upTime).setEaseInOutCubic().setLoopPingPong();
    }

    #endregion

    #region LeftRight


    public void Right()
    {
        LeanTween.moveX(gameObject, gameObject.transform.position.x + rightIntensity, rightTime).setEaseInOutCubic().setLoopPingPong();
    }

    #endregion

    #region Rotate


    public void Rotate()
    {
        LeanTween.rotateZ(gameObject, gameObject.transform.rotation.z + rotateIntensity, rotateTime).setFrom(gameObject.transform.rotation.z - rotateIntensity).setEaseInOutCubic().setLoopPingPong();
    }

    #endregion
}
