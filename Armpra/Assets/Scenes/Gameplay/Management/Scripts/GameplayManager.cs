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
    public GameObject storeMenu;
    public GameObject movementJoystick;
    public GameObject attackJoystick;
    private LevelGeneration lg;


    private GameObject gameManager;
    public DynamicBackground background;
    private PlayerStats playerStatsComponent;
    public Shield shield;
    public SpeedPowerUp speedPowerUp;
    public float bestAttemptPercentage;
    public Data loadedData;
    private GameObject shieldObject=null;
    public int currentLevel;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerStatsComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if (SavingSystem.LoadData() != null)
        {
            loadedData = SavingSystem.LoadData();
            //Load General Stats
            gameManager.GetComponent<LevelGeneration>().currentLevel = loadedData.currentLevel;
            currentLevel = loadedData.currentLevel;
            gameManager.GetComponent<GameplayManager>().bestAttemptPercentage = loadedData.bestAttemptPercentage;

            //Load Player Stats
            playerStatsComponent.playerLevel = loadedData.playerLevel;
            playerStatsComponent.damageReduction = loadedData.damageReduction;
            playerStatsComponent.attackSpeed = loadedData.attackSpeed;
            //Load Powerup Upgrades
            //Shield Powerup
            shield.maxShieldHealth = loadedData.maxShieldHealth;
            shield.shieldDamage = loadedData.shieldDamage;
            //Speed Powerup
            speedPowerUp.powerupDuration = loadedData.speedPowerupDuration;
            speedPowerUp.powerupMultiplier = loadedData.speedPowerUpMultiplier;
        }
        else //First time the game is launched/Save file is missing. All variables are set to default values.
        {
            gameManager.GetComponent<LevelGeneration>().currentLevel = 1;
            gameManager.GetComponent<GameplayManager>().bestAttemptPercentage = 1;
            //Player variables
            playerStatsComponent.playerLevel = 1;
            playerStatsComponent.XP = 0;

            //Powerup variables

            //Shield powerup
            shield.maxShieldHealth = 80;
            shield.shieldDamage = 0.5f;
            //Speed powerup
            speedPowerUp.powerupDuration = 7;
            speedPowerUp.powerupMultiplier = 1.5f;
        }

        gameManager.GetComponent<EnemySpawner>().BeginSpawning();

        pauseMenu.SetActive(false);
        lostMenu.SetActive(false);
        wonMenu.SetActive(false);
        gameUI.SetActive(true);
        storeMenu.SetActive(false);
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
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
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
        SavingSystem.SaveProgress(playerStatsComponent, shield, speedPowerUp, gameObject);
        lostMenu.SetActive(true);
        gameUI.SetActive(false);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
    }

    public void CompleteLevel()
    {
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject != null)
            Destroy(shieldObject);
        SavingSystem.SaveProgress(playerStatsComponent, shield, speedPowerUp, gameManager);
        Time.timeScale = 0;
        gameUI.SetActive(false);
        wonMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
    }

    public void ProceedToNextLevel()
    {
        Time.timeScale = 1;
        background.ChangeBlackgroundColor();
        playerStatsComponent.RefillStats();
        gameUI.SetActive(true);
        wonMenu.SetActive(false);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
    }

    public void VisitStore()
    {
        storeMenu.SetActive(true);
        gameUI.SetActive(false);
        wonMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void ReturnToCompleteLevel()
    {
        wonMenu.SetActive(true);
        storeMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void ReturnHome(){
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
        EditorSceneManager.OpenScene("MainMenu");
        SceneManager.UnloadSceneAsync("Gameplay");
    }
}
