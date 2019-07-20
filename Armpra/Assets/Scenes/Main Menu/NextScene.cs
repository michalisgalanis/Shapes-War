using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Gameplay");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
