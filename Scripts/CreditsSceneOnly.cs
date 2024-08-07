using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneOnly : MonoBehaviour
{
    private void Start()
    {
        Function();
    }

    private void Update()
    {
        WaitForSpace();
    }

    void Function()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, 1).setEaseInOutSine().setDelay(0);
    }

    void WaitForSpace()
    {
        if (Input.GetKey("space"))
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
                gameObject.AddComponent<CanvasGroup>();
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1, 1).setEaseInOutSine().setDelay(0)
            .setOnComplete(delegate ()
            {
                SceneManager.LoadScene("Scene 00");
            });
        }
    }
}
