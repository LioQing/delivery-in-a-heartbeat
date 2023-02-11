using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartNote : MonoBehaviour
{
    public float indicatorTime = 5f;
    public float timer;
    public GameObject indicator;

    private Heartbeat heartbeat;

    private void Start()
    {
        heartbeat = GameObject.Find("Heartbeat").GetComponent<Heartbeat>();
        if (heartbeat.started) return;

        heartbeat.StartFirstAfter(indicatorTime);
        timer = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += heartbeat.interval;
            SpawnIndicators();
        }
    }

    private void SpawnIndicators()
    {
        var left = Instantiate(indicator, transform).GetComponent<HeartIndicator>();
        var right = Instantiate(indicator, transform).GetComponent<HeartIndicator>();

        left.isLeft = true;
        left.time = indicatorTime;
        right.time = indicatorTime;
    }
}
