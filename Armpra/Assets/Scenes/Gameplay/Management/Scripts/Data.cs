using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    //General Info
    public int currentLevel;
    public float bestAttemptPercentage;
    //Player Data
    public int playerLevel;
    public float damageReduction;
    //public Weapon weapon;
    public float rangedDamage;
    public float attackSpeed;
    public double XP;

    //Powerup Upgrades

    //Shield powerup
    public float maxShieldHealth;
    public float shieldDamage;
    //Speed powerup
    public float speedPowerupDuration;
    public float speedPowerUpMultiplier;

    public Data(PlayerStats playerStatsComponent, Shield shield, SpeedPowerUp speedPowerUp, GameObject gameManager)
    {

        //General variables
        level = gameManager.GetComponent<LevelGeneration>().currentLevel + 1;
        bestAttemptPercentage = gameManager.GetComponent<GameplayManager>().bestAttemptPercentage;
        //Player variables
        playerLevel = playerStatsComponent.playerLevel;
        damageReduction = playerStatsComponent.damageReduction;
        //weapon = player.weapon;
        rangedDamage = playerStatsComponent.rangedDamage;
        attackSpeed = playerStatsComponent.attackSpeed;
        XP = playerStatsComponent.XP;

        //Powerup variables

        //Shield powerup
        maxShieldHealth = shield.maxShieldHealth;
        shieldDamage = shield.shieldDamage;
        //Speed powerup
        speedPowerupDuration = speedPowerUp.powerupDuration;
        speedPowerUpMultiplier = speedPowerUp.powerupMultiplier;


    }
}
