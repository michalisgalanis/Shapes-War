using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    private TextMeshProUGUI mapLevelText;
    private TextMeshProUGUI maxEnemyCountText;
    private TextMeshProUGUI playerLevelText;
    private TextMeshProUGUI playerXPText;
    private TextMeshProUGUI playerXPRemainingText;
    private TextMeshProUGUI meleeDamageText;
    private TextMeshProUGUI rangedDamageText;
    private TextMeshProUGUI attackSpeedText;
    private TextMeshProUGUI maxHealthText;
    private TextMeshProUGUI damageReductionText;
    private TextMeshProUGUI movementSpeedText;
    private TextMeshProUGUI currentHealthText;

    //Constants
    private const int DIGITS = 2;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        mapLevelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        maxEnemyCountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerLevelText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        playerXPText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        playerXPRemainingText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        meleeDamageText = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        rangedDamageText = transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        attackSpeedText = transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        maxHealthText = transform.GetChild(8).GetComponent<TextMeshProUGUI>();
        damageReductionText = transform.GetChild(9).GetComponent<TextMeshProUGUI>();
        movementSpeedText = transform.GetChild(10).GetComponent<TextMeshProUGUI>();
        currentHealthText = transform.GetChild(11).GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        mapLevelText.text = "Map Level: " + RuntimeSpecs.mapLevel;
        maxEnemyCountText.text = "Max # of En: " + RuntimeSpecs.maxEnemyCount;
        playerLevelText.text = "Player Level: " + RuntimeSpecs.playerLevel;
        playerXPText.text = "Player XP: " + Mathf.Round((float)RuntimeSpecs.currentPlayerXP * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        playerXPRemainingText.text = "Player XP Rem.: " + Mathf.Round((float)RuntimeSpecs.remainingXP * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        meleeDamageText.text = "Melee Damage: " + Mathf.Round(rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MELEE_DAMAGE) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        rangedDamageText.text = "Ranged Damage: " + Mathf.Round(rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.RANGED_DAMAGE) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        attackSpeedText.text = "Attack Speed: " + Mathf.Round(rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.ATTACK_SPEED) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        maxHealthText.text = "Max Health: " + Mathf.Round(rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        damageReductionText.text = "Dam. Red.: " + Mathf.Round(rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        movementSpeedText.text = "Mov. Speed: " + Mathf.Round(rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED) * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
        currentHealthText.text = "Curr. Health: " + Mathf.Round(RuntimeSpecs.currentPlayerHealth * Mathf.Pow(10, DIGITS)) / Mathf.Pow(10, DIGITS);
    }
}
