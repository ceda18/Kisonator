using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            LeanTween.cancelAll();
            foreach (Animator animator in FindObjectsOfType<Animator>())
                animator.enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

            GameObject.FindGameObjectWithTag("Fade Image").GetComponent<Animation>().FadeIn();
            LeanTween.delayedCall(2, delegate ()
            {
                PlayerPrefs.SetInt("Fade Image Color", 0);
                SceneManager.LoadScene(sceneName);
            });
        }
    }
}
