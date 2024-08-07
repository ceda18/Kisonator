using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationTime;
    public float rotationIntensity;
    public float sleepTime;
    float startRotation;

    private void Start()
    {
        startRotation = transform.localEulerAngles.z;
        StartRotating();
    }

    void StartRotating()
    {
        LeanTween.rotateZ(gameObject, rotationIntensity, rotationTime).setDelay(sleepTime)
        .setOnComplete(delegate ()
        {
            LeanTween.rotateZ(gameObject, startRotation, rotationTime).setDelay(sleepTime)
            .setOnComplete(delegate () { StartRotating(); });
        });
    }
}
