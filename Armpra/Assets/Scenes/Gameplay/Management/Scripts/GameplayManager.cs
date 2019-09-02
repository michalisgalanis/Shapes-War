using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayManager : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    private Constants.Gameplay.Manager.storeSource source;
    private Constants.Gameplay.Manager.gameState currentGameState;

    public void Awake() {
        rf = GetComponent<Referencer>();
        Application.targetFrameRate = 60;
    }

    private void Start() {
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
        rf.ps.EstimateStats();
        rf.ps.RefillStats();
        rf.pm.resetMovement();
        SavingSystem.getInstance().LoadAll();
        if ((RuntimeSpecs.mapLevel % 5 == 0 || RuntimeSpecs.mapLevel == 1) && Constants.Text.lastEnemyRemembered != (RuntimeSpecs.mapLevel / 5))
            NewEnemyFound();
        rf.es.BeginSpawning();
    }

    public void Pause() {
        currentGameState = Constants.Gameplay.Manager.gameState.PAUSE;
        manageMenus();
    }

    public void Resume() {
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
    }

    public void Restart() {
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
        SavingSystem.getInstance().Save(Constants.Data.dataTypes.PLAYER_MAP_DATA);
        SceneManager.LoadScene(Constants.Scenes.GAMEPLAY_SCENE_NAME);
    }

    public void Lose() {
        Time.timeScale = 0;
        source = Constants.Gameplay.Manager.storeSource.LOST_MENU;
        currentGameState = Constants.Gameplay.Manager.gameState.LOST;
        clearField();
        manageMenus();
        rf.pm.resetMovement();
        RuntimeSpecs.bap = Mathf.Max(RuntimeSpecs.ap, RuntimeSpecs.bap);
        SavingSystem.getInstance().Save(Constants.Data.dataTypes.PLAYER_MAP_DATA);
    }

    public void CompleteLevel() {
        source = Constants.Gameplay.Manager.storeSource.WIN_MENU;
        currentGameState = Constants.Gameplay.Manager.gameState.WIN;
        gameObject.GetComponent<AudioManager>().Play("WinningSound");
        rf.levelCompleteText.text = "Level " + RuntimeSpecs.mapLevel + " Complete!";
        rf.cs.FixedUpdate();
        RuntimeSpecs.mapLevel++;
        clearField();
        rf.lg.EstimateLevel();
        rf.ps.RefillStats();
        rf.pm.resetMovement();
        manageMenus();
        RuntimeSpecs.bap = 0f;
        SavingSystem.getInstance().Save(Constants.Data.dataTypes.PLAYER_MAP_DATA);
    }

    public void ProceedToNextLevel() {
        Time.timeScale = 1;
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
        rf.backgroundScript.ChangeBackgroundColor();
        if ((RuntimeSpecs.mapLevel % 5 == 0 || RuntimeSpecs.mapLevel == 1) && Constants.Text.lastEnemyRemembered != (RuntimeSpecs.mapLevel / 5))
            NewEnemyFound();
    }

    public void NewEnemyFound() {
        Time.timeScale = 0;
        currentGameState = Constants.Gameplay.Manager.gameState.NEW_ENEMY_FOUND;
        int index = (RuntimeSpecs.mapLevel / 5);
        Constants.Text.lastEnemyRemembered = index;
        rf.enemyDescriptorPanelUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =  "\"" + Constants.Text.enemyNames[index] + "\"";
        rf.enemyDescriptorPanelUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = Constants.Text.enemyDescriptions[index];
        manageMenus();
    }

    public void NewEnemyFoundContinue() {
        Time.timeScale = 1;
        currentGameState = Constants.Gameplay.Manager.gameState.PLAY;
        manageMenus();
    }

    public void VisitStore() {
        currentGameState = Constants.Gameplay.Manager.gameState.STORE;
        manageMenus();
        rf.ss.forceRefresh();
    }

    public void ReturnToCompleteLevel() {
        currentGameState = Constants.Gameplay.Manager.gameState.WIN;
        manageMenus();
    }

    public void ExitStore() {
        SavingSystem.getInstance().Save(Constants.Data.dataTypes.STORE_DATA);
        rf.ps.EstimateStats();
        if (source == Constants.Gameplay.Manager.storeSource.WIN_MENU)
            ReturnToCompleteLevel();
        else if (source == Constants.Gameplay.Manager.storeSource.LOST_MENU)
            ReturnToLostMenu();
    }

    public void ReturnToLostMenu() {
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
        Time.timeScale = 0;
        ShowSelectedLayers();
        switch (currentGameState) {
            case Constants.Gameplay.Manager.gameState.PLAY:
                Time.timeScale = 1;
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
                rf.enemyDescriptorPanelUI.SetActive(false);
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
                rf.enemyDescriptorPanelUI.SetActive(false);
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
                rf.enemyDescriptorPanelUI.SetActive(false);
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
                rf.enemyDescriptorPanelUI.SetActive(false);
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
                rf.enemyDescriptorPanelUI.SetActive(false);
                break;
            case Constants.Gameplay.Manager.gameState.NEW_ENEMY_FOUND:
                rf.scoreCoinsUI.SetActive(false);
                rf.pauseMenuUI.SetActive(false);
                rf.winMenuUI.SetActive(false);
                rf.lostMenuUI.SetActive(false);
                rf.storeMenuUI.SetActive(false);
                rf.movementJoystickUI.SetActive(false);
                rf.attackJoystickUI.SetActive(false);
                rf.debugPanelUI.SetActive(false);
                rf.hackPanelUI.SetActive(false);
                rf.ammoPanelUI.SetActive(false);
                rf.enemyDescriptorPanelUI.SetActive(true);
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
        RuntimeSpecs.enemiesKilled = 0;
        RuntimeSpecs.enemiesSpawned = 0;
    }

    public void hackPanelCompleteLevel() {
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag(Constants.Tags.ENEMY_TAG);
        foreach (GameObject activeEnemy in activeEnemies) {
            if (activeEnemy.activeInHierarchy && activeEnemy.GetComponent<Enemy>() != null) {
                activeEnemy.GetComponent<Enemy>().TakeDamage(activeEnemy.GetComponent<Enemy>().currentHealth);
            }
        }
        rf.es.spawningTime = false;
        RuntimeSpecs.enemiesKilled = RuntimeSpecs.maxEnemyCount;
    }

    public void hackPanelLose() {
        rf.ps.TakeDamage(RuntimeSpecs.currentPlayerHealth);
    }

    private void loadData() {
        SavingSystem.SetPath();
        if (SavingSystem.LoadData() != null && enableSavingSystem == true) {
            Data loadedData = SavingSystem.LoadData();
            loadedData.Load();
        }
    }
}
