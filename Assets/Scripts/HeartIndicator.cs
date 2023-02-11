using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartIndicator : MonoBehaviour
{
    public bool isLeft;
    public float time = 5f;
    public float timer;

    private void Start()
    {
        timer = time;
        var xPos = isLeft ? 0 : Screen.width;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        // move towards middle of screen proportional to timer
        var prop = timer / time;
        var halfWidth = Screen.width / 2f;

        var xPos = halfWidth + (isLeft ? -1 : 1) * halfWidth * prop;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        // destroy the object after passing middle of the screen
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
