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


    private GameObject gameManager;
    private PlayerStats playerStatsComponent;
    public Shield shield;
    public SpeedPowerUp speedPowerUp;
    public float bestAttemptPercentage;
    public Data loadedData;
    private GameObject shieldObject=null;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerStatsComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        loadedData = SavingSystem.LoadData();
        //Load General Stats
        gameManager.GetComponent<LevelGeneration>().currentLevel = loadedData.currentLevel;
        gameManager.GetComponent<GameplayManager>().bestAttemptPercentage = loadedData.bestAttemptPercentage;

        //Load Player Stats
        playerStatsComponent.playerLevel = loadedData.playerLevel;
        playerStatsComponent.damageReduction = loadedData.damageReduction;
        playerStatsComponent.attackSpeed = loadedData.attackSpeed;
        playerStatsComponent.rangedDamage = loadedData.rangedDamage;
        //Load Powerup Upgrades
        //Shield Powerup
        shield.maxShieldHealth = loadedData.maxShieldHealth;
        shield.shieldDamage = loadedData.shieldDamage;
        //Speed Powerup
        speedPowerUp.powerupDuration = loadedData.speedPowerupDuration;
        speedPowerUp.powerupMultiplier = loadedData.speedPowerUpMultiplier;

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
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject)
            Destroy(shieldObject);
        float maxEC = gameManager.GetComponent<EnemySpawner>().maxEnemyCount;
        float EC = gameManager.GetComponent<EnemySpawner>().enemyCounter;
        bestAttemptPercentage = ((maxEC - EC )/ maxEC)*100;
        bestAttemptPercentage = Mathf.Round(bestAttemptPercentage * 100f) / 100f;
        bestAttemptPercentage = Mathf.Max(bestAttemptPercentage, loadedData.bestAttemptPercentage);
        SavingSystem.SaveProgress(playerStatsComponent, shield, speedPowerUp, gameManager);
        lostMenu.gameObject.SetActive(true);
    }

    public void CompleteLevel()
    {
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject != null)
        {
            Debug.Log("THERE IS A SHIELD!");
            Destroy(shieldObject);
        }
        SavingSystem.SaveProgress(playerStatsComponent, shield, speedPowerUp, gameManager);
        Time.timeScale = 0;
        gameUI.SetActive(false);
        wonMenu.SetActive(true);
    }

    public void ProceedToNextLevel()
    {
        Time.timeScale = 1;
        playerStatsComponent.RefillStats();
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
