using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Gameplay");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
