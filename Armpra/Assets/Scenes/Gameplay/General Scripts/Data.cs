using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    //General Info
    public int level;
    //Player Data
    public int playerLevel;
    public float damageReduction;
    public Weapon weapon;
    public float weaponDamage;
    public float attackSpeed;
    public double XP;
    //Powerup Upgrades
    public float shieldArmor;
    public float powerupADuration;

    public Data(PlayerStats player, GameplayManager gmanager)
    {
        playerLevel = player.playerLevel;
        damageReduction = player.damageReduction;
        weapon = player.weapon;
        weaponDamage = player.weaponDamage;
        attackSpeed = player.attackSpeed;
        XP = player.XP;
        shieldArmor = gmanager.shieldArmor;
        shieldArmor = gmanager.powerupADuration;
    }
}
