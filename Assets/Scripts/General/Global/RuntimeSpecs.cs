using System.Collections.Generic;

public class RuntimeSpecs {
    //Player Level & Experience
    public static int playerLevel = 1;
    public static double currentPlayerXP = 0f;
    public static double remainingXP;
    public static double nextXPMilestone;
    public static double prevXPMilestone;
    public static double xpBetweenMilestones;
    public static double accumulatedXP;

    //Enemy Spawner
    public static int enemiesKilled;
    public static int enemiesSpawned;
    public static int maxEnemyCount;
    public static float ap;

    public static bool toBeRevived = false;
    public static float currentPlayerHealth = 100f;
    public static int currentCoins = 0;
    public static int mapLevel = 1;

    //Popup Messages
    public static int lastEnemyRemembered = -1;
    public static int newestEnemy = 0;
    public static int newestBoss = 0;

    //Per Map Level Stats
    public static double xpCollected = 0;
    public static int coinsCollected = 0;
    public static int levelsCollected = 0;
    public static List<StoreItem> unlockedItems = new List<StoreItem>();

    public static void ResetTempStats() {
        xpCollected = 0;
        coinsCollected = 0;
        levelsCollected = 0;
        unlockedItems.Clear();
    }

    //Long Term Game Stats
    public static int bulletsFired = 0;
    public static int bulletsHit = 0;
    public static float damageDealtToEnemies = 0f;
    public static float damageDealtToPlayer = 0f;
    public static int timesDead = 0;
    public static float hpHealed = 0;
    public static int totalEnemiesKilled = 0;
    public static int powerupsActivated = 0;
    public static int totalCoinsCollected = 0;
    public static int totalXpGained = 0;
    public static int itemUpgradesMade = 0;
    public static float timePlayed = 0;
    public static float roundsPlayed = 0;
}
