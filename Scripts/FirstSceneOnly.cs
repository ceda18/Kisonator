using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneOnly : MonoBehaviour
{
    private void Start()
    {
        Function();
    }

    void Function()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, 1).setEaseInOutSine().setDelay(0)
        .setOnComplete(delegate ()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
                gameObject.AddComponent<CanvasGroup>();
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1, 1).setEaseInOutSine().setDelay(2)
            .setOnComplete(delegate ()
            {
                SceneManager.LoadScene("Scene 01");
            });
        });
    }
}
