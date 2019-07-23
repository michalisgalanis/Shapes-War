﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject dimmer;
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject lostMenu;
    public GameObject movementJoystick;
    public GameObject attackJoystick;

    void Start(){
        dimmer.SetActive(false);
        pauseMenu.SetActive(false);
        lostMenu.SetActive(false);
        gameUI.SetActive(true);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
    }

    public void Pause(){
        Time.timeScale = 0;
        dimmer.SetActive(true);
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
    }

    public void Resume(){
        Time.timeScale = 1;
        dimmer.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
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
