using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score;
    public Sprite standSprite;
    public Sprite stunnedSprite;

    private Heartbeat heartbeat;
    private SpriteRenderer renderer;
    private TMP_Text text;
    private TMP_Text scoreText;
    private bool stunned;
    private Vector3 stunBackPos;

    private void Start()
    {
        heartbeat = GameObject.Find("Heartbeat").GetComponent<Heartbeat>();
        text = GameObject.Find("BeatText").GetComponent<TMP_Text>();
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        score = 0;
        stunned = false;
    }
    
    private void Update()
    {
        // stun lerp and reset
        if (stunned)
        {
            transform.position = Vector3.Lerp(transform.position, stunBackPos, 0.1f);
            if ((transform.position - stunBackPos).magnitude < 0.05f)
            {
                stunned = false;
                renderer.sprite = standSprite;
                transform.position = stunBackPos;
            }
            return;
        }
        
        // move
        var move = Vector3.zero;
        var moved = false;
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            move += new Vector3(0, 1);
            moved = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            move += new Vector3(0, -1);
            moved = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            move += new Vector3(-1, 0);
            moved = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            move += new Vector3(1, 0);
            moved = true;
        }

        if (moved)
        {
            if (heartbeat.IsBeat() && moved)
            {
                transform.Translate(move);
                text.color = Color.red;
                text.text = "Beat!";
            }
            else if (moved)
            {
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        move = new Vector3(0, 1);
                        break;
                    case 1:
                        move = new Vector3(0, -1);
                        break;
                    case 2:
                        move = new Vector3(-1, 0);
                        break;
                    case 3:
                        move = new Vector3(1, 0);
                        break;
                }

                transform.position += move;
                text.color = Color.white;
                text.text = "Missed!";
            }
            
            // food
            var food = GameObject.FindGameObjectsWithTag("Food");
            foreach (var f in food)
            {
                if (f.transform.position == transform.position)
                {
                    Destroy(f);
                    score++;
                    scoreText.text = $"No. of Food: {score}";
                    break;
                }
            }

            // walls
            var walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (var w in walls)
            {
                if (w.transform.position == transform.position)
                {
                    stunBackPos = w.transform.position - move;
                    transform.position -= move * 0.01f;
                    stunned = true;
                    renderer.sprite = stunnedSprite;
                    break;
                }
            }
        }
    }
}
