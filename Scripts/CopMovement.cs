using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopMovement : MonoBehaviour
{
    public float movementTime;
    public float endPositionX;
    public float sleepTime;
    public bool faceRight = true;
    Animator animator;

    float startPositionX;

    private void Start()
    {
        animator = GetComponent<Animator>();
        startPositionX = transform.position.x;
        StartMoving();
    }

    void StartMoving()
    {
        LeanTween.move(gameObject, new Vector2(startPositionX + endPositionX, transform.position.y), movementTime).setDelay(sleepTime)
        .setOnStart(delegate () { FaceItStart(); StartMovingAnimation(); })
        .setOnComplete(delegate ()
        {
            StopMovingAnimation();
            LeanTween.move(gameObject, new Vector2(startPositionX - endPositionX, transform.position.y), movementTime).setDelay(sleepTime)
            .setOnStart(delegate () { FaceItEnd(); StartMovingAnimation(); })
            .setOnComplete(delegate () { StartMoving(); StopMovingAnimation(); });
        });
    }

    void StartMovingAnimation()
    {
        animator.SetBool("Moving", true);
    }

    void StopMovingAnimation()
    {
        animator.SetBool("Moving", false);
    }

    void FaceItStart()
    {
        if (transform.position.x < endPositionX)
        {
            if (!faceRight)
            {
                faceRight = true;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
        if (endPositionX < transform.position.x)
        {
            if (faceRight)
            {
                faceRight = false;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
    }

    void FaceItEnd()
    {
        if (transform.position.x < startPositionX)
        {
            if (!faceRight)
            {
                faceRight = true;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
        if (startPositionX < transform.position.x)
        {
            if (faceRight)
            {
                faceRight = false;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
    }
}
