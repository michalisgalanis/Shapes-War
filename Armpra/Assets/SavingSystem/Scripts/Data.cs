using UnityEngine;

[System.Serializable]
public class Data {
    //Variables to Store
    public int mapLevel;
    public float bestAttemptPercentage;
    public int playerLevel;
    public double currentPlayerXP;
    public int currentCoins;
    public int[] storeUpgradesCounters;
    public int lastEnemyRemembered;

    public Data() {
        Referencer rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        playerLevel = RuntimeSpecs.playerLevel;
        currentPlayerXP = RuntimeSpecs.currentPlayerXP;
        mapLevel = RuntimeSpecs.mapLevel;
        currentCoins = RuntimeSpecs.currentCoins;
        bestAttemptPercentage = RuntimeSpecs.bap;
        storeUpgradesCounters = new int[rf.ss.upgrades.Length];
        lastEnemyRemembered = Constants.Text.lastEnemyRemembered;
        for (int i = 0; i < rf.ss.upgrades.Length; i++) {
            storeUpgradesCounters[i] = rf.ss.upgrades[i].counter;
        }
        rf.ss.forceRefresh();
    }

    public void Load() {
        Referencer rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        RuntimeSpecs.mapLevel = mapLevel;
        RuntimeSpecs.bap = bestAttemptPercentage;
        RuntimeSpecs.currentPlayerXP = currentPlayerXP;
        RuntimeSpecs.playerLevel = playerLevel;
        RuntimeSpecs.currentCoins = currentCoins;
        Constants.Text.lastEnemyRemembered = lastEnemyRemembered;
        rf.ss.forceRefresh();
        rf.pg.Refresh();
        rf.ps.EstimateStats();
        rf.ps.RefillStats();
        for (int i = 0; i < rf.ss.upgrades.Length; i++) {
            rf.ss.upgrades[i].counter = storeUpgradesCounters[i];
        }
    }

}