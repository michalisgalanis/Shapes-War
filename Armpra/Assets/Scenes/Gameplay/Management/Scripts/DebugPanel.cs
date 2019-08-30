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

    public void Awake() {
        rf = GetComponent<Referencer>();
    }

    public void Update() {
        mapLevelText.text = "Map Level: " + RuntimeSpecs.mapLevel;
        maxEnemyCountText.text = "Max #Enemies: " + RuntimeSpecs.maxEnemyCount;
        playerLevelText.text = "Player Level: " + RuntimeSpecs.playerLevel;
        playerXPText.text = "Player XP: " + Mathf.Round((float)RuntimeSpecs.currentPlayerXP);
        playerXPRemainingText.text = "Player XP Rem.: " + Mathf.Round((float)RuntimeSpecs.remainingXP);
        meleeDamageText.text = "Melee Damage: " + rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MELEE_DAMAGE);
        attackSpeedText.text = "Attack Speed: " + rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.ATTACK_SPEED);
        maxHealthText.text = "Max Health: " + Mathf.Round(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH));
        damageReductionText.text = "Dam. Red.: " + rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION); ;
        movementSpeedText.text = "Mov. Speed: " + rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED); ;
        currentHealthText.text = "Curr. Health: " + Mathf.Round(RuntimeSpecs.currentPlayerHealth);
    }
}
