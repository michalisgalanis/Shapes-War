using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    public TextMeshProUGUI mapLevelText;
    public TextMeshProUGUI maxEnemyCountText;
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerXPText;
    public TextMeshProUGUI playerXPRemainingText;
    public TextMeshProUGUI meleeDamageText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI damageReductionText;
    public TextMeshProUGUI movementSpeedText;
    public TextMeshProUGUI currentHealthText;

    //Constants
    private const int DIGITS = 2;

    public void Awake() {
        rf = GetComponent<Referencer>();
    }

    public void Update() {
        mapLevelText.text = "Map Level: " + RuntimeSpecs.mapLevel;
        maxEnemyCountText.text = "Max # of En: " + RuntimeSpecs.maxEnemyCount;
        playerLevelText.text = "Player Level: " + RuntimeSpecs.playerLevel;
        playerXPText.text = "Player XP: " + Mathf.Round((float)RuntimeSpecs.currentPlayerXP * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        playerXPRemainingText.text = "Player XP Rem.: " + Mathf.Round((float)RuntimeSpecs.remainingXP * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        meleeDamageText.text = "Melee Damage: " + Mathf.Round(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MELEE_DAMAGE) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        attackSpeedText.text = "Attack Speed: " + Mathf.Round(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.ATTACK_SPEED) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        maxHealthText.text = "Max Health: " + Mathf.Round(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        damageReductionText.text = "Dam. Red.: " + Mathf.Round(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        movementSpeedText.text = "Mov. Speed: " + Mathf.Round(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        currentHealthText.text = "Curr. Health: " + Mathf.Round(RuntimeSpecs.currentPlayerHealth * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
    }
}
