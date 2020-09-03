using UnityEngine;

public class Data {

    [System.Serializable]
    public class PlayerMapData {
        public float bestAttemptPercentage;
        public int playerLevel;
        public double currentPlayerXP;
        public int currentCoins = 0;
        public int mapLevel;
        public int lastEnemyRemembered;

        public PlayerMapData() {
            playerLevel = RuntimeSpecs.playerLevel;
            currentPlayerXP = RuntimeSpecs.currentPlayerXP;
            currentCoins = RuntimeSpecs.currentCoins;
            mapLevel = RuntimeSpecs.mapLevel;
            lastEnemyRemembered = RuntimeSpecs.lastEnemyRemembered;
        }

        public void Load() {
            Referencer rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            RuntimeSpecs.currentPlayerXP = currentPlayerXP;
            RuntimeSpecs.playerLevel = playerLevel;
            RuntimeSpecs.currentCoins = currentCoins;
            RuntimeSpecs.mapLevel = mapLevel;
            RuntimeSpecs.lastEnemyRemembered = lastEnemyRemembered;
            rf.ps.EstimateStats();
            rf.ps.RefillStats();
            UnlockSystem.CheckUnlockProgress();
            CoinSystem.RefreshCoins();
            rf.pe.RefreshXP();
            rf.pg.UpdateVisuals();
            rf.wp.ResetShooting();
        }

        public static void Reset() {
            RuntimeSpecs.playerLevel = 1;
            RuntimeSpecs.mapLevel = 1;
            RuntimeSpecs.currentPlayerXP = 0;
            RuntimeSpecs.nextXPMilestone = 0;
            RuntimeSpecs.prevXPMilestone = 0;
            RuntimeSpecs.xpBetweenMilestones = 0;
            RuntimeSpecs.accumulatedXP = 0;
        }
    }

    [System.Serializable]
    public class StoreData {
        public int[] storeUpgradesCounters;

        public StoreData() {
            Referencer rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            storeUpgradesCounters = new int[rf.ss.upgrades.Length];
            for (int i = 0; i < rf.ss.upgrades.Length; i++) {
                storeUpgradesCounters[i] = rf.ss.upgrades[i].counter;
            }
        }

        public void Load() {
            Referencer rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            for (int i = 0; i < rf.ss.upgrades.Length; i++) {
                rf.ss.upgrades[i].counter = storeUpgradesCounters[i];
            }
            rf.ss.forceRefresh();
            rf.ps.EstimateStats();
            rf.ps.RefillStats();
        }

        public static void Reset() {

        }
    }

    [System.Serializable]
    public class SettingsData {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public float uiVolume;
        public bool joystickType;
        public bool showMusicInfo;
        public bool displayFPSCounter;
        public bool animationsEnabled;
        public float animationSpeedFactor;
        public bool vibrationsEnabled;

        public SettingsData() {
            masterVolume = GameSettings.masterVolume;
            musicVolume = GameSettings.musicVolume;
            sfxVolume = GameSettings.sfxVolume;
            uiVolume = GameSettings.uiVolume;
            joystickType = GameSettings.currentControlType == Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK;
            showMusicInfo = GameSettings.showMusicInfo;
            displayFPSCounter = GameSettings.displayFPSCounter;
            animationsEnabled = GameSettings.animationsEnabled;
            animationSpeedFactor = GameSettings.animationSpeedFactor;
            vibrationsEnabled = GameSettings.vibrationsEnabled;
        }

        public void Load() {
            GameSettings.masterVolume = masterVolume;
            GameSettings.musicVolume = musicVolume;
            GameSettings.sfxVolume = sfxVolume;
            GameSettings.uiVolume = uiVolume;
            GameSettings.currentControlType = (joystickType) ? Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK : Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK;
            GameSettings.showMusicInfo = showMusicInfo;
            GameSettings.displayFPSCounter = displayFPSCounter;
            GameSettings.animationsEnabled = animationsEnabled;
            GameSettings.animationSpeedFactor = animationSpeedFactor;
            GameSettings.animationSpeed = animationsEnabled ? (animationSpeedFactor == 1 ? Utility.Gameplay.Animations.animationSpeed.RELAXED : Utility.Gameplay.Animations.animationSpeed.FAST) : Utility.Gameplay.Animations.animationSpeed.OFF;
            GameSettings.vibrationsEnabled = vibrationsEnabled;
        }

