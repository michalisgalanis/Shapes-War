using UnityEngine;

public class GameSettings {
    /////////////////////////////////////
    //Sounds Tab
    public static float masterVolume = 1f;
    public static float musicVolume = 0.5f;
    public static float sfxVolume = 0.5f;
    public static float uiVolume = 0.5f;
    public static bool showMusicInfo = true;
    public static bool vibrationsEnabled = true;

    //Display Tab
    public static bool displayFPSCounter = false;
    public static Utility.Gameplay.Animations.animationSpeed animationSpeed = Utility.Gameplay.Animations.animationSpeed.FAST;
    public static bool animationsEnabled = true;
    public static float animationSpeedFactor = 1f;

    //Misc Tab
    public static Utility.Gameplay.Controls.controlType currentControlType = Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK;
    
    /////////////////////////////////////
    //Other Settings
    public static RuntimePlatform platform = Application.platform;
    public const int APPLICATION_FRAMERATE_TARGET = 60;
}
