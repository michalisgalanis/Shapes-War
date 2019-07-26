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
    public GameObject movementJoystick;
    public GameObject attackJoystick;
    public int level;
    public float bestAttemptPercentage=0; //Defines the best attempt to defeat current level in a 0-99% percentage.

    void Start(){
        SavingSystem.LoadData();
        pauseMenu.SetActive(false);
        lostMenu.SetActive(false);
        gameUI.SetActive(true);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
    }

    public void Pause(){
        Time.timeScale = 0;
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
    }

    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }

    public void Lose(){
        lostMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnHome(){
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
        EditorSceneManager.OpenScene("MainMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
    }
}
