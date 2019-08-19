using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject lostMenu;
    public GameObject wonMenu;
    public GameObject storeMenu;
    public GameObject hackToolMenu;
    public GameObject debugPanel;
    public GameObject ammoPanel;
    public GameObject movementJoystick;
    public GameObject attackJoystick;

    public bool iWantToLoadTheSavedData;
    public float bestAttemptPercentage=0;
    public int currentLevel;

    private GameObject gameManager;
    private PlayerStats playerStatsComponent;
    private PlayerExperience playerExperience;
    public Shield shield;
    public DynamicBackground background;
    public Data loadedData;
    private GameObject shieldObject = null;
    private StoreSystem ss;


    private void Start() {
        Application.targetFrameRate = 60;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerExperience = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerStatsComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ss = gameManager.GetComponent<StoreSystem>();

        if (SavingSystem.LoadData() != null && iWantToLoadTheSavedData) {
            StoreSystem storeSystem = gameManager.GetComponent<StoreSystem>();
            loadedData = SavingSystem.LoadData();
            //Load General Stats
            gameManager.GetComponent<LevelGeneration>().currentLevel = loadedData.currentLevel;
            currentLevel = loadedData.currentLevel;
            gameManager.GetComponent<GameplayManager>().bestAttemptPercentage = loadedData.bestAttemptPercentage;

            //Load Player Stats
            playerExperience.currentPlayerXP = loadedData.currentPlayerXP;
            playerExperience.playerLevel = loadedData.playerLevel;
            gameManager.GetComponent<CoinSystem>().currentCoins = loadedData.currentCoins;

            //Load Store Upgrades
            storeSystem.attackSpeedUpgradeCounter = loadedData.attackSpeedUpgradeCounter;
            storeSystem.bulletEffectUpgradeCounter = loadedData.bulletEffectUpgradeCounter;
            storeSystem.bulletSpeedUpgradeCounter = loadedData.bulletSpeedUpgradeCounter;
            storeSystem.damageReductionUpgradeCounter = loadedData.damageReductionUpgradeCounter;
            storeSystem.maxHealthUpgradeCounter = loadedData.maxHealthUpgradeCounter;
            storeSystem.meleeDamageUpgradeCounter = loadedData.meleeDamageUpgradeCounter;
            storeSystem.movementSpeedUpgradeCounter = loadedData.movementSpeedUpgradeCounter;
            storeSystem.powerupDurationCounter = loadedData.powerupDurationCounter;
            storeSystem.powerupEffectCounter = loadedData.powerupEffectCounter;
            storeSystem.powerupSpawnFrequencyCounter = loadedData.powerupSpawnFrequencyCounter;
        } /*else //First time the game is launched/Save file is missing. All variables are set to default values.
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
        }*/
        //Debug.Log(playerStatsComponent.maxHealth);
        playerStatsComponent.RefillStats();
        gameManager.GetComponent<EnemySpawner>().BeginSpawning();

        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        hackToolMenu.SetActive(true);
        wonMenu.SetActive(false);
        lostMenu.SetActive(false);
        storeMenu.SetActive(false);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(true);
    
}

    public void Pause() {
        Time.timeScale = 0;
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
    }

    public void Resume() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
        hackToolMenu.SetActive(true);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene("Gameplay");
    }

    public void Lose() {
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject)
            Destroy(shieldObject);

        float maxEC = gameManager.GetComponent<EnemySpawner>().maxEnemyCount;
        float EC = gameManager.GetComponent<EnemySpawner>().enemyCounter;
        bestAttemptPercentage = ((maxEC - EC) / maxEC) * 100;
        Debug.Log(bestAttemptPercentage);
        bestAttemptPercentage = Mathf.Round(bestAttemptPercentage * 100f) / 100f;
        bestAttemptPercentage = Mathf.Max(bestAttemptPercentage, loadedData.bestAttemptPercentage);
        SavingSystem.SaveProgress(playerExperience, shield, gameObject);
        lostMenu.SetActive(true);
        gameUI.SetActive(false);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
    }

    public void CompleteLevel() {
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject != null)
            Destroy(shieldObject);

        SavingSystem.SaveProgress(playerExperience, shield, gameManager);
        Time.timeScale = 0;
        gameUI.SetActive(false);
        wonMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
    }

    public void ProceedToNextLevel(){
        bestAttemptPercentage = 0;
        Time.timeScale = 1;
        background.ChangeBlackgroundColor();
        playerStatsComponent.RefillStats();
        gameUI.SetActive(true);
        wonMenu.SetActive(false);
        movementJoystick.SetActive(true);
        attackJoystick.SetActive(true);
        hackToolMenu.SetActive(true);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(true);
    }

    public void VisitStore() {
        ss.RefreshOnStoreEnter();
        storeMenu.SetActive(true);
        gameUI.SetActive(false);
        wonMenu.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void ReturnToCompleteLevel() {
        wonMenu.SetActive(true);
        storeMenu.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void ReturnHome() {
        Time.timeScale = 1;

        SceneManager.LoadScene("Main Menu");
        SceneManager.UnloadSceneAsync("Gameplay");
    }
}
