using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded = true;
    public float movementSpeed = 8;
    public Animator playerAnimator;

    GameObject lastPlatformTouchedGO;
    bool isTouchingPlatform = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovementControl();
        jumpLimit();
    }

    void MovementControl()
    {
        if (Input.GetKey("1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
        if (Input.GetKey("2"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("up") || Input.GetKeyDown("space"))
        {
            jump();
            playerAnimator.SetBool("Jump", true);
        }

        if (Input.GetKey("right"))
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            playerAnimator.SetFloat("Speed", movementSpeed);
        }

        if (Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            transform.rotation = new Quaternion(0, 180, 0, 0);
            playerAnimator.SetFloat("Speed", movementSpeed);
        }

        if (!Input.GetKey("right") && !Input.GetKey("left"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            playerAnimator.SetFloat("Speed", 0);
        }

        if (Input.GetKey("down"))
        {
            if (isTouchingPlatform)
            {
                lastPlatformTouchedGO.GetComponent<Collider2D>().enabled = false;
                playerAnimator.SetBool("Jump", true);
                isGrounded = false;
                LeanTween.delayedCall(0.5f, delegate ()
                {
                    foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
                    {
                        platform.GetComponent<Collider2D>().enabled = true;
                    }
                    // lastPlatformTouchedGO.GetComponent<Collider2D>().enabled = true;
                });
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Reset Jump When On Ground
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform"))
        {
            playerAnimator.SetBool("Jump", false);
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            lastPlatformTouchedGO = other.gameObject;
            isTouchingPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isTouchingPlatform = false;
        }
    }

    void jump()
    {
        if (isGrounded)
        {
            rb.velocity += Vector2.up * 15;
            isGrounded = false;
        }
    }

    void jumpLimit()
    {
        if (rb.velocity.y > 15)
        {
            rb.velocity = new Vector2(rb.velocity.x, 15);
        }
    }
}
