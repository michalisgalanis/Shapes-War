using UnityEngine;

public class Data {

    [System.Serializable]
    public class StoreData {
        public int[] storeUpgradesCounters;

        public StoreData() {
            Referencer rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            storeUpgradesCounters = new int[rf.ss.upgrades.Length];
            for (int i = 0; i < rf.ss.upgrades.Length; i++) {
                storeUpgradesCounters[i] = rf.ss.upgrades[i].counter;
            }
        }

        public void Load() {
            Referencer rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            for (int i = 0; i < rf.ss.upgrades.Length; i++) {
                rf.ss.upgrades[i].counter = storeUpgradesCounters[i];
            }
            rf.ss.forceRefresh();
            rf.pg.Refresh();
            rf.ps.EstimateStats();
            rf.ps.RefillStats();
        }
    }

    [System.Serializable]
    public class PlayerMapData{
        public float bestAttemptPercentage;
        public int playerLevel;
        public double currentPlayerXP;
        public int currentCoins;
        public int mapLevel;
        public int lastEnemyRemembered;

        public PlayerMapData() {
            playerLevel = RuntimeSpecs.playerLevel;
            currentPlayerXP = RuntimeSpecs.currentPlayerXP;
            currentCoins = RuntimeSpecs.currentCoins;
            bestAttemptPercentage = RuntimeSpecs.bap;
            mapLevel = RuntimeSpecs.mapLevel;
            lastEnemyRemembered = Constants.Text.lastEnemyRemembered;
        }

        public void Load() {
            Referencer rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            RuntimeSpecs.bap = bestAttemptPercentage;
            RuntimeSpecs.currentPlayerXP = currentPlayerXP;
            RuntimeSpecs.playerLevel = playerLevel;
            RuntimeSpecs.currentCoins = currentCoins;
            RuntimeSpecs.mapLevel = mapLevel;
            Constants.Text.lastEnemyRemembered = lastEnemyRemembered;
            rf.pg.Refresh();
            rf.ps.EstimateStats();
            rf.ps.RefillStats();
        }
    }

    [System.Serializable]
    public class AudioData {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public float uiVolume;

        public AudioData() {
            masterVolume=   RuntimeSpecs.masterVolume;
            musicVolume=    RuntimeSpecs.musicVolume;
            sfxVolume=      RuntimeSpecs.sfxVolume;
            uiVolume=       RuntimeSpecs.uiVolume;
        }

        public void Load() {
            RuntimeSpecs.masterVolume = masterVolume;
            RuntimeSpecs.musicVolume = musicVolume;
            RuntimeSpecs.sfxVolume = sfxVolume;
            RuntimeSpecs.uiVolume = uiVolume;
        }
    }
}