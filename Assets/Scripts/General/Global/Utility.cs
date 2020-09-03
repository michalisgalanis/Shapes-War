using UnityEngine;
using System.IO;

public class Utility {
    public class Scenes {
        public const string MAIN_MENU_SCENE_NAME = "Main Menu";
        public const string GAMEPLAY_SCENE_NAME = "Gameplay";
    }
    public class Layers {
        public const string PLAYER_LAYER_NAME = "Player";
        public const string ENEMY_LAYER_NAME = "Enemy";
        public const string PROJECTILES_LAYER_NAME = "Projectiles";
        public const string POWERUPS_LAYER_NAME = "Powerups";
    }
    public class Tags {
        public const string GAME_MANAGER_TAG = "GameController";
        public const string PLAYER_TAG = "Player";
        public const string ENEMY_TAG = "Enemy";
        public const string SHIELD_TAG = "Shield";
        public const string MAP_BOUNDS_TAG = "MapBounds";
        public const string POWERUPS_TAG = "Powerups";
        public const string AUDIO_MANAGER_TAG = "AudioManager";
        public const string PROJECTILES_TAG = "Projectiles";
    }

    public class Gameplay {

        public class Ads {
            public enum rewardedAdType {
                DOUBLE_XP_COINS, TRIPLE_COINS, TRIPLE_XP
            }
        }

        public class Background {
            public const float SHAPE_SPAWN_TIMER = 0.01f;
            public const int SHAPES_AMOUNT = 25;
            public const float SHAPES_SIZE_VAR = 1f;
            public const float SHAPES_POSITION_SPEED_FACTOR = 0.4f;
            public const float SHAPES_ROTATION_SPEED_FACTOR = 0.1f;

            public static float MAP_WIDTH;
            public static float MAP_HEIGHT;
        }
        
        public class Camera {
            public static float HORIZONTAL_CAMERA_OFFSET;
            public static float VERTICAL_CAMERA_OFFSET;
            public const float SMOOTH_SPEED = 3f;
        }

        public class Player {
            public enum headTypes {
                CWC, CWL1, CWR1, CWL2, CWR2, LWC, LWL1, LWR1, RWC, RWL1, RWR1
            }

            public const int MAX_SIZE_CHANGE = 75;
            public const float PLAYER_ACCELERATION = 0.15f;
            public enum playerStatTypes {
                ATTACK_SPEED, MELEE_DAMAGE, RANGED_DAMAGE, MAX_HEALTH, DAMAGE_REDUCTION, MOVEMENT_SPEED
            }

            public enum skinTypes {
                BOX_3D, CIRCLES 
            }

        }

        public class Enemy {
            public const int MAX_ENEMIES_SPAWNED = 50;
            public const float ENEMY_SPAWN_TIMER = 0.8f;
            public enum enemyTypes { SCOUT, BASIC, DOUBLE_HEAD, BOLT_ACTION, SHOTGUN, MACHINE_GUN, SLOWER, KAMIKAZE, TANK, NINJA };

            public enum bossTypes { BOSS_L1_SCOUT, BOSS_L2_BASIC, BOSS_L3_DOUBLE_HEAD, BOSS_L4_BOLT_ACTION, BOSS_L5_SHOTGUN, BOSS_L6_MACHINE_GUN, BOSS_L7_SLOWER, BOSS_L8_KAMIKAZE, BOSS_L9_TANK, BOSS_L10_NINJA }

            public class Text {

                public static string[] enemyNames = { "Scout", "Basic", "Double Head", "Bolt Action", "Shotgun", "Machine Gun", "Slower", "Kamikaze", "Tank", "Ninja" };//, "Pyro"


                public static string[] enemyDescriptions = {
                        "This small devil is not dangerous but agile. Try to keep a safe distance from it.",
                        "The first enemy that can actually shoot. Medium Speed, low-medium damage and a meh range.",
                        "Double the number of heads, double the damage and even tankier. Do not underestimate it!",
                        "This one can hit you from very far and deal a lot of damage. It will never stop until you do something about it.",
                        "Low range - High damage enemy, It's one of those things you wouln't want them to lying around.",
                        "This enemy is weak, yet very powerful with a huge rate of fire. Don't let it statatatatatatatatatatatatatatart.",
                        "This one is boring, although supports the enemy team with great utility. Its bullets can slow you down.",
                        "Sorry.",
                        "This giant one takes a lot of time to take down. Be very patient.",
                        "You can't see it. You don't know where it's gonna come from. You don't know its stats. The only thing you can do is to run."};
            }
        }

        public class Bullet {
            public enum bulletTypes {
                NORMAL, HV, EXPLOSIVE, POISONOUS, SNOW, PENETRATION, ELECTRICITY, SHOTGUN_CELL, MINIGUN_CELL, MISSILE
            }
        }

        public class Powerups {
            public enum powerUpTypes {
                instantPowerupTypes, overTimePowerupTypes
            }
            public enum instantPowerupTypes {
                COIN_PACK_POWERUP,
                INSTANT_HEAL_POWERUP,
                TELEPORT_POWERUP,
                XP_PACK_POWERUP,
                PUZZLE_POWERUP,
                RESURRECTION_POWERUP
            }
            public enum overTimePowerupTypes {
                ATTACK_SPEED_POWERUP,
                CLONE_PLAYER_POWERUP,
                DAMAGE_REDUCTION_POWERUP,
                HEALTH_REGEN_POWERUP,
                MELEE_DAMAGE_POWERUP,
                MOVEMENT_SPEED_POWERUP,
                RANGED_DAMAGE_POWERUP,
                SHIELD_POWERUP,
                ZOOM_OUT_POWERUP,
                RESURRECTION_POWERUP,
                ENEMY_CHAOS_POWERUP,
                REPULSION_POWERUP,
                POWERUP_ATTRACTOR_POWERUP 
            }

            public enum powerupTiers {
                TIER_0, TIER_1, TIER_2, TIER_3
            }

            public const int POWERUPS_TIER_0_LEVEL = 1;
            public const int POWERUPS_TIER_1_LEVEL = 5;
            public const int POWERUPS_TIER_2_LEVEL = 10;
            public const int POWERUPS_TIER_3_LEVEL = 25;

        }

        public class Animations {
            public enum animationSpeed {
                OFF, RELAXED, FAST
            }
        }

        public class Vibrations {
            public const long VIBRATION_BUTTON_PRESS = 20;
            public const long VIBRATION_PLAYER_DEATH = 30;
            public const long VIBRATION_PLAYER_HIT = 10;
            public const long VIBRATION_COMPLETE_LEVEL = 100;
            public const long VIBRATION_LOST = 70;
        }

        public class Controls {
            public const int JOYSTICK_TO_VECTOR_FACTOR = 2;

            public enum controlType {
                NORMAL_JOYSTICK, DUAL_ZONE_JOYSTICK
            }
        }

        public class Manager {
            public enum adSource {
                WIN_MENU, LOST_MENU
            }
            public enum storeSource {
                WIN_MENU, LOST_MENU
            }
            public enum gameState {
                PLAY, PAUSE, WIN, LOST, STORE, NEW_ENEMY_FOUND, AD_PLAYING, UNLOCKED_ITEMS
            }
            public enum mainMenuState {
                HOME, OPTIONS, STATS, CONFIRM_DELETE_PROGRESS
            }
            public enum storeState {
                PLAYER, POWERUPS, AMMO, SKINS, EXTRAS
            }
            public enum optionsState {
                SOUND, DISPLAY, MISC
            }
        }

        public class Store {
            public const float UPGRADES_FACTOR = 0.001f;
            public const int MAX_COUNTER = 100;

            public enum storeItem {
                MELEE_DAMAGE, RANGED_DAMAGE, ATTACK_SPEED, MAX_HEALTH, DAMAGE_REDUCTION, MOVEMENT_SPEED,
                NORMAL, HV, EXPLOSIVE, POISONOUS, SNOW, PENETRATION, ELECTRICITY,
                POWERUP_EFFECT, POWERUP_DURATION, POWERUP_SPAWN_FREQUENCY,
                BOX_3D_SKIN, CIRCLES_SKIN, DOTS_SKIN
            }
        }
    }

    public class Data {
        public enum dataTypes { PLAYER_MAP_DATA, STORE_DATA, SETTINGS_DATA, STATS_DATA };

        public static readonly string PLAYER_MAP_DATA = "player_map.data";
        public static readonly string STORE_DATA = "store.data";
        public static readonly string SETTINGS_DATA = "settings.data";
        public static readonly string STATS_DATA = "stats.data";

        public static readonly string WINDOWS_PATH_ROOT = Application.persistentDataPath;
        public static readonly string ANDROID_PATH_ROOT = Application.persistentDataPath;
        public static readonly string EDITOR_PATH_ROOT = Application.dataPath + "/Saves/";
    }

    public class Functions {

        public class ExperienceFunctions {
            public static float getPrevXpMilestone(int playerLevel) {
                return -410f + 300f * Mathf.Pow(playerLevel, 1.6f);
            }
            public static float getNextXpMilestone(int playerLevel) {
                return -410f + 300f * Mathf.Pow(playerLevel + 1, 1.6f);
            }
        }

        public class PlayerStats {
            public static float getPlayerSizeAtLevel(int playerLevel) {
                return 0.4f + (Mathf.Clamp(playerLevel, 0f, Gameplay.Player.MAX_SIZE_CHANGE) - 1) * 0.003f;
            }

            public static float getPlayerAttackSpeed(int playerLevel, int upgradeCounter, float powerupFactor) {
                return (1f - 0.1f * Mathf.Pow(playerLevel, 0.4f * (1 + upgradeCounter * Gameplay.Store.UPGRADES_FACTOR))) * (1 - powerupFactor);
            }

            public static float getPlayerMeleeDamage(int playerLevel, int upgradeCounter, float powerupFactor, float enemyPenalty) {
                return 0.05f * Mathf.Pow(playerLevel, 0.98f * (1 + upgradeCounter * Gameplay.Store.UPGRADES_FACTOR)) * (1 + powerupFactor) * (1 - enemyPenalty);
            }

            public static float getPlayerMaxHealth(int playerLevel, int upgradeCounter) {
                return 70f + 30f * Mathf.Pow(playerLevel, 1.5f * (1 + upgradeCounter * Gameplay.Store.UPGRADES_FACTOR));
            }

            public static float getPlayerDamageReduction(int playerLevel, int upgradeCounter, float powerupFactor) {
                return (Mathf.Sqrt(Mathf.Pow(playerLevel, 0.15f * (1 + upgradeCounter * Gameplay.Store.UPGRADES_FACTOR))) - 1f) * (1 + powerupFactor);
            }

            public static float getPlayerMovementSpeed(int playerLevel, int upgradeCounter, float powerupFactor, float enemyPenalty) {
                return (2.5f + Mathf.Pow(playerLevel, 0.099f * (1 + upgradeCounter * Gameplay.Store.UPGRADES_FACTOR))) * (1 + powerupFactor) * (1 - enemyPenalty);
            }

            public static float getPlayerRangedDamageExternalFactor(int playerLevel, int upgradeCounter, float powerupFactor, float enemyPenalty) {
                return (1f + 0.02f * Mathf.Pow(playerLevel, 1.3f * (1 + upgradeCounter * Gameplay.Store.UPGRADES_FACTOR))) * (1 + powerupFactor) * (1 - enemyPenalty);
            }
        }

        public class PowerupStats {
            public static float getPowerupEffectMultiplier(int powerupEffectStoreCounter) {
                return 0.2f + powerupEffectStoreCounter / 125f;
            }

            public static float getPowerupDuration(int powerupDurationStoreCounter) {
                return 5f + 0.1f * powerupDurationStoreCounter;
            }

            public static float getPowerupSpawnTimer(int spawnTimerStoreCounter) {
                return 30f - 0.25f * spawnTimerStoreCounter;
            }
        }

        public class EnemySpawn {
            public static int getMaxEnemyCount(int mapLevel) {
                return Mathf.RoundToInt(4f + Mathf.Pow(RuntimeSpecs.mapLevel, 1.05f));
            }

            public static float getDecreaseCurve(int mapLevel) {
                return (float)(100f / (0.5 * mapLevel + 1));
            }

            public static int getNewEnemyGenerationLevel(int mapLevel) {
                return Mathf.RoundToInt(4f * Mathf.Pow(mapLevel, 1.3f) + 1);
            }

            public static int getNewBossGenerationLevel(int mapLevel) {
                return getNewEnemyGenerationLevel(mapLevel) - 1;
            }
        }

        public class EnemyStats {
            public static float getEnemyMaxHealth(int mapLevel) {
                return 0.99f + 0.05f * Mathf.Pow(mapLevel, 1.5f);
            }

            public static float getEnemyMeleeDamageFactor(int mapLevel) {
                return 0.98f + 0.02f * Mathf.Pow(mapLevel, 1.1f);
            }

            public static float getEnemyRangedDamageFactor(int mapLevel) {
                return 0.15f + 0.007f * Mathf.Pow(mapLevel, 1.4f);
            }

            public static float getEnemyAttackSpeed(int mapLevel) {
                return 0.97f + 0.025f * Mathf.Pow(mapLevel, 1.02f);
            }

            public static float getEnemyXpGather(int mapLevel) {
                return 0.95f + 0.05f * Mathf.Pow(mapLevel, 1.02f);
            }

            public static float getEnemyCoinGather(int mapLevel) {
                return 0.95f + 0.05f * Mathf.Pow(mapLevel, 1.02f);
            }
        }

        public class CompleteLevelStats {

            public static float getCompleteLevelXp(int mapLevel) {
                return 70f * (Mathf.Pow(mapLevel, 0.8f) + 3);
            }

            public static float getCompleteLevelCoins(int mapLevel) {
                return 5f * (Mathf.Pow(mapLevel, 1.2f) + 2);
            }
        }

        public class GeneralFunctions {
            public static float getRoundedFloat(float number, int precision) {
                return Mathf.Round(number * Mathf.Pow(10f, precision)) / Mathf.Pow(10f, precision);
            }

            public static string compressCoinsText(float currentCoins) {
                string currentCoinsText = currentCoins.ToString();
                if (currentCoins > 1000000000)
                    currentCoinsText = getRoundedFloat(currentCoins / 1000000000, 1) + "B";
                else if (currentCoins > 1000000)
                    currentCoinsText = getRoundedFloat(currentCoins / 1000000, 1) + "M";
                else if (currentCoins > 1000)
                    currentCoinsText = getRoundedFloat(currentCoins / 1000, 1) + "K";
                return currentCoinsText;
            }

            public static Color generateColor(float h, float s, float v, float a) {
                return new Color(Color.HSVToRGB(h, s, v).r, Color.HSVToRGB(h, s, v).g, Color.HSVToRGB(h, s, v).b, a);
            }
            public static Color generateColor(SpriteRenderer sr) {
                Color.RGBToHSV(sr.color, out float h, out float s, out float v);
                float a = sr.color.a;
                return new Color(Color.HSVToRGB(h, s, v).r, Color.HSVToRGB(h, s, v).g, Color.HSVToRGB(h, s, v).b, a);
            }

            public static void Vibrate(long millis) {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (GameSettings.vibrationsEnabled) {
                Vibration.Cancel();
                Vibration.Vibrate(millis);
            }
#endif
            }
            public static void Vibrate(long millis, bool strongBoom) {
                //coming soon
#if UNITY_ANDROID && !UNITY_EDITOR
            Vibrate(millis);
#endif
            }
        }

        public class Utility {

            public static void TakeObjectScreenShot(GameObject gameObject, string folderName, Camera cam) {
                string fileName = gameObject.name + ".png";
                fileName = Application.dataPath + "/Snapshots/" + folderName + "/" + fileName;
                FileInfo info = new FileInfo(fileName);
                if (info.Exists)
                    File.Delete(fileName);
                else if (!info.Directory.Exists)
                    info.Directory.Create();

                RenderTexture renderTarget = RenderTexture.GetTemporary(1024, 1024);
                cam.aspect = 1024 / 1024;
                cam.orthographic = true;
                cam.targetTexture = renderTarget;
                cam.Render();

                RenderTexture.active = renderTarget;
                Texture2D tex = new Texture2D(renderTarget.width, renderTarget.height);
                tex.ReadPixels(new Rect(0, 0, renderTarget.width, renderTarget.height), 0, 0);
                File.WriteAllBytes(fileName, tex.EncodeToPNG());
                cam.targetTexture = null;
            }
        }
    }

    public class Audio {
        public enum soundTypes { MUSIC, UI_SOUNDS, SFX, BOSS_MUSIC };
        public static GameObject audioManager = null;
    }
}
