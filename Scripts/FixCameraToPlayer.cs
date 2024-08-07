using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCameraToPlayer : MonoBehaviour
{
    public GameObject player;
    public float limitX;
    public float limitY;
    float minPositionX;
    float maxPositionX;
    float minPositionY;
    float maxPositionY;
    public float yIncrease = 2f;

    private void Start()
    {
        minPositionX = -Mathf.Abs(limitX);
        maxPositionX = Mathf.Abs(limitX);
        minPositionY = -Mathf.Abs(limitY);
        maxPositionY = Mathf.Abs(limitY);
    }

    private void Update()
    {
        if (player.transform.position.x > minPositionX && player.transform.position.x < maxPositionX)
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if (player.transform.position.y + yIncrease > minPositionY && player.transform.position.y + yIncrease < maxPositionY)
            transform.position = new Vector3(transform.position.x, player.transform.position.y + yIncrease, transform.position.z);
    }
}
