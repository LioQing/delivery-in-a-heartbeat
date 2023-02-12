using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlBehavior : MonoBehaviour
{
    public Sprite heart;

    private new SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Finished()
    {
        renderer.sprite = heart;
    }
}
