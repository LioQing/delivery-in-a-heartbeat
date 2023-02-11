using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBeat : MonoBehaviour
{
    private Heartbeat heartbeat;

    private void Start()
    {
        heartbeat = GameObject.Find("Heartbeat").GetComponent<Heartbeat>();
        heartbeat.onBeat += () => GetComponent<Camera>().orthographicSize = 2.9f;
    }
    
    private void Update()
    {
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 3f, 0.1f);
    }
}
