using UnityEngine;

public class StatItem {
    //References
    private readonly Referencer rf;

    //Runtime Variables
    public Utility.Gameplay.Player.playerStatTypes statType;
    public int upgradeCounter;
    public float powerupFactor;
    public float enemyPenaltyFactor;
    public float statValue;

    public StatItem(Utility.Gameplay.Player.playerStatTypes statTypes) {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        statType = statTypes;
        EstimateStat();
    }

    public void EstimateStat() {
        switch (statType) {
            case Utility.Gameplay.Player.playerStatTypes.ATTACK_SPEED:
                //powerupFactor = rf.ps.GetPowerupMultiplier(Utility.Gameplay.Powerups.overTimePowerupTypes.ATTACK_SPEED_POWERUP);
                upgradeCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.ATTACK_SPEED).counter;
                enemyPenaltyFactor = 0f;
                statValue = Utility.Functions.PlayerStats.getPlayerAttackSpeed(RuntimeSpecs.playerLevel, upgradeCounter, powerupFactor);
                rf.wp.shootingTime = statValue;
                break;
            case Utility.Gameplay.Player.playerStatTypes.RANGED_DAMAGE:
                //powerupFactor = rf.ps.GetPowerupMultiplier(Utility.Gameplay.Powerups.overTimePowerupTypes.RANGED_DAMAGE_POWERUP);
                upgradeCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.RANGED_DAMAGE).counter;
                enemyPenaltyFactor = 0f;
                statValue = Utility.Functions.PlayerStats.getPlayerRangedDamageExternalFactor(RuntimeSpecs.playerLevel, upgradeCounter, powerupFactor, enemyPenaltyFactor);
                rf.wp.damageFactor = statValue;
                break;
            case Utility.Gameplay.Player.playerStatTypes.MELEE_DAMAGE:
                //powerupFactor = rf.ps.GetPowerupMultiplier(Utility.Gameplay.Powerups.overTimePowerupTypes.MELEE_DAMAGE_POWERUP);
                upgradeCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.ATTACK_SPEED).counter;
                enemyPenaltyFactor = 0f;
                statValue = Utility.Functions.PlayerStats.getPlayerMeleeDamage(RuntimeSpecs.playerLevel, upgradeCounter, powerupFactor, enemyPenaltyFactor);
                break;
            case Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH:
                upgradeCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.MAX_HEALTH).counter;
                statValue = Utility.Functions.PlayerStats.getPlayerMaxHealth(RuntimeSpecs.playerLevel, upgradeCounter);
                break;
            case Utility.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION:
                //powerupFactor = rf.ps.GetPowerupMultiplier(Utility.Gameplay.Powerups.overTimePowerupTypes.DAMAGE_REDUCTION_POWERUP);
                upgradeCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.DAMAGE_REDUCTION).counter;
                enemyPenaltyFactor = 0f;
                statValue = Utility.Functions.PlayerStats.getPlayerDamageReduction(RuntimeSpecs.playerLevel, upgradeCounter, powerupFactor);
                break;
            case Utility.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED:
                //powerupFactor = rf.ps.GetPowerupMultiplier(Utility.Gameplay.Powerups.overTimePowerupTypes.MOVEMENT_SPEED_POWERUP);
                upgradeCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.MOVEMENT_SPEED).counter;
                enemyPenaltyFactor = rf.pm.slowDownFactor;
                statValue = Utility.Functions.PlayerStats.getPlayerMovementSpeed(RuntimeSpecs.playerLevel, upgradeCounter, powerupFactor, enemyPenaltyFactor);
                rf.pm.velocityFactor = statValue;
                break;
        }
    }
}
