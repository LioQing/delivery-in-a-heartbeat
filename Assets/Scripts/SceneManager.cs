using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> mappers;
    public GameObject player;
    public TileMapper mapper;
    public bool isLoading;

    private void Start()
    {
        mapper = Instantiate(mappers[GameInfo.currentMap]).GetComponent<TileMapper>();
        player = GameObject.Find("Player");
        player.transform.position = mapper.playerPosition;
        isLoading = false;

        if (GameInfo.currentMap == 0)
        {
            GameInfo.score = 0;
        }
    }
    
    private void Update()
    {
        if (player.transform.position == mapper.exitPosition && !isLoading && mapper.nextMap > 0)
        {
            isLoading = true;
            LoadMap(mapper.nextMap);
        }
    }

    public void LoadMap(int map)
    {
        GameInfo.currentMap = map;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
}