        public static void Reset() {
            GameSettings.masterVolume = 1f;
            GameSettings.uiVolume = 0.5f;
            GameSettings.musicVolume = 0.5f;
            GameSettings.sfxVolume = 0.5f;
            GameSettings.currentControlType = Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK;
            GameSettings.showMusicInfo = true;
            GameSettings.displayFPSCounter = false;
            GameSettings.animationsEnabled = true;
            GameSettings.animationSpeedFactor = 1f;
            GameSettings.animationSpeed = Utility.Gameplay.Animations.animationSpeed.FAST;
            GameSettings.vibrationsEnabled = true;
        }
    }

    [System.Serializable]
    public class StatsData {
        public int bulletsFired;
        public int bulletsHit;
        public float damageDealtToEnemies;
        public float damageDealtToPlayer;
        public int timesDead;
        public float hpHealed;
        public int totalEnemiesKilled;
        public int powerupsActivated;
        public int totalCoinsCollected;
        public int totalXpGained;
        public int itemUpgradesMade;
        public float playtime;
        public float roundsPlayed;

        public StatsData() {
            bulletsFired = RuntimeSpecs.bulletsFired;
            damageDealtToEnemies = RuntimeSpecs.damageDealtToEnemies;
            damageDealtToPlayer = RuntimeSpecs.damageDealtToPlayer;
            timesDead = RuntimeSpecs.timesDead;
            hpHealed = RuntimeSpecs.hpHealed;
            totalEnemiesKilled = RuntimeSpecs.totalEnemiesKilled;
            powerupsActivated = RuntimeSpecs.powerupsActivated;
            totalCoinsCollected = RuntimeSpecs.totalCoinsCollected;
            totalXpGained = RuntimeSpecs.totalXpGained;
            itemUpgradesMade = RuntimeSpecs.itemUpgradesMade;
            playtime = RuntimeSpecs.timePlayed;
            roundsPlayed = RuntimeSpecs.roundsPlayed;
        }

        public void Load() {
            RuntimeSpecs.bulletsFired = bulletsFired;
            RuntimeSpecs.bulletsHit = bulletsHit;
            RuntimeSpecs.damageDealtToEnemies = damageDealtToEnemies;
            RuntimeSpecs.damageDealtToPlayer = damageDealtToPlayer;
            RuntimeSpecs.timesDead = timesDead;
            RuntimeSpecs.hpHealed = hpHealed;
            RuntimeSpecs.totalEnemiesKilled = totalEnemiesKilled;
            RuntimeSpecs.powerupsActivated = powerupsActivated;
            RuntimeSpecs.totalCoinsCollected = totalCoinsCollected;
            RuntimeSpecs.totalXpGained = totalXpGained;
            RuntimeSpecs.itemUpgradesMade = itemUpgradesMade;
            RuntimeSpecs.timePlayed = playtime;
            RuntimeSpecs.roundsPlayed = roundsPlayed;
        }

        public static void Reset() {
            RuntimeSpecs.bulletsFired = 0;
            RuntimeSpecs.bulletsHit = 0;
            RuntimeSpecs.damageDealtToEnemies = 0f;
            RuntimeSpecs.damageDealtToPlayer = 0f;
            RuntimeSpecs.timesDead = 0;
            RuntimeSpecs.hpHealed = 0;
            RuntimeSpecs.totalEnemiesKilled = 0;
            RuntimeSpecs.powerupsActivated = 0;
            RuntimeSpecs.totalCoinsCollected = 0;
            RuntimeSpecs.totalXpGained = 0;
            RuntimeSpecs.itemUpgradesMade = 0;
            RuntimeSpecs.timePlayed = 0;
            RuntimeSpecs.roundsPlayed = 0;
        }
    }
}