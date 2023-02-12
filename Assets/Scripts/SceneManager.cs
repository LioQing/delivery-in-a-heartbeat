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
        mapper = Instantiate(mappers[SceneInfo.currentMap]).GetComponent<TileMapper>();
        player = GameObject.Find("Player");
        player.transform.position = mapper.playerPosition;
        isLoading = false;
    }
    
    private void Update()
    {
        if (player.transform.position == mapper.exitPosition && !isLoading)
        {
            isLoading = true;
            LoadMap(mapper.nextMap);
        }
    }

    public void LoadMap(int map)
    {
        SceneInfo.currentMap = map;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
