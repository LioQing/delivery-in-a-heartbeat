using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite standSprite;
    public Sprite stunnedSprite;

    private Heartbeat heartbeat;
    private new SpriteRenderer renderer;
    private TMP_Text text;
    private TMP_Text scoreText;
    private bool stunned;
    private Vector3 stunBackPos;
    private string scoreColor;

    private void Start()
    {
        heartbeat = GameObject.Find("Heartbeat").GetComponent<Heartbeat>();
        text = GameObject.Find("BeatText").GetComponent<TMP_Text>();
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        stunned = false;
        
        
        scoreText.text = $"No. of Food: {GameInfo.score}";
    }
    
    private void Update()
    {
        scoreColor = "white";
        // stun lerp and reset
        if (stunned)
        {
            transform.position = Vector3.Lerp(transform.position, stunBackPos, Time.deltaTime * 2f);
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
            if (heartbeat.IsBeat())
            {
                text.color = Color.red;
                text.text = "Beat!";
            }
            else
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
                
                text.color = Color.white;
                text.text = "Missed!";
            }
            
            // girl
            var girl = GameObject.FindGameObjectWithTag("Girl");
            if (girl is not null && transform.position + move == girl.transform.position)
            {
                girl.GetComponent<GirlBehavior>().Finished();
                return;
            }
            
            transform.Translate(move);

            // food
            var food = GameObject.FindGameObjectsWithTag("Food");
            foreach (var f in food)
            {
                if (f.transform.position == transform.position)
                {
                    Destroy(f);
                    GameInfo.score++;
                    scoreText.text = $"No. of Food: {GameInfo.score}";
                    scoreColor = "yellow";
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
                    if (GameInfo.score > 0)
                        GameInfo.score--;
                    scoreText.text = $"No. of Food: {GameInfo.score}";
                    scoreColor = "red";
                    break;
                }
            }
        }
        if (scoreColor == "yellow")
        {
            scoreText.color = Color.yellow;
            Debug.Log("Color yellow!");
        }
        else if (scoreColor == "red")
            scoreText.color = Color.red;
        else
            scoreText.color = Color.white;
    }
}
