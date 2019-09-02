using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Variables
    [Header("Setup Variables")]
    public bool enableSavingSystem = false;

    //Runtime Variables
    private Constants.Gameplay.Manager.storeSource source;
    private Constants.Gameplay.Manager.gameState currentGameState;

    public void Awake() {
        rf = GetComponent<Referencer>();
        Application.targetFrameRate = 60;
    }

    private void Start() {
        Time.timeScale = 1;
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
        rf.ps.EstimateStats();
        rf.ps.RefillStats();
        rf.pm.resetMovement();
        loadData();
        rf.es.BeginSpawning();
    }

    public void Pause() {
        Time.timeScale = 0;
        currentGameState = Constants.Gameplay.Manager.gameState.PAUSE;
        manageMenus();
    }

    public void Resume() {
        Time.timeScale = 1;
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
    }

    public void Restart() {
        Time.timeScale = 1;
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
        if (enableSavingSystem)
            SavingSystem.SaveProgress();
        SceneManager.LoadScene(Constants.Scenes.GAMEPLAY_SCENE_NAME);
    }

    public void Lose() {
        Time.timeScale = 0;
        source = Constants.Gameplay.Manager.storeSource.LOST_MENU;
        currentGameState = Constants.Gameplay.Manager.gameState.LOST;
        clearField();
        manageMenus();
        rf.pm.resetMovement();
        if (RuntimeSpecs.ap > RuntimeSpecs.bap)
            RuntimeSpecs.bap = RuntimeSpecs.ap;
        if (enableSavingSystem)
            SavingSystem.SaveProgress();
    }

    public void CompleteLevel() {
        Time.timeScale = 0;
        rf.levelCompleteText.text = "Level " + RuntimeSpecs.mapLevel + " Complete!";
        RuntimeSpecs.mapLevel++;
        RuntimeSpecs.enemiesKilled = 0;
        RuntimeSpecs.enemiesSpawned = 0;
        clearField();
        rf.lg.EstimateLevel();
        rf.ps.RefillStats();
        rf.pm.resetMovement();
        source = Constants.Gameplay.Manager.storeSource.WIN_MENU;
        currentGameState = Constants.Gameplay.Manager.gameState.WIN;
        manageMenus();
        RuntimeSpecs.bap = 0f;
        if (enableSavingSystem)
            SavingSystem.SaveProgress();
    }

    public void ProceedToNextLevel() {
        Time.timeScale = 1;
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();

        rf.backgroundScript.ChangeBackgroundColor();
    }

    public void VisitStore() {
        Time.timeScale = 0;
        currentGameState = Constants.Gameplay.Manager.gameState.STORE;
        manageMenus();
        rf.ss.forceRefresh();
    }

    public void ReturnToCompleteLevel() {
        Time.timeScale = 0;
        currentGameState = Constants.Gameplay.Manager.gameState.WIN;
        manageMenus();
    }

    public void ExitStore() {
        if (source == Constants.Gameplay.Manager.storeSource.WIN_MENU) {
            ReturnToCompleteLevel();
        } else if (source == Constants.Gameplay.Manager.storeSource.LOST_MENU) {
            ReturnToLostMenu();
        }
        rf.ps.EstimateStats();
        if (enableSavingSystem)
            SavingSystem.SaveProgress();
    }

    public void ReturnToLostMenu() {
        Time.timeScale = 0;
        currentGameState = Constants.Gameplay.Manager.gameState.LOST;
        manageMenus();
    }

    public void ReturnHome() {
        Time.timeScale = 1;
        SceneManager.LoadScene(Constants.Scenes.MAIN_MENU_SCENE_NAME);
        SceneManager.UnloadSceneAsync(Constants.Scenes.GAMEPLAY_SCENE_NAME);
    }

    //Utility Methods
    private void manageMenus() {
        ShowSelectedLayers();
        switch (currentGameState) {
            case Constants.Gameplay.Manager.gameState.PLAY:
                rf.scoreCoinsUI.SetActive(true);
                rf.pauseMenuUI.SetActive(false);
                rf.winMenuUI.SetActive(false);
                rf.lostMenuUI.SetActive(false);
                rf.storeMenuUI.SetActive(false);
                rf.movementJoystickUI.SetActive(true);
                rf.attackJoystickUI.SetActive(true);

                rf.debugPanelUI.SetActive(false);
                rf.hackPanelUI.SetActive(true);
                rf.ammoPanelUI.SetActive(true);
                ShowSelectedLayers();
                break;
            case Constants.Gameplay.Manager.gameState.PAUSE:
                rf.scoreCoinsUI.SetActive(false);
                rf.pauseMenuUI.SetActive(true);
                rf.winMenuUI.SetActive(false);
                rf.lostMenuUI.SetActive(false);
                rf.storeMenuUI.SetActive(false);
                rf.movementJoystickUI.SetActive(false);
                rf.attackJoystickUI.SetActive(false);

                rf.debugPanelUI.SetActive(false);
                rf.hackPanelUI.SetActive(false);
                rf.ammoPanelUI.SetActive(false);
                break;
            case Constants.Gameplay.Manager.gameState.WIN:
                rf.scoreCoinsUI.SetActive(false);
                rf.pauseMenuUI.SetActive(false);
                rf.winMenuUI.SetActive(true);
                rf.lostMenuUI.SetActive(false);
                rf.storeMenuUI.SetActive(false);
                rf.movementJoystickUI.SetActive(false);
                rf.attackJoystickUI.SetActive(false);

                rf.debugPanelUI.SetActive(false);
                rf.hackPanelUI.SetActive(false);
                rf.ammoPanelUI.SetActive(false);
                break;
            case Constants.Gameplay.Manager.gameState.LOST:
                rf.scoreCoinsUI.SetActive(false);
                rf.pauseMenuUI.SetActive(false);
                rf.winMenuUI.SetActive(false);
                rf.lostMenuUI.SetActive(true);
                rf.storeMenuUI.SetActive(false);
                rf.movementJoystickUI.SetActive(false);
                rf.attackJoystickUI.SetActive(false);

                rf.debugPanelUI.SetActive(false);
                rf.hackPanelUI.SetActive(false);
                rf.ammoPanelUI.SetActive(false);
                break;
            case Constants.Gameplay.Manager.gameState.STORE:
                rf.scoreCoinsUI.SetActive(false);
                rf.pauseMenuUI.SetActive(false);
                rf.winMenuUI.SetActive(false);
                rf.lostMenuUI.SetActive(false);
                rf.storeMenuUI.SetActive(true);
                rf.movementJoystickUI.SetActive(false);
                rf.attackJoystickUI.SetActive(false);

                rf.debugPanelUI.SetActive(false);
                rf.hackPanelUI.SetActive(false);
                rf.ammoPanelUI.SetActive(false);
                break;
        }
    }
    private void ShowSelectedLayers() {
        GameObject camera = rf.cam;
        switch (currentGameState) {
            case Constants.Gameplay.Manager.gameState.PLAY:
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Constants.Layers.PLAYER_LAYER_NAME);
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Constants.Layers.ENEMY_LAYER_NAME);
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Constants.Layers.PROJECTILES_LAYER_NAME);
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Constants.Layers.POWERUPS_LAYER_NAME);

                break;
            default:
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Constants.Layers.PLAYER_LAYER_NAME));
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Constants.Layers.ENEMY_LAYER_NAME));
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Constants.Layers.PROJECTILES_LAYER_NAME));
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Constants.Layers.POWERUPS_LAYER_NAME));
                break;
        }
    }

    private void clearField() {
        foreach (GameObject tempPowerup in GameObject.FindGameObjectsWithTag(Constants.Tags.POWERUPS_TAG)) {
            if (tempPowerup.activeInHierarchy)
                Destroy(tempPowerup);
        }
    }

    private void loadData() {
        SavingSystem.SetPath();
        if (SavingSystem.LoadData() != null && enableSavingSystem == true) {
            Data loadedData = SavingSystem.LoadData();

            //Loading Data
            RuntimeSpecs.mapLevel = loadedData.mapLevel;
            RuntimeSpecs.ap = loadedData.bestAttemptPercentage;
            RuntimeSpecs.currentPlayerXP = loadedData.currentPlayerXP;
            RuntimeSpecs.playerLevel = loadedData.playerLevel;
            RuntimeSpecs.currentCoins = loadedData.currentCoins;

            //Load Store Upgrades
            for (int i = 0; i < rf.ss.upgrades.Length; i++) {
                rf.ss.upgrades[i].counter = loadedData.storeUpgradesCounters[i];
            }

            rf.ss.forceRefresh();
            rf.pg.Refresh();
            rf.ps.EstimateStats();
            rf.ps.RefillStats();
        }
    }
}
