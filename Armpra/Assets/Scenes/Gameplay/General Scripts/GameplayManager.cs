using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject lostMenu;

    void Start(){
        pauseMenu.gameObject.SetActive(false);
        lostMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    public void Pause(){
        Time.timeScale = 0;
        gameUI.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }

    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
    }

    public void Lose(){
        lostMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnHome(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }
}
