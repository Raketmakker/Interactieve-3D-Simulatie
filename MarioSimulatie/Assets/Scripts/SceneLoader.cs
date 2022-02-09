using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string additiveScene = "Level2Addition";

    public void LoadSceneAdditive()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (this.additiveScene.Equals(SceneManager.GetSceneAt(i).name))
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
        SceneManager.LoadScene(this.additiveScene, LoadSceneMode.Additive);
    }
}
