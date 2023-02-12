using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlBehavior : MonoBehaviour
{
    public Sprite heart;

    private new SpriteRenderer renderer;
    private float endTime;
    private float endTimer;

    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        endTime = 1f;
    }

    private void Update()
    {
        if (endTimer > 0)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
            }
        }
    }

    public void Finished()
    {
        renderer.sprite = heart;
        endTimer = endTime;
    }
}
