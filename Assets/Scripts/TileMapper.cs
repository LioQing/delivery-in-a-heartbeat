using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileMapper : MonoBehaviour
{
    [Multiline]
    public string mapString;
    public List<GameObject> tileSprites;
    public List<GameObject> foodSprites;
    public Vector3 playerPosition;
    public Vector3 exitPosition;
    public int nextMap;
    public GameObject girl;
    public Color background;

    private void CreateFoodObj(int x, int y)
    {
        int rndNo = Random.Range(0, 6);
        var foodSprite = foodSprites[rndNo]; // Chooses a sprite from foodSprites at random
        var foodObject = Instantiate(foodSprite, transform);
        foodObject.transform.position = new Vector3(x, y);
    }

    private void Awake()
    {
        var fileData = mapString;
        var lines = fileData.Split('\n');
        string[] lineData = null;
        int mapEnd = 0; // Probably not a good fix. Open to improvements  -Eric

        for (int y = 0; y < lines.Length; y++)
        {
            lineData = (lines[y].Trim()).Split(' ');
            if (lineData.Length == 0)
                break;

            if (lineData[0] == "-1")
                mapEnd = 1;

            if (mapEnd == 0)
            {
                for (int x = 0; x < lineData.Length; x++) // Read tile map line
                {
                    var tileIndex = int.Parse(lineData[x]);
                    var tileSprite = tileSprites[tileIndex];
                    var tileObject = Instantiate(tileSprite, transform);
                    tileObject.transform.position = new Vector3(x, -y);
                }
            }
            else if (mapEnd == 1 && lineData is not null && lineData.Length != 0)
            {
                if (lineData[0] == "0") // Initialize food positions
                {
                    Debug.Log("Instatiated food, x: " + int.Parse(lineData[1]) + ", y: {0}" + int.Parse(lineData[2]) + " loop: " + y);
                    CreateFoodObj(int.Parse(lineData[1]), -int.Parse(lineData[2]));
                }
                else
                    Debug.Log("Unknown item id. Loop: " + y);
            }
        }

        // spawn girl
        if (nextMap == -1)
        {
            var girlInstance = Instantiate(girl);
            girlInstance.transform.position = exitPosition;
        }

        // camera background color
        Camera.main.backgroundColor = background;
    }
}
