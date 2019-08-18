using UnityEngine;

[System.Serializable]
public class Data {
    //General Info
    public int currentLevel;
    public float bestAttemptPercentage;
    //Player Data
    public int playerLevel;
    public float damageReduction;
    //public Weapon weapon;
    public float attackSpeed;
    public double XP;

    //Powerup Upgrades

    //Shield powerup
    public float maxShieldHealth;
    public float shieldDamage;
    //Speed powerup
    public float speedPowerupDuration;
    public float speedPowerUpMultiplier;

    public Data(PlayerStats playerStatsComponent, Shield shield, GameObject gameManager) {
        //General variables
        currentLevel = gameManager.GetComponent<LevelGeneration>().currentLevel;
        bestAttemptPercentage = gameManager.GetComponent<GameplayManager>().bestAttemptPercentage;
        //Player variables
        playerLevel = playerStatsComponent.playerLevel;
        damageReduction = playerStatsComponent.damageReduction;
        attackSpeed = playerStatsComponent.attackSpeed;
        XP = playerStatsComponent.XP;
        //Powerup variables

        //Shield powerup
        maxShieldHealth = shield.maxShieldHealth;
        shieldDamage = shield.shieldDamage;
    }
}
