/*using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    *//*private string mapLevel;
    private string maxEnemyCount;
    private string playerLevel;
    private string playerXP;
    private string playerXPRemaining;
    private string meleeDamage;
    private string attackSpeed;
    private string maxHealth;
    private string damageReduction;
    private string movementSpeed;
    private string currentHealth;*//*

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

    private LevelGeneration lg;
    private EnemySpawner es;
    private PlayerStats ps;
    private PlayerExperience pe;


    public void Start()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameController");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        lg = gm.GetComponent<LevelGeneration>();
        es = gm.GetComponent<EnemySpawner>();
        ps = player.GetComponent<PlayerStats>();
        pe = player.GetComponent<PlayerExperience>();
        
    }

    public void Update()
    {
        mapLevelText.text = "Map Level: " + lg.currentLevel;
        maxEnemyCountText.text = "Max #Enemies: " + es.maxEnemyCount;
        playerLevelText.text = "Player Level: " + ps.playerLevel;
        playerXPText.text = "Player XP: " + Mathf.Round((float)ps.XP);
        playerXPRemainingText.text = "Player XP Rem.: " + Mathf.Round((float)pe.remainingXP);
        meleeDamageText.text = "Melee Damage: " + (ps.meleeDamage);
        attackSpeedText.text = "Attack Speed: " + (ps.attackSpeed);
        maxHealthText.text = "Max Health: " + Mathf.Round(ps.maxHealth);
        damageReductionText.text = "Dam. Red.: " + (ps.damageReduction);
        movementSpeedText.text = "Mov. Speed: " + (ps.movementSpeed);
        currentHealthText.text = "Curr. Health: " + Mathf.Round(ps.currentHealth);
    }
}*/
