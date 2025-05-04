using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float moveSpeed = 3.5f;  // move speed
    private float targetPositionX = -480f;  // left scroll distance
    private float startPosX;  // starting pos
    private bool movingLeft = true;  // moving left or returning
    private float pauseDuration = 20f;  // how long it pauses
    private float pauseTimer = 0f;  // tracking pause time

    void Start()
    {
        startPosX = transform.position.x;  // grab initial start pos
    }

    void Update()
    {
        if (movingLeft)
        {
            float newPosX = Mathf.MoveTowards(transform.position.x, targetPositionX, moveSpeed * Time.deltaTime); // move left
            transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

            if (transform.position.x == targetPositionX) // when background reaches destination
            {
                movingLeft = false;
                pauseTimer = pauseDuration;  // start timer
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime; // background pause

            if (pauseTimer <= 0f)
            {
                float newPosX = Mathf.MoveTowards(transform.position.x, startPosX, moveSpeed * Time.deltaTime); // return to start pos
                transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

                if (transform.position.x == startPosX) // loop
                {
                    movingLeft = true;
                }
            }
        }
    }
}
