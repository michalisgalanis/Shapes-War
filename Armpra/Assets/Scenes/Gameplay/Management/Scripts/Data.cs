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
    public float weaponDamage;
    public float attackSpeed;
    public double XP;

    //Powerup Upgrades

    //Shield powerup
    public float maxShieldHealth;
    public float shieldDamage;
    //Speed powerup
    public float speedPowerupDuration;
    public float speedPowerUpMultiplier;

    public Data(PlayerStats player, GameplayManager gmanager, Shield shield, SpeedPowerUp rushB)
    {
        //General variables
        currentLevel = gmanager.level;
        bestAttemptPercentage = gmanager.bestAttemptPercentage;
        //Player variables
        playerLevel = player.playerLevel;
        damageReduction = player.damageReduction;
        //weapon = player.weapon;
        weaponDamage = player.rangedDamage;
        attackSpeed = player.attackSpeed;
        XP = player.xP;

        //Powerup variables

        //Shield powerup
        maxShieldHealth = shield.maxShieldHealth;
        shieldDamage = shield.shieldDamage;
        //Speed powerup
        speedPowerupDuration = rushB.powerupDuration;
        speedPowerUpMultiplier = rushB.powerupDuration;


    }
}
