using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScore : MonoBehaviour
{
    public Sprite heart;
    public Sprite noHeart;

    void Start()
    {
        if (GameInfo.score == 0)
        {
            GetComponent<TMP_Text>().text = "Oh no, you didn't get any food...";
            GameObject.FindGameObjectWithTag("Girl").GetComponent<SpriteRenderer>().sprite = noHeart;
        }
        else
        {
            GetComponent<TMP_Text>().text = "You got it in a heartbeat!\nYou delivered " + GameInfo.score + " food!";
            GameObject.FindGameObjectWithTag("Girl").GetComponent<SpriteRenderer>().sprite = heart;
        }
        
        GameInfo.currentMap = 0;
        GameInfo.score = 0;
    }
}
