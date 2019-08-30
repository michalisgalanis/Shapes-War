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

    public Data() {
        Referencer rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        //Variables to Store
        playerLevel = RuntimeSpecs.playerLevel;
        currentPlayerXP = RuntimeSpecs.currentPlayerXP;
        mapLevel = RuntimeSpecs.mapLevel;
        currentCoins = RuntimeSpecs.currentCoins;
        bestAttemptPercentage = RuntimeSpecs.bap;
        storeUpgradesCounters = new int[rf.ss.upgrades.Length];
        for (int i = 0; i < rf.ss.upgrades.Length; i++) {
            storeUpgradesCounters[i] = rf.ss.upgrades[i].counter;
        }
    }
}
