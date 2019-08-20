using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    enum storeSource { WIN_MENU, LOST_MENU }

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

    public bool enableSavingSystem;
    public float bestAttemptPercentage=0;
    public int currentLevel;

    //Needed References
    private GameObject gm;
    private PlayerStats ps;
    private PlayerExperience pe;
    private StoreSystem ss;


    public Shield shield;
    public DynamicBackground background;
    public Data loadedData;
    private GameObject shieldObject = null;

    private storeSource source;

    public void InstantiateReferences() {
        gm = GameObject.FindGameObjectWithTag("GameController");
        pe = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ss = gm.GetComponent<StoreSystem>();
    }

    private void Start() {
        Application.targetFrameRate = 60;
        InstantiateReferences();
        SavingSystem.SetPath();
        if (SavingSystem.LoadData() != null && enableSavingSystem) {
            loadedData = SavingSystem.LoadData();
            //Load General Stats
            gm.GetComponent<LevelGeneration>().currentLevel = loadedData.currentLevel;
            currentLevel = loadedData.currentLevel;
            gm.GetComponent<GameplayManager>().bestAttemptPercentage = loadedData.bestAttemptPercentage;

            //Load Player Stats
            pe.currentPlayerXP = loadedData.currentPlayerXP;
            pe.playerLevel = loadedData.playerLevel;
            ps.playerLevel = loadedData.playerLevel;
            gm.GetComponent<CoinSystem>().currentCoins = loadedData.currentCoins;

            //Load Store Upgrades
            ss.attackSpeedUpgradeCounter = loadedData.attackSpeedUpgradeCounter;
            ss.bulletEffectUpgradeCounter = loadedData.bulletEffectUpgradeCounter;
            ss.bulletSpeedUpgradeCounter = loadedData.bulletSpeedUpgradeCounter;
            ss.damageReductionUpgradeCounter = loadedData.damageReductionUpgradeCounter;
            ss.maxHealthUpgradeCounter = loadedData.maxHealthUpgradeCounter;
            ss.meleeDamageUpgradeCounter = loadedData.meleeDamageUpgradeCounter;
            ss.movementSpeedUpgradeCounter = loadedData.movementSpeedUpgradeCounter;
            ss.powerupDurationCounter = loadedData.powerupDurationCounter;
            ss.powerupEffectCounter = loadedData.powerupEffectCounter;
            ss.powerupSpawnFrequencyCounter = loadedData.powerupSpawnFrequencyCounter;
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
        ps.InstantiateReferences();
        ps.EstimateStats();
        ps.RefillStats();
        gm.GetComponent<EnemySpawner>().BeginSpawning();

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
        Time.timeScale = 1;
    }

    public void Lose() {
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject)
            Destroy(shieldObject);

        float maxEC = gm.GetComponent<EnemySpawner>().maxEnemyCount;
        float EC = gm.GetComponent<EnemySpawner>().enemyCounter;
        bestAttemptPercentage = ((maxEC - EC) / maxEC) * 100;
        Debug.Log(bestAttemptPercentage);
        bestAttemptPercentage = Mathf.Round(bestAttemptPercentage * 100f) / 100f;
        bestAttemptPercentage = Mathf.Max(bestAttemptPercentage, loadedData.bestAttemptPercentage);
        SavingSystem.SaveProgress(pe, shield, gameObject);
        lostMenu.SetActive(true);
        gameUI.SetActive(false);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
        source = storeSource.LOST_MENU;
    }
        
    public void CompleteLevel() {
        shieldObject = GameObject.FindGameObjectWithTag("Shield");
        if (shieldObject != null)
            Destroy(shieldObject);

        SavingSystem.SaveProgress(pe, shield, gm);
        Time.timeScale = 0;
        gameUI.SetActive(false);
        wonMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        movementJoystick.SetActive(false);
        attackJoystick.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
        source = storeSource.WIN_MENU;
    }

    public void ProceedToNextLevel(){
        bestAttemptPercentage = 0;
        Time.timeScale = 1;
        background.ChangeBlackgroundColor();
        ps.RefillStats();
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
        lostMenu.SetActive(false);
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

    public void ExitStore() {
        if (source == storeSource.WIN_MENU) ReturnToCompleteLevel();
        else if (source == storeSource.LOST_MENU) ReturnToLostMenu();
    }

    public void ReturnToLostMenu() {
        lostMenu.SetActive(true);
        storeMenu.SetActive(false);
        hackToolMenu.SetActive(false);
        debugPanel.SetActive(false);
        ammoPanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void ReturnHome() {
        Time.timeScale = 1;

        SceneManager.LoadScene("Main Menu");
        SceneManager.UnloadScene("Gameplay");
    }
}
