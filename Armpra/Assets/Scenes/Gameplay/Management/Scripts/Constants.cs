﻿using UnityEngine;
public class Constants {
    public class Scenes {
        public const string MAIN_MENU_SCENE_NAME = "Main Menu";
        public const string GAMEPLAY_SCENE_NAME = "Gameplay";
    }

    public class Layers {
        public const string PLAYER_LAYER_NAME = "Player";
        public const string ENEMY_LAYER_NAME = "Enemy";
        public const string PROJECTILES_LAYER_NAME = "Projectiles";
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
            public const int MAX_VISUAL_CHANGES_LEVEL = 20;
            public const float PLAYER_ACCELERATION = 0.15f;
            public enum playerStatTypes { ATTACK_SPEED, MELEE_DAMAGE, MAX_HEALTH, DAMAGE_REDUCTION, MOVEMENT_SPEED }

        }

        public class Joystick {
            public const int JOYSTICK_TO_VECTOR_FACTOR = 2;
        }

        public class Powerups {
            public enum instantPowerupTypes { INSTANT_HEAL_POWERUP }
            public enum overTimePowerupTypes { ATTACK_SPEED_POWERUP, MELEE_DAMAGE_POWERUP, HEALTH_REGEN_POWERUP, IMMUNITY_POWERUP, RANGED_DAMAGE_POWERUP, MOVEMENT_SPEED_POWERUP }
            public enum instantiatorPowerupTypes { SHIELD_POWERUP }
        }

        public class Manager {
            public enum storeSource { WIN_MENU, LOST_MENU }
            public enum gameState { PLAY, PAUSE, WIN, LOST, STORE }
        }


        public class Bullet {
            public enum bulletTypes { NORMAL, HV, EXPLOSIVE, POISONOUS }
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
            return 0.4f + (Mathf.Clamp(playerLevel, 0f, Gameplay.Player.MAX_VISUAL_CHANGES_LEVEL) - 1) * 0.02f;
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
    }

    public class FileLocations {
        public static readonly string FILE_SUFFIX = "/progress.stpd";
        public static readonly string WINDOWS_PATH = Application.dataPath + FILE_SUFFIX;
        public static readonly string ANDROID_PATH = "/storage/emulated/0/Armpra" + FILE_SUFFIX;
    }
}