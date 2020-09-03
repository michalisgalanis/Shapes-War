using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    //References
    public MainMenuReferencer mmr;

    //Runtime Variables
    private Utility.Gameplay.Manager.mainMenuState currentMainMenuState;
    private Utility.Gameplay.Manager.optionsState currentOptionsState;
    private float hRand;

    private void Start() {
        mmr = GetComponent<MainMenuReferencer>();
        Application.targetFrameRate = GameSettings.APPLICATION_FRAMERATE_TARGET;
        GameSettings.platform = Application.platform;
        hRand = UnityEngine.Random.Range(0f, 1f);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.HOME;
        FirstRunUIRefresh();
        manageMenus();
    }

    private void FirstRunUIRefresh() {
        //Background
        mmr.background.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hRand, 1f, 0.1f);
        //Play Button
        mmr.mainMenu.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 0.5f);
        ParticleSystem.MainModule main = mmr.mainMenu.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
        main.startColor = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 1f, 0.5f);
        //Home Panel
        mmr.mainMenu.transform.GetChild(4).gameObject.SetActive(GameSettings.showMusicInfo); //Music Info Adjustments
        //Options Panel
        //Sound - Volume Sliders Colors
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 0.5f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 0.5f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 0.5f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(2).GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 0.5f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.vibrationsEnabled ? 0f : 0.3f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.vibrationsEnabled ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(6).GetComponent<Toggle>().onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(6).GetComponent<Toggle>().isOn = GameSettings.showMusicInfo;
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(6).GetComponent<Toggle>().onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(6).GetChild(1).GetChild(2).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 1f);
        //Display
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Toggle>().isOn = GameSettings.displayFPSCounter;
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(1).GetChild(2).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, 1f);

        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.animationSpeed == Utility.Gameplay.Animations.animationSpeed.OFF ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.animationSpeed == Utility.Gameplay.Animations.animationSpeed.FAST ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.animationSpeed == Utility.Gameplay.Animations.animationSpeed.RELAXED ? 0.3f : 0f);
        //Misc - Joystick Button Selected Colors
        mmr.optionsMenu.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, (GameSettings.currentControlType == Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK) ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, (GameSettings.currentControlType == Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK) ? 0.3f : 0f);

    }

    public void Play() {
        Time.timeScale = 1f;
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        SceneManager.LoadScene(Utility.Scenes.GAMEPLAY_SCENE_NAME);
        SceneManager.UnloadSceneAsync(Utility.Scenes.MAIN_MENU_SCENE_NAME);
    }

    public void VisitOptions() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.OPTIONS;
        currentOptionsState = Utility.Gameplay.Manager.optionsState.SOUND;
        manageMenus();
    }

    public void ChangeOptionsTab(string tab) {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        Enum.TryParse(tab, out Utility.Gameplay.Manager.optionsState optionsState);
        currentOptionsState = optionsState;
        manageOptionsSubMenus();
    }

    public void VisitStats() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.STATS;
        mmr.statsMenu.GetComponent<Stats>().RefreshStats();
        manageMenus();
    }

    public void ReturnToMainMenu() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.HOME;
        manageMenus();
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.SETTINGS_DATA);
    }

    public void ConfirmErase() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.CONFIRM_DELETE_PROGRESS;
        manageMenus();
    }

    public void DeleteProgress() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        SavingSystem.getInstance().DeleteProgress(Utility.Data.dataTypes.PLAYER_MAP_DATA);
        SavingSystem.getInstance().DeleteProgress(Utility.Data.dataTypes.STORE_DATA);
        SavingSystem.getInstance().DeleteProgress(Utility.Data.dataTypes.STATS_DATA);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.HOME;
        manageMenus();
    }

    public void ResetSettings() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        currentMainMenuState = Utility.Gameplay.Manager.mainMenuState.HOME;
        SavingSystem.getInstance().DeleteProgress(Utility.Data.dataTypes.SETTINGS_DATA);
        SceneManager.LoadScene(Utility.Scenes.MAIN_MENU_SCENE_NAME);
        manageMenus();
    }

    public void SelectControls(string joystickType) {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        Enum.TryParse(joystickType, out Utility.Gameplay.Controls.controlType controlType);
        GameSettings.currentControlType = controlType;
        mmr.optionsMenu.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.currentControlType == Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.currentControlType == Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK ? 0.3f : 0f);
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.SETTINGS_DATA);
    }

    public void ShowMusicInfoToggle() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        GameSettings.showMusicInfo = mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(6).GetComponent<Toggle>().isOn;
        mmr.mainMenu.transform.GetChild(4).gameObject.SetActive(GameSettings.showMusicInfo);
        mmr.mainMenu.transform.GetChild(5).gameObject.GetComponent<RectTransform>().position += Camera.main.ScreenToViewportPoint(new Vector3(0, GameSettings.showMusicInfo ? 500 : -500));
        mmr.mainMenu.transform.GetChild(6).gameObject.GetComponent<RectTransform>().position += Camera.main.ScreenToViewportPoint(new Vector3(0, GameSettings.showMusicInfo ? 500 : -500));
        mmr.mainMenu.transform.GetChild(7).gameObject.GetComponent<RectTransform>().position += Camera.main.ScreenToViewportPoint(new Vector3(0, GameSettings.showMusicInfo ? 500 : -500));
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.SETTINGS_DATA);
    }

    public void ShuffleSong() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        mmr.audioManagerComponent.PlayRandomMusic();
        mmr.mainMenu.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = mmr.audioManagerComponent.nextSong.name;
    }

    public void ChangeAnimationSpeed(int animationSpeedFactor) {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        GameSettings.animationsEnabled = animationSpeedFactor > 0;
        GameSettings.animationSpeed = GameSettings.animationsEnabled ? (animationSpeedFactor == 1 ? Utility.Gameplay.Animations.animationSpeed.RELAXED : Utility.Gameplay.Animations.animationSpeed.FAST) : Utility.Gameplay.Animations.animationSpeed.OFF;
        GameSettings.animationSpeedFactor = (animationSpeedFactor > 0) ? animationSpeedFactor : 1;
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.animationSpeed == Utility.Gameplay.Animations.animationSpeed.OFF ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.animationSpeed == Utility.Gameplay.Animations.animationSpeed.FAST ? 0.3f : 0f);
        mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, GameSettings.animationSpeed == Utility.Gameplay.Animations.animationSpeed.RELAXED ? 0.3f : 0f);
        AnimationManager.ResetChildAnimations(mmr.optionsMenu, false);
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.SETTINGS_DATA);
    }

    public void ToggleVibrations(bool enabled) {
        GameSettings.vibrationsEnabled = enabled;
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, enabled ? 0f : 0.3f);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).GetChild(4).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(hRand, 1f, 0.5f, enabled ? 0.3f : 0f);
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.SETTINGS_DATA);
    }

    public void FpsCounterToggle() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        GameSettings.displayFPSCounter = mmr.optionsMenu.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Toggle>().isOn;
        SavingSystem.getInstance().Save(Utility.Data.dataTypes.SETTINGS_DATA);
    }

    public void UnassignedButton() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void ExitApplication() {
        mmr.audioManagerComponent.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        Application.Quit();
    }
    //UTILITY METHODS

    private void manageMenus() {
        Time.timeScale = 0;
        mmr.mainMenu.SetActive(false);
        mmr.optionsMenu.SetActive(false);
        mmr.eraseConfPanel.SetActive(false);
        mmr.statsMenu.SetActive(false);
        switch (currentMainMenuState) {
            case Utility.Gameplay.Manager.mainMenuState.HOME:
                mmr.mainMenu.SetActive(true);
                mmr.mainMenu.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = mmr.audioManagerComponent.nextSong.name;
                AnimationManager.ResetChildAnimations(mmr.mainMenu, false);
                break;
            case Utility.Gameplay.Manager.mainMenuState.OPTIONS:
                manageOptionsSubMenus();
                AnimationManager.ResetChildAnimations(mmr.optionsMenu, false);
                break;
            case Utility.Gameplay.Manager.mainMenuState.CONFIRM_DELETE_PROGRESS:
                mmr.eraseConfPanel.SetActive(true);
                AnimationManager.ResetChildAnimations(mmr.eraseConfPanel, false);
                break;
            case Utility.Gameplay.Manager.mainMenuState.STATS:
                mmr.statsMenu.SetActive(true);
                AnimationManager.ResetChildAnimations(mmr.statsMenu, false);
                break;
        }
    }

    private void manageOptionsSubMenus() {
        Time.timeScale = 0;
        mmr.optionsMenu.SetActive(true);
        mmr.optionsMenu.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
        mmr.optionsMenu.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        mmr.optionsMenu.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        mmr.optionsMenu.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);

        mmr.optionsMenu.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
        mmr.optionsMenu.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        mmr.optionsMenu.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        mmr.optionsMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);

        mmr.optionsMenu.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
        mmr.optionsMenu.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0f);
        mmr.optionsMenu.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        mmr.optionsMenu.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
        switch (currentOptionsState) {
            case Utility.Gameplay.Manager.optionsState.SOUND:
                mmr.optionsMenu.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                mmr.optionsMenu.transform.GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.2f);
                mmr.optionsMenu.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                mmr.optionsMenu.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(mmr.optionsMenu.transform.GetChild(3).GetChild(0).gameObject, false);
                break;
            case Utility.Gameplay.Manager.optionsState.DISPLAY:
                mmr.optionsMenu.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                mmr.optionsMenu.transform.GetChild(2).GetChild(1).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.2f);
                mmr.optionsMenu.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                mmr.optionsMenu.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(mmr.optionsMenu.transform.GetChild(4).GetChild(0).gameObject, false);
                break;
            case Utility.Gameplay.Manager.optionsState.MISC:
                mmr.optionsMenu.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
                mmr.optionsMenu.transform.GetChild(2).GetChild(2).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.2f);
                mmr.optionsMenu.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                mmr.optionsMenu.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.9f);
                AnimationManager.ResetChildAnimations(mmr.optionsMenu.transform.GetChild(5).GetChild(0).gameObject, false);
                break;
        }
    }
}
