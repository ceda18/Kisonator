using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spotlights : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        LeanTween.cancelAll();
        foreach (Animator animator in FindObjectsOfType<Animator>())
            animator.enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("Fade Image").GetComponent<Animation>().FadeIn();
        LeanTween.delayedCall(2, delegate () { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
        PlayerPrefs.SetInt("Fade Image Color", 1);
    }
}
