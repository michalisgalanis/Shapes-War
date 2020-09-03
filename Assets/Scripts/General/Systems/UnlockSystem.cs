using UnityEngine;

public class UnlockSystem {
    //References
    private static readonly Referencer rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();

    public static void CheckUnlockProgress() {
        CheckPowerupsUnlockProgress();
        CheckStoreUnlockProgress();
    }

    public static void CheckPowerupsUnlockProgress() {
        int playerLevel = RuntimeSpecs.playerLevel;
        rf.activePowerupTypes.Clear();
        foreach (GameObject powerup in rf.powerupTypes) {
            if (playerLevel >= Utility.Gameplay.Powerups.POWERUPS_TIER_0_LEVEL && powerup.GetComponent<PowerUp>().powerupTier == Utility.Gameplay.Powerups.powerupTiers.TIER_0)
                rf.activePowerupTypes.Add(powerup);
            if (playerLevel >= Utility.Gameplay.Powerups.POWERUPS_TIER_1_LEVEL && powerup.GetComponent<PowerUp>().powerupTier == Utility.Gameplay.Powerups.powerupTiers.TIER_1)
                rf.activePowerupTypes.Add(powerup);
            if (playerLevel >= Utility.Gameplay.Powerups.POWERUPS_TIER_2_LEVEL && powerup.GetComponent<PowerUp>().powerupTier == Utility.Gameplay.Powerups.powerupTiers.TIER_2)
                rf.activePowerupTypes.Add(powerup);
        }
    }

    private static void CheckStoreUnlockProgress() {
        foreach (StoreItem si in rf.ss.upgrades) {
            bool tempLocked = si.locked;
            si.locked = RuntimeSpecs.playerLevel < si.unlockableAt;
            si.refreshItem();
            if (tempLocked && !si.locked) RuntimeSpecs.unlockedItems.Add(si);
        }
    }
}
