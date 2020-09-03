 using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    [HideInInspector] public Utility.Gameplay.Manager.adSource adSource;
    [HideInInspector] public Utility.Gameplay.Manager.adSource rewardedAdSource;

    private Utility.Gameplay.Manager.storeSource tempSource;
    private Utility.Gameplay.Manager.storeState storeState;
    private Utility.Gameplay.Manager.gameState currentGameState;

    public void Awake() {
        rf = GetComponent<Referencer>();
        Application.targetFrameRate = GameSettings.APPLICATION_FRAMERATE_TARGET;
        GameSettings.platform = Application.platform;
    }

    private void Start() {
        currentGameState = Utility.Gameplay.Manager.gameState.PLAY;
        SavingSystem.getInstance().LoadAll();
        rf.pm.SetupControls(GameSettings.currentControlType);
        UnlockSystem.CheckUnlockProgress();
        RuntimeSpecs.ResetTempStats();
        if (RuntimeSpecs.lastEnemyRemembered < RuntimeSpecs.newestEnemy) {
            rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.SFX, 2);
            currentGameState = Utility.Gameplay.Manager.gameState.NEW_ENEMY_FOUND;
        }
        manageMenus();
        rf.pm.resetMovement();
        rf.ps.RefillStats();
        StartCoroutine(rf.es.SpawnEnemies());
        rf.pus.ReEvaluatePowerups();
    }

    public void Play() {
        currentGameState = Utility.Gameplay.Manager.gameState.PLAY;
        manageMenus();
    }

    public void Pause() {
        currentGameState = Utility.Gameplay.Manager.gameState.PAUSE;
        manageMenus();
    }

    public void Resume() {
        currentGameState = Utility.Gameplay.Manager.gameState.PLAY;
        manageMenus();
    }

    public void Restart() {
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.PLAYER_MAP_DATA);
        SceneManager.LoadScene(Utility.Scenes.GAMEPLAY_SCENE_NAME);
    }

    public void Lose() {
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_LOST);
        UnlockSystem.CheckUnlockProgress();
        tempSource = Utility.Gameplay.Manager.storeSource.LOST_MENU;
        currentGameState = Utility.Gameplay.Manager.gameState.LOST;
        clearField();
#if UNITY_ADS
        if (AdManager.getInstance().CheckAdPropability()) {
            adSource = Utility.Gameplay.Manager.adSource.LOST_MENU;
            currentGameState = Utility.Gameplay.Manager.gameState.AD_PLAYING;
            AdManager.getInstance().PlayAd();
        }
        if (AdManager.getInstance().CheckRewardedAdPropability()) {
            rf.lostMenuUI.transform.GetChild(7).gameObject.SetActive(true);
            rf.lostMenuUI.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text = AdManager.getInstance().rewardedAdTypeString;
        }
#endif
        manageMenus();
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.PLAYER_MAP_DATA);
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.STATS_DATA);
    }

    public void CompleteLevel() {
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_COMPLETE_LEVEL);
        if (LevelGeneration.bossLevel) {
            LevelGeneration.bossLevel = false;
            rf.audioManagerComp.PlayRandomMusic();
        } else
            rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.SFX, 1);
        rf.levelCompleteText.text = "Level " + RuntimeSpecs.mapLevel + " Complete!";
        CoinSystem.AddCoins(RuntimeSpecs.mapLevel * 2);
        rf.pe.AddXP(RuntimeSpecs.mapLevel * 10);
        RuntimeSpecs.mapLevel++;
        UnlockSystem.CheckUnlockProgress();
        tempSource = Utility.Gameplay.Manager.storeSource.WIN_MENU;
        currentGameState = Utility.Gameplay.Manager.gameState.WIN;
#if UNITY_ADS
        if (AdManager.getInstance().CheckAdPropability()) {
            adSource = Utility.Gameplay.Manager.adSource.WIN_MENU;
            currentGameState = Utility.Gameplay.Manager.gameState.AD_PLAYING;
            AdManager.getInstance().PlayAd();
        }
        if (AdManager.getInstance().CheckRewardedAdPropability()) {
            rf.winMenuUI.transform.GetChild(6).gameObject.SetActive(true);
            rf.winMenuUI.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text = AdManager.getInstance().rewardedAdTypeString;
        }
