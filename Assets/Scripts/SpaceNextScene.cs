using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceNextScene : MonoBehaviour
{
    public int sceneIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
