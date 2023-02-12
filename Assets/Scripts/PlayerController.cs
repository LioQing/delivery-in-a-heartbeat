using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score;

    private Heartbeat heartbeat;
    private TMP_Text text;
    private TMP_Text scoreText;

    private void Start()
    {
        heartbeat = GameObject.Find("Heartbeat").GetComponent<Heartbeat>();
        text = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        score = 0;
    }
    
    private void Update()
    {
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
                switch (Random.Range(0, 4))
                {
                    case 0:
                        transform.position += new Vector3(0, 1);
                        break;
                    case 1:
                        transform.position += new Vector3(0, -1);
                        break;
                    case 2:
                        transform.position += new Vector3(-1, 0);
                        break;
                    case 3:
                        transform.position += new Vector3(1, 0);
                        break;
                }

                text.color = Color.white;
                text.text = "Missed!";
            }
            
            var food = GameObject.FindGameObjectsWithTag("Food");
            foreach (var f in food)
            {
                if (f.transform.position == transform.position)
                {
                    Destroy(f);
                    score++;
                    scoreText.text = $"No. of Food: {score}";
                }
            }
        }
    }
}
