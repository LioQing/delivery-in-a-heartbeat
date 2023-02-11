using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    public float interval = 1f;
    public float allowance = 0.1f;
    public float timer;
    public bool started;
    
    private void Start()
    {
        if (interval < allowance)
        {
            Debug.LogError("Heartbeat interval is too small");
        }
        
        started = false;
    }

    private void Update()
    {
        if (!started) return;
        
        timer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (timer <= 0)
        {
            timer += interval;
        }
    }

    public bool IsBeat()
    {
        return started && (timer <= allowance / 2f || timer >= interval - allowance / 2f && timer <= interval);
    }
    
    public void StartFirstAfter(float seconds)
    {
        started = true;
        timer = seconds;
    }
}