#endif
        manageMenus();
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.PLAYER_MAP_DATA);
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.STATS_DATA);
    }

    public void ProceedToNextLevel() {
        if (rf == null) rf = GetComponent<Referencer>();
        currentGameState = Utility.Gameplay.Manager.gameState.PLAY;
        if (RuntimeSpecs.lastEnemyRemembered < RuntimeSpecs.newestEnemy) {
            rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.SFX, 2);
            currentGameState = Utility.Gameplay.Manager.gameState.NEW_ENEMY_FOUND;
        }
        manageMenus();
        rf.pm.resetMovement();
        rf.ps.RefillStats();
        clearField();
        StartCoroutine(rf.es.SpawnEnemies());
        rf.pus.ReEvaluatePowerups();
    }

    public void CheckUnlockedItems() {
        currentGameState = Utility.Gameplay.Manager.gameState.UNLOCKED_ITEMS;
        manageMenus();
    }

    public void NewEnemyFoundContinue() {
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        currentGameState = Utility.Gameplay.Manager.gameState.PLAY;
        manageMenus();
    }

    public void VisitStore() {
        currentGameState = Utility.Gameplay.Manager.gameState.STORE;
        storeState = Utility.Gameplay.Manager.storeState.PLAYER;
        manageMenus();
        rf.ss.forceRefresh();
    }

    public void ChangeStoreTab(string tab) {
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        Enum.TryParse(tab, out Utility.Gameplay.Manager.storeState storeState);
        this.storeState = storeState;
        manageStoreSubMenus();
    }

    public void ReturnToTempSource() {
        if (tempSource == Utility.Gameplay.Manager.storeSource.WIN_MENU)
            currentGameState = Utility.Gameplay.Manager.gameState.WIN;
        else if (tempSource == Utility.Gameplay.Manager.storeSource.LOST_MENU)
            currentGameState = Utility.Gameplay.Manager.gameState.LOST;
        manageMenus();
    }

    public void ExitUnlockMenu() {
        for (int i = 0; i < rf.unlockMenuUI.transform.GetChild(2).childCount; i++) {
            Destroy(rf.unlockMenuUI.transform.GetChild(2).GetChild(i).gameObject);
        }
        ReturnToTempSource();
    }

    public void ExitStore() {
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.STORE_DATA);
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.STATS_DATA);
        rf.ps.EstimateStats();
        ReturnToTempSource();
    }

    public void ReturnHome() {
        Time.timeScale = 1;
        if (LevelGeneration.bossLevel)
            rf.audioManagerComp.PlayRandomMusic();
        SceneManager.LoadScene(Utility.Scenes.MAIN_MENU_SCENE_NAME);
    }

    public void PlayRewardedAd(string sourceString) {
        Enum.TryParse(sourceString, out Utility.Gameplay.Manager.adSource source);
        rewardedAdSource = source;
        currentGameState = Utility.Gameplay.Manager.gameState.AD_PLAYING;
        AdManager.getInstance().PlayRewardedAd();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        manageMenus();
    }

    public void ExitAd(Utility.Gameplay.Manager.adSource source) {
        switch (source) {
            case Utility.Gameplay.Manager.adSource.WIN_MENU:
                currentGameState = Utility.Gameplay.Manager.gameState.WIN;
                break;
            case Utility.Gameplay.Manager.adSource.LOST_MENU:
                currentGameState = Utility.Gameplay.Manager.gameState.LOST;
                break;
        }
        manageMenus();
    }

    //Utility Methods
    private void manageMenus() {
        Time.timeScale = 0;
        if (rf == null) rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        rf.performancePanelUI.GetComponent<PerformancePanel>().ClearAvgFps();
        ShowSelectedLayers();
        rf.gameplayHUD.SetActive(false);
        rf.statsPanelUI.SetActive(false);
        rf.pauseMenuUI.SetActive(false);
        rf.winMenuUI.SetActive(false);
        rf.lostMenuUI.SetActive(false);
        rf.storeMenuUI.SetActive(false);
        rf.normalJoystick.SetActive(false);
        rf.dualZoneJoystick.SetActive(false);
        rf.debugPanelUI.SetActive(false);
        rf.performancePanelUI.SetActive(false);
        rf.hackPanelUI.SetActive(false);
        rf.ammoPanelUI.SetActive(false);
        rf.bossProgressBar.SetActive(false);
        rf.enemyDescriptorPanelUI.SetActive(false);
        rf.unlockMenuUI.SetActive(false);
        CoinSystem.RefreshCoins();
        rf.pe.RefreshXP();
        switch (currentGameState) {
            case Utility.Gameplay.Manager.gameState.PLAY:
                Time.timeScale = 1;
                rf.gameplayHUD.SetActive(true);
                rf.statsPanelUI.SetActive(true);
                rf.hackPanelUI.SetActive(true);
                rf.normalJoystick.SetActive(rf.pm.controlType == Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK && (GameSettings.platform == RuntimePlatform.Android || GameSettings.platform == RuntimePlatform.IPhonePlayer));
                rf.dualZoneJoystick.SetActive(rf.pm.controlType == Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK && (GameSettings.platform == RuntimePlatform.Android || GameSettings.platform == RuntimePlatform.IPhonePlayer));
                rf.ammoPanelUI.SetActive(true);
                rf.performancePanelUI.SetActive(GameSettings.displayFPSCounter);
                rf.ammoPanelUI.GetComponent<RectTransform>().position += Camera.main.ScreenToViewportPoint(new Vector3(rf.pm.controlType == Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK ? 2000 : 0, 0));
                rf.winMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
                rf.winMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
                rf.lostMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
                rf.lostMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
                rf.winMenuUI.transform.GetChild(6).gameObject.SetActive(false);
                rf.lostMenuUI.transform.GetChild(7).gameObject.SetActive(false);
                AnimationManager.ResetChildAnimations(rf.gameplayHUD, false);
                break;
            case Utility.Gameplay.Manager.gameState.PAUSE:
                rf.pauseMenuUI.SetActive(true);
                rf.pauseMenuUI.transform.GetChild(9).gameObject.SetActive(GameSettings.showMusicInfo);
                rf.pauseMenuUI.transform.GetChild(9).GetChild(2).GetComponent<TextMeshProUGUI>().text = rf.audioManagerComp.nextSong.name;
                AnimationManager.ResetChildAnimations(rf.pauseMenuUI, false);
                break;
            case Utility.Gameplay.Manager.gameState.WIN:
                rf.winMenuUI.SetActive(true);
                rf.winMenuUI.transform.GetChild(10).gameObject.SetActive(GameSettings.showMusicInfo);
                rf.winMenuUI.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text = rf.audioManagerComp.nextSong.name;
                rf.winMenuUI.transform.GetChild(11).gameObject.SetActive(RuntimeSpecs.unlockedItems.Count > 0);
                AnimationManager.ResetChildAnimations(rf.winMenuUI, false);
                break;
            case Utility.Gameplay.Manager.gameState.LOST:
                rf.lostMenuUI.SetActive(true);
                RuntimeSpecs.ap = Mathf.Round((RuntimeSpecs.enemiesKilled / (float)RuntimeSpecs.maxEnemyCount) * 100f);
                rf.bapText.text = RuntimeSpecs.ap.ToString();
                rf.lostMenuUI.transform.GetChild(10).gameObject.SetActive(GameSettings.showMusicInfo);
                rf.lostMenuUI.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text = rf.audioManagerComp.nextSong.name;
                rf.lostMenuUI.transform.GetChild(11).gameObject.SetActive(RuntimeSpecs.unlockedItems.Count > 0);
                AnimationManager.ResetChildAnimations(rf.lostMenuUI, false);
                break;
            case Utility.Gameplay.Manager.gameState.STORE:
                manageStoreSubMenus();
                AnimationManager.ResetChildAnimations(rf.storeMenuUI, false);
                break;
            case Utility.Gameplay.Manager.gameState.NEW_ENEMY_FOUND:
                rf.enemyDescriptorPanelUI.SetActive(true);
                rf.enemyDescriptorPanelUI.transform.GetChild(5).gameObject.SetActive(GameSettings.showMusicInfo);
                rf.enemyDescriptorPanelUI.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text = rf.audioManagerComp.nextSong.name;
                RuntimeSpecs.lastEnemyRemembered = RuntimeSpecs.newestEnemy;
                rf.enemyDescriptorPanelUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "\"" + Utility.Gameplay.Enemy.Text.enemyNames[RuntimeSpecs.lastEnemyRemembered] + "\"";
                rf.enemyDescriptorPanelUI.transform.GetChild(2).GetComponent<Image>().sprite = rf.enemySprites[RuntimeSpecs.lastEnemyRemembered];
                rf.enemyDescriptorPanelUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = Utility.Gameplay.Enemy.Text.enemyDescriptions[RuntimeSpecs.lastEnemyRemembered];
                AnimationManager.ResetChildAnimations(rf.enemyDescriptorPanelUI, false);
                break;
            case Utility.Gameplay.Manager.gameState.AD_PLAYING:
                rf.winMenuUI.transform.GetChild(6).gameObject.SetActive(false);
                rf.lostMenuUI.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case Utility.Gameplay.Manager.gameState.UNLOCKED_ITEMS:
                rf.unlockMenuUI.SetActive(true);
                rf.unlockMenuUI.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = rf.audioManagerComp.nextSong.name;
                for (int i = 0; i < RuntimeSpecs.unlockedItems.Count; i++) {
                    GameObject unlockedItem = Instantiate(rf.unlockItemPrefab, rf.unlockMenuUI.transform.GetChild(2));
                    unlockedItem.GetComponent<EnlargeAnimation>().delay = i * 0.1f + 0.5f;
                    unlockedItem.transform.GetChild(2).GetComponent<Image>().color = RuntimeSpecs.unlockedItems[i].storeUpgradeUI.transform.GetChild(0).GetChild(3).GetComponent<Image>().color;
                    unlockedItem.transform.GetChild(1).GetComponent<RectTransform>().offsetMax = new Vector2(- rf.unlockMenuUI.transform.GetChild(2).GetChild(i).GetChild(2).GetComponent<RectTransform>().rect.height / 2f, 0f);
                    unlockedItem.transform.GetChild(1).GetComponent<RectTransform>().offsetMin = new Vector2(1.25f * unlockedItem.transform.GetChild(2).GetComponent<RectTransform>().rect.width, 0f);
                    unlockedItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (RuntimeSpecs.unlockedItems[i].isAmmo ? "Ammo: " : (RuntimeSpecs.unlockedItems[i].isSkin ? "Skin: " : "Upgrade: ")) + RuntimeSpecs.unlockedItems[i].storeUpgradeUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;
                    for (int j = 1; j < RuntimeSpecs.unlockedItems[i].storeUpgradeUI.transform.GetChild(0).GetChild(3).childCount; j++) {
                        GameObject go = Instantiate(RuntimeSpecs.unlockedItems[i].storeUpgradeUI.transform.GetChild(0).GetChild(3).GetChild(j).gameObject, rf.unlockMenuUI.transform.GetChild(2).GetChild(i).GetChild(2));
                        float ratio = (unlockedItem.transform.GetChild(2).GetComponent<RectTransform>().rect.width / RuntimeSpecs.unlockedItems[i].storeUpgradeUI.transform.GetChild(0).GetChild(3).GetComponent<RectTransform>().rect.width);
                        if (go.GetComponent<RectTransform>() != null) go.GetComponent<RectTransform>().sizeDelta = go.GetComponent<RectTransform>().rect.size * ratio;
                        else go.transform.localScale = go.transform.localScale * ratio;
                    }
                }
                AnimationManager.ResetChildAnimations(rf.unlockMenuUI, false);
                break;
        }
    }

    private void manageStoreSubMenus() {
        Time.timeScale = 0;
        if (rf == null) rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        rf.storeMenuUI.SetActive(true);
        rf.storeMenuUI.transform.GetChild(5).gameObject.SetActive(false);
        rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);

        rf.storeMenuUI.transform.GetChild(6).gameObject.SetActive(false);
        rf.storeMenuUI.transform.GetChild(4).GetChild(1).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);

        rf.storeMenuUI.transform.GetChild(7).gameObject.SetActive(false);
        rf.storeMenuUI.transform.GetChild(4).GetChild(2).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);

        rf.storeMenuUI.transform.GetChild(8).gameObject.SetActive(false);
        rf.storeMenuUI.transform.GetChild(4).GetChild(3).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);

        rf.storeMenuUI.transform.GetChild(9).gameObject.SetActive(false);
        rf.storeMenuUI.transform.GetChild(4).GetChild(4).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        rf.storeMenuUI.transform.GetChild(4).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        switch (storeState) {
            case Utility.Gameplay.Manager.storeState.PLAYER:
                rf.storeMenuUI.transform.GetChild(5).gameObject.SetActive(true);
                rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.4f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(rf.storeMenuUI.transform.GetChild(5).GetChild(0).gameObject, false);
                break;
            case Utility.Gameplay.Manager.storeState.POWERUPS:
                rf.storeMenuUI.transform.GetChild(6).gameObject.SetActive(true);
                rf.storeMenuUI.transform.GetChild(4).GetChild(1).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.4f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(rf.storeMenuUI.transform.GetChild(6).GetChild(0).gameObject, false);
                break;
            case Utility.Gameplay.Manager.storeState.AMMO:
                rf.storeMenuUI.transform.GetChild(7).gameObject.SetActive(true);
                rf.storeMenuUI.transform.GetChild(4).GetChild(2).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.4f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(rf.storeMenuUI.transform.GetChild(7).GetChild(0).gameObject, false);
                break;
            case Utility.Gameplay.Manager.storeState.SKINS:
                rf.storeMenuUI.transform.GetChild(8).gameObject.SetActive(true);
                rf.storeMenuUI.transform.GetChild(4).GetChild(3).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.4f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(rf.storeMenuUI.transform.GetChild(8).GetChild(0).gameObject, false);
                break;
            case Utility.Gameplay.Manager.storeState.EXTRAS:
                rf.storeMenuUI.transform.GetChild(9).gameObject.SetActive(true);
                rf.storeMenuUI.transform.GetChild(4).GetChild(4).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.4f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
                rf.storeMenuUI.transform.GetChild(4).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(rf.storeMenuUI.transform.GetChild(9).GetChild(0).gameObject, false);
                break;
        }
    }

    private void ShowSelectedLayers() {
        GameObject camera = rf.cam.gameObject;
        switch (currentGameState) {
            case Utility.Gameplay.Manager.gameState.PLAY:
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Utility.Layers.PLAYER_LAYER_NAME);
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Utility.Layers.ENEMY_LAYER_NAME);
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Utility.Layers.PROJECTILES_LAYER_NAME);
                camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer(Utility.Layers.POWERUPS_LAYER_NAME);
                break;
            default:
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.PLAYER_LAYER_NAME));
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.ENEMY_LAYER_NAME));
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.PROJECTILES_LAYER_NAME));
                camera.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.POWERUPS_LAYER_NAME));
                break;
        }
    }

    private void clearField() {
        //Clearing Powerups
        foreach (GameObject tempPowerup in GameObject.FindGameObjectsWithTag(Utility.Tags.POWERUPS_TAG)) {
            if (tempPowerup.activeInHierarchy)
                Destroy(tempPowerup);
        }
        //Clearing Projectiles
        foreach (GameObject tempProjectile in GameObject.FindGameObjectsWithTag(Utility.Tags.PROJECTILES_TAG)) {
            if (tempProjectile.activeInHierarchy)
                Destroy(tempProjectile);
        }

        RuntimeSpecs.enemiesKilled = 0;
        RuntimeSpecs.enemiesSpawned = 0;
    }

    public void hackPanelCompleteLevel() {
        StopCoroutine(rf.es.SpawnEnemies());
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag(Utility.Tags.ENEMY_TAG);
        foreach (GameObject activeEnemy in activeEnemies) {
            if (activeEnemy.activeInHierarchy && activeEnemy.GetComponent<Enemy>() != null) {
                activeEnemy.GetComponent<Enemy>().TakeDamage(activeEnemy.GetComponent<Enemy>().currentHealth, false);
            }
        }
    }

    public void hackPanelLose() {
        rf.ps.TakeDamage(RuntimeSpecs.currentPlayerHealth, true, true);
    }

    public void hackPanelGodMode() {
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        rf.ps.godMode = !rf.ps.godMode;
        rf.ps.RefillStats();
    }

    public void hackPanelMultipleLevel(int levelsToSkip) {
        StopCoroutine(rf.es.SpawnEnemies());
        RuntimeSpecs.mapLevel += levelsToSkip - 1;
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag(Utility.Tags.ENEMY_TAG);
        foreach (GameObject activeEnemy in activeEnemies) {
            if (activeEnemy.activeInHierarchy && activeEnemy.GetComponent<Enemy>() != null) {
                activeEnemy.GetComponent<Enemy>().TakeDamage(activeEnemy.GetComponent<Enemy>().currentHealth, false);
            }
        }
        RuntimeSpecs.enemiesKilled = RuntimeSpecs.maxEnemyCount;
        StartCoroutine(rf.es.SpawnEnemies());
    }

    public void hackPanelGetAmmo(int amount) {
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.HV).counter = amount;
        rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.EXPLOSIVE).counter = amount;
        rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POISONOUS).counter = amount;
        rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.SNOW).counter = amount;
        rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.PENETRATION).counter = amount;
        rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.ELECTRICITY).counter = amount;
    }
}
