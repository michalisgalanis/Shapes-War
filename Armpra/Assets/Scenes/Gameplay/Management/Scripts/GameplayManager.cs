using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject lostMenu;
    public GameObject wonMenu;
    public GameObject movementJoystick;
    public GameObject attackJoystick;

    private PlayerStats player;
    public int level;
    public float bestAttemptPercentage;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        SavingSystem.LoadData();
        pauseMenu.SetActive(false);
        lostMenu.SetActive(false);
        wonMenu.SetActive(false);
        gameUI.SetActive(true);
        //movementJoystick.SetActive(true);
        //attackJoystick.SetActive(true);
    }

    public void Pause(){
        Time.timeScale = 0;
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        //movementJoystick.SetActive(false);
        //attackJoystick.SetActive(false);
    }

    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        //movementJoystick.SetActive(true);
        //attackJoystick.SetActive(true);
    }

    public void Restart(){
        SceneManager.LoadScene("Gameplay");
    }

    public void Lose(){
        lostMenu.gameObject.SetActive(true);
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0;
        gameUI.SetActive(false);
        wonMenu.SetActive(true);
    }

    public void ProceedToNextLevel()
    {
        Time.timeScale = 1;
        player.RefillStats();
        gameUI.SetActive(true);
        wonMenu.SetActive(false);
    }

    public void ReturnHome(){
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
        EditorSceneManager.OpenScene("MainMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
    }
}
