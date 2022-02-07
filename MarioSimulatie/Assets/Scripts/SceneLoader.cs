using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string scenename;
    public string singleScene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadSceneAdditive();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(this.singleScene, LoadSceneMode.Single);
        }
    }

    public void LoadSceneAdditive()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (this.scenename.Equals(SceneManager.GetSceneAt(i).name))
            {
                return;
            }
        }
        SceneManager.LoadScene(this.scenename, LoadSceneMode.Additive);
    }
}
