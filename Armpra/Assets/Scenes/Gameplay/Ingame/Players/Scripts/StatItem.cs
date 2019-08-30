using UnityEngine;

public class StatItem {
    //References
    private readonly Referencer rf;

    //Runtime Variables
    public Constants.Gameplay.Player.playerStatTypes statType;
    public int playerLevel;
    public int upgradeCounter;
    public float powerupFactor;
    public float enemyPenaltyFactor;
    public float statValue;

    public StatItem(int playerLevel, Constants.Gameplay.Player.playerStatTypes statTypes) {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        this.playerLevel = playerLevel;
        statType = statTypes;
        EstimateStat();
    }

    public void EstimateStat() {
        switch (statType) {
            case Constants.Gameplay.Player.playerStatTypes.ATTACK_SPEED:
            powerupFactor = rf.ps.GetPowerupMultiplier(Constants.Gameplay.Powerups.overTimePowerupTypes.ATTACK_SPEED_POWERUP);
            upgradeCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.ATTACK_SPEED).counter;
            enemyPenaltyFactor = 0f;
            statValue = Constants.Functions.getPlayerAttackSpeed(playerLevel, upgradeCounter, powerupFactor);
            rf.wp.shootingTime = statValue;
            break;
            case Constants.Gameplay.Player.playerStatTypes.MELEE_DAMAGE:
            powerupFactor = rf.ps.GetPowerupMultiplier(Constants.Gameplay.Powerups.overTimePowerupTypes.MELEE_DAMAGE_POWERUP);
            upgradeCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.ATTACK_SPEED).counter;
            enemyPenaltyFactor = 0f;
            statValue = Constants.Functions.getPlayerMeleeDamage(playerLevel, upgradeCounter, powerupFactor, enemyPenaltyFactor);
            break;
            case Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH:
            upgradeCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.MAX_HEALTH).counter;
            statValue = Constants.Functions.getPlayerMaxHealth(playerLevel, upgradeCounter);
            break;
            case Constants.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION:
            powerupFactor = rf.ps.GetPowerupMultiplier(Constants.Gameplay.Powerups.overTimePowerupTypes.IMMUNITY_POWERUP);
            upgradeCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.DAMAGE_REDUCTION).counter;
            enemyPenaltyFactor = 0f;
            statValue = Constants.Functions.getPlayerDamageReduction(playerLevel, upgradeCounter, powerupFactor);
            break;
            case Constants.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED:
            powerupFactor = rf.ps.GetPowerupMultiplier(Constants.Gameplay.Powerups.overTimePowerupTypes.MOVEMENT_SPEED_POWERUP);
            upgradeCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.MOVEMENT_SPEED).counter;
            enemyPenaltyFactor = 0f;
            statValue = Constants.Functions.getPlayerMovementSpeed(playerLevel, upgradeCounter, powerupFactor, enemyPenaltyFactor);
            rf.pm.velocityFactor = statValue;
            break;
        }
    }
}
