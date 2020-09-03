using UnityEngine;

public class ButtonManager : MonoBehaviour {
    //Referencer
    private static Referencer rf;

    public void Awake() {
        rf = GetComponent<Referencer>();
    }

    public void PlayPress() {
        rf.gm.Play();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void PausePress() {
        rf.gm.Pause();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void ResumePress() {
        rf.gm.Resume();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void RestartPress() {
        rf.gm.Restart();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void ProceedToNextLevelPress() {
        rf.gm.ProceedToNextLevel();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void StorePress() {
        rf.gm.VisitStore();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void StoreExitPress() {
        rf.gm.ExitStore();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void CheckUnlockedItems() {
        rf.gm.CheckUnlockedItems();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void ExitUnlockMenu() {
        rf.gm.ExitUnlockMenu();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void ReturnToMainMenuPress() {
        rf.gm.ReturnHome();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void HackLosePress() {
        rf.gm.hackPanelLose();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void HackWinPress() {
        rf.gm.hackPanelCompleteLevel();
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void DebugPanelPress() {
        rf.debugPanelUI.SetActive(!rf.debugPanelUI.activeSelf);
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void AddCoins(int coins) {
        CoinSystem.AddCoins(coins);
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void AddXp(int xp) {
        rf.pe.AddXP(xp);
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }

    public void SpawnPowerup(int index) {
        rf.pus.SpawnPowerup(index);
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
    }
}
