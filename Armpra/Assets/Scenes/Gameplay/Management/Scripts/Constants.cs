using UnityEngine;
public class Constants {
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
    }

    public class Gameplay {

        public class Camera {
            public static float HORIZONTAL_CAMERA_OFFSET;
            public static float VERTICAL_CAMERA_OFFSET;
            public const float SMOOTH_SPEED = 3f;
        }

        public class Background {
            public const int SHAPES_AMOUNT = 25;
            public const float SHAPES_SIZE_VAR = 1f;
            public const float SHAPES_POSITION_SPEED_FACTOR = 0.4f;
            public const float SHAPES_ROTATION_SPEED_FACTOR = 0.1f;

            public static float MAP_WIDTH;
            public static float MAP_HEIGHT;
        }

        public class Player {
            public enum headTypes {
                CWC, CWL1, CWR1, CWL2, CWR2, LWC, LWL1, LWR1, RWC, RWL1, RWR1
            }

            public const int MAX_SIZE_CHANGE = 75;
            public const float PLAYER_ACCELERATION = 0.15f;
            public enum playerStatTypes {
                ATTACK_SPEED, MELEE_DAMAGE, MAX_HEALTH, DAMAGE_REDUCTION, MOVEMENT_SPEED
            }

        }

        public class Joystick {
            public const int JOYSTICK_TO_VECTOR_FACTOR = 2;
        }

        public class Powerups {
            public enum instantPowerupTypes {
                INSTANT_HEAL_POWERUP, COIN_PACK_POWERUP, XP_PACK_POWERUP
            }
            public enum overTimePowerupTypes {
                ATTACK_SPEED_POWERUP, MELEE_DAMAGE_POWERUP, HEALTH_REGEN_POWERUP, DAMAGE_REDUCTION_POWERUP, RANGED_DAMAGE_POWERUP, MOVEMENT_SPEED_POWERUP, ZOOM_OUT_POWERUP
            }
            public enum instantiatorPowerupTypes {
                SHIELD_POWERUP
            }
        }

        public class Manager {
            public enum storeSource {
                WIN_MENU, LOST_MENU
            }
            public enum gameState {
                PLAY, PAUSE, WIN, LOST, STORE, NEW_ENEMY_FOUND
            }
        }

        public class Bullet {
            public enum bulletTypes {
                NORMAL, HV, EXPLOSIVE, POISONOUS
            }
        }

        public class Store {
            public const float UPGRADES_FACTOR = 0.001f;
            public enum storeItem {
                MELEE_DAMAGE, ATTACK_SPEED, MAX_HEALTH, DAMAGE_REDUCTION, MOVEMENT_SPEED,
                NORMAL, HV, EXPLOSIVE, POISONOUS, BULLET_SPEED, BULLET_EFFECT,
                POWERUP_EFFECT, POWERUP_DURATION, POWERUP_SPAWN_FREQUENCY
            }
        }
    }

    public class Data {
        public enum dataTypes { PLAYER_MAP_DATA, STORE_DATA, AUDIO_DATA };


        public static readonly string PLAYER_MAP_DATA = "player_map.data";
        public static readonly string STORE_DATA = "store.data";
        public static readonly string AUDIO_DATA = "audio.data";

        public static readonly string WINDOWS_PATH_ROOT = Application.dataPath + "/Saves/";
        public static readonly string ANDROID_PATH_ROOT = "/storage/emulated/0/Armpra/Saves/";
    }

    public class Timers {
        public const float SHAPE_SPAWN_TIMER = 0.01f;
        public const float ENEMY_SPAWN_TIMER = 0.5f;
    }

    public class Functions {
        public static float getPrevXpMilestone(int playerLevel) {
            return 200f * Mathf.Pow(playerLevel, 1.5f) - 100f;
        }
        public static float getNextXpMilestone(int playerLevel) {
            return 200f * Mathf.Pow(playerLevel + 1, 1.5f) - 100f;
        }

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

        public static float getPowerupEffectMultiplier(int powerupEffectStoreCounter) {
            return 0.2f + powerupEffectStoreCounter / 125f;
        }

        public static float getPowerupDuration(int powerupDurationStoreCounter) {
            return 5f + 0.1f * powerupDurationStoreCounter;
        }

        public static float getPowerupSpawnTimer(int spawnTimerStoreCounter) {
            return 30f - 0.25f * spawnTimerStoreCounter;
        }

        public static float getDecreaseCurve(int mapLevel) {
            return (float)(100f / (0.5 * mapLevel + 1));
        }

        public static float getRoundedFloat(float number, int precision) {
            return Mathf.Round(number * Mathf.Pow(10f, precision)) / Mathf.Pow(10f, precision);
        }
    }

    public class Text {

        public static string[] enemyNames = {
            "Scout", "Basic", "Double Head", "Bolt Action", "Shotgun", "Machine Gun", "Slower", "Kamikaze", "Tank", "Ninja" };//, "Pyro"

        public static string[] enemyDescriptions = {
            "This is a small devil, not dangerous but very agile. Try to stay away from it as much as possible.",
            "The first enemy that can actually shoot. Medium Speed, low-medium damage and a meh range.",
            "Double the number of heads, double the damage and even tankier. Do not underestimate it!",
            "This one can hit you from very far and deal a lot of damage. It will never stop until you do something about it.",
            "Low range - High damage enemy, It's one of those things you wouln't want them to lying around.",
            "This enemy is weak, yet very powerful with a huge rate of fire. Don't let it statatatatatatatatatatatatatatart.",
            "This one is boring, although supports the enemy team with great utility. Its bullets can slow you down.",
            "Nuff Said.",
            "This tough one takes a lot of time to take down. Be very patient.",
            "You can't see them. You won't know where they'll come from. You don't know their stats. Just run.",
            "This is a description. This is a description. This is a description. This is a description. This is a description."};

        public static int lastEnemyRemembered = -1;
    }

    public class Audio {
        public enum soundTypes {
            MUSIC, UI_SOUNDS, SFX 
        }
    }
}