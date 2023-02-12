using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TileMapper : MonoBehaviour
{
    public string mapFilePath;
    public List<GameObject> tileSprites;
    public Vector3 playerPosition;
    public Vector3 exitPosition;
    public int nextMap;

    private void Awake()
    {
        var fullPath = $"{Application.streamingAssetsPath}/{mapFilePath}";

        if (!File.Exists(fullPath))
        {
            Debug.LogError("Map file not found at " + fullPath);
            return;
        }

        var fileData = File.ReadAllText(fullPath);
        var lines = fileData.Split('\n');
        string[] lineData = null;

        for (int y = 0; y < lines.Length; y++)
        {
            lineData = (lines[y].Trim()).Split(' ');
            if (lineData.Length == 0 || lineData[0] == "-1")
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
    }
}
