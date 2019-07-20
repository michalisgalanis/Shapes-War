using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject lostMenu;

    void Start(){
        pauseMenu.gameObject.SetActive(false);
        lostMenu.gameObject.SetActive(false);
    }

    void Update(){
        
    }

    public void Lose(){
        lostMenu.gameObject.SetActive(true);
    }

    public void GoHome(){
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }
}
