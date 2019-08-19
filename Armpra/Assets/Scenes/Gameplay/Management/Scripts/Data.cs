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

	//Store Upgrades
	public int attackSpeedUpgradeCounter;
	public int bulletEffectUpgradeCounter;
	public int bulletSpeedUpgradeCounter;
	public int damageReductionUpgradeCounter;
	public int maxHealthUpgradeCounter;
	public int meleeDamageUpgradeCounter;
	public int movementSpeedUpgradeCounter;
	public int powerupDurationCounter;
	public int powerupEffectCounter;
	public int powerupSpawnFrequencyCounter;

public Data(PlayerStats playerStatsComponent, Shield shield, GameObject gameManager, StoreSystem storeSystem) {
        //General variables
        currentLevel = gameManager.GetComponent<LevelGeneration>().currentLevel;
        bestAttemptPercentage = gameManager.GetComponent<GameplayManager>().bestAttemptPercentage;
        //Player variables
        playerLevel = playerStatsComponent.playerLevel;
        XP = playerStatsComponent.XP;

		//Store
		attackSpeedUpgradeCounter = storeSystem.attackSpeedUpgradeCounter;
		bulletEffectUpgradeCounter = storeSystem.bulletEffectUpgradeCounter;
		bulletSpeedUpgradeCounter = storeSystem.bulletSpeedUpgradeCounter;
		damageReductionUpgradeCounter = storeSystem.damageReductionUpgradeCounter;
		maxHealthUpgradeCounter = storeSystem.maxHealthUpgradeCounter;
		meleeDamageUpgradeCounter = storeSystem.meleeDamageUpgradeCounter;
		movementSpeedUpgradeCounter = storeSystem.movementSpeedUpgradeCounter;
		powerupDurationCounter = storeSystem.powerupDurationCounter;
		powerupEffectCounter = storeSystem.powerupEffectCounter;
		powerupSpawnFrequencyCounter = storeSystem.powerupSpawnFrequencyCounter;
    }
}
