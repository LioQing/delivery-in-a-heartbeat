using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    public float interval = 1f;
    public float allowance = 0.1f;
    public float timer;
    public bool started;

    public delegate void OnBeat();
    public OnBeat onBeat;
    private bool onBeatInvoked;
    
    private void Start()
    {
        if (interval < allowance)
        {
            Debug.LogError("Heartbeat interval is too small");
        }
        
        started = false;
        onBeatInvoked = false;
    }

    private void Update()
    {
        if (!started) return;
        
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            timer += interval;
        }
        else if (timer <= allowance / 2f && !onBeatInvoked)
        {
            onBeat?.Invoke();
            onBeatInvoked = true;
        }
        else if (timer > allowance)
        {
            onBeatInvoked = false;
        }
    }

    public bool IsBeat()
    {
        return started && timer <= allowance;
    }

    public void StartFirstAfter(float seconds)
    {
        started = true;
        timer = seconds;
    }
}
