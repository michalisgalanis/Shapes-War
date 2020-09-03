using System;
using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    //References
    private Referencer rf;
    private PlayerStats ps;
    private StoreSystem ss;

    //Runtime Variables
    [HideInInspector] public float effectValue; //Need to fix (Currently only works bc we only use it for 1 powerUp)
    public float[] overTimePowerUpsTimers;
    public bool[] activeOvertimePowerups;
    public float generalPowerupMultiplier;
    private float MAX_BORDER;
    public ParticleSystem[] particles;
    private EnemyWeapon[] ews;

    public GameObject playerPrefab;
    private GameObject currentPlayer;
    private GameObject shield;
    private void Start() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        ss = rf.ss;
        ps = rf.ps;
        for (int i = 0; i < Enum.GetNames(typeof(Utility.Gameplay.Powerups.overTimePowerupTypes)).Length; i++) {
            activeOvertimePowerups[i] = false;
            overTimePowerUpsTimers[i] = 0f;
        }
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
    }

    public void OverTimePowerUpEnable(Utility.Gameplay.Powerups.overTimePowerupTypes type, Color color) {
        int effectDurationStoreCounter = ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POWERUP_DURATION).counter;
        int storeCounter = ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
        int powerUpIndex = 0;

        for (int i = 0; i < Enum.GetValues(typeof(Utility.Gameplay.Powerups.overTimePowerupTypes)).Length; i++) {
            if ((Utility.Gameplay.Powerups.overTimePowerupTypes)i == type) {
                powerUpIndex = i;
                break;
            }
        }

        generalPowerupMultiplier = Utility.Functions.PowerupStats.getPowerupEffectMultiplier(storeCounter);
        overTimePowerUpsTimers[powerUpIndex] = Utility.Functions.PowerupStats.getPowerupDuration(effectDurationStoreCounter);
        if (activeOvertimePowerups[powerUpIndex]) {
            if (type == Utility.Gameplay.Powerups.overTimePowerupTypes.SHIELD_POWERUP)
                shield.GetComponent<Shield>().RestoreShieldStats();
            else if (type == Utility.Gameplay.Powerups.overTimePowerupTypes.CLONE_PLAYER_POWERUP)
                currentPlayer.transform.position = new Vector3(UnityEngine.Random.Range(-MAX_BORDER, MAX_BORDER), UnityEngine.Random.Range(-MAX_BORDER, MAX_BORDER), 0);
            else if (type == Utility.Gameplay.Powerups.overTimePowerupTypes.ENEMY_CHAOS_POWERUP) {
                foreach (EnemyWeapon ew in ews)
                    ew.range /= 2;
                ews = rf.spawnedEnemies.GetComponentsInChildren<EnemyWeapon>();
                foreach (EnemyWeapon ew in ews)
                    ew.range *= 2;
            }
        } else {
            activeOvertimePowerups[powerUpIndex] = true;
            switch (type) {
                case Utility.Gameplay.Powerups.overTimePowerupTypes.ATTACK_SPEED_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    ps.UpdatePowerupMultiplier(Utility.Gameplay.Player.playerStatTypes.ATTACK_SPEED, effectValue);
                    StartCoroutine(GenericPowerup(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.DAMAGE_REDUCTION_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    ps.UpdatePowerupMultiplier(Utility.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION, effectValue);
                    StartCoroutine(GenericPowerup(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.HEALTH_REGEN_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    effectValue = (1 + generalPowerupMultiplier) * rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH) / 3000;
                    StartCoroutine(GenericPowerup(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.MELEE_DAMAGE_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    ps.UpdatePowerupMultiplier(Utility.Gameplay.Player.playerStatTypes.MELEE_DAMAGE, effectValue);
                    StartCoroutine(GenericPowerup(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.MOVEMENT_SPEED_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    ps.UpdatePowerupMultiplier(Utility.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED, effectValue);
                    StartCoroutine(GenericPowerup(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.RANGED_DAMAGE_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    ps.UpdatePowerupMultiplier(Utility.Gameplay.Player.playerStatTypes.RANGED_DAMAGE, effectValue);
                    StartCoroutine(GenericPowerup(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.ENEMY_CHAOS_POWERUP:
                    StartCoroutine(UnleashEnemyChaos(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.REPULSION_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    StartCoroutine(RepulseEnemies(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.POWERUP_ATTRACTOR_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    StartCoroutine(AttractPowerups(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.CLONE_PLAYER_POWERUP:
                    StartCoroutine(PlayerClone(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.ZOOM_OUT_POWERUP:
                    StartCoroutine(ZoomOut(powerUpIndex));
                    break;
                case Utility.Gameplay.Powerups.overTimePowerupTypes.SHIELD_POWERUP:
                    InstantiateParticles(powerUpIndex, color);
                    StartCoroutine(Shield(powerUpIndex));
                    break;
            }
        }
    }

    public void InstantEffectPowerupEnable(Utility.Gameplay.Powerups.instantPowerupTypes type, Color color) {
        int storeCounter = ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
        generalPowerupMultiplier = Utility.Functions.PowerupStats.getPowerupEffectMultiplier(storeCounter);

        switch (type) {
            case Utility.Gameplay.Powerups.instantPowerupTypes.INSTANT_HEAL_POWERUP:
                rf.ps.InstantHeal(generalPowerupMultiplier, true);
                Vector3 initialScale = rf.healingParticles.transform.localScale;
                float sizeIncrease = Mathf.Max(PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1), 1);
                ParticleSystem healingParticles = Instantiate(rf.healingParticles, rf.player.transform.localPosition, Quaternion.identity);
                ParticleSystem.MainModule main = healingParticles.main;
                main.duration = 0.5f;
                main.startColor = new ParticleSystem.MinMaxGradient(color);
                healingParticles.Play();
                healingParticles.transform.localScale = initialScale * sizeIncrease;
                healingParticles.transform.parent = rf.player.transform;
                break;
            case Utility.Gameplay.Powerups.instantPowerupTypes.COIN_PACK_POWERUP:
                CoinSystem.AddCoins((int)(20 + 100 * RuntimeSpecs.playerLevel * generalPowerupMultiplier));
                break;
            case Utility.Gameplay.Powerups.instantPowerupTypes.XP_PACK_POWERUP:
                rf.pe.AddXP((double)(20 + 100 * RuntimeSpecs.playerLevel * generalPowerupMultiplier));
                break;
            case Utility.Gameplay.Powerups.instantPowerupTypes.TELEPORT_POWERUP:
                rf.pm.generateRandomMovement();
                break;
            case Utility.Gameplay.Powerups.instantPowerupTypes.RESURRECTION_POWERUP:
                Debug.Log("To be revived!");
                RuntimeSpecs.toBeRevived = true;
                break;
        }
    }

    public IEnumerator GenericPowerup(int powerUpIndex) {
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }
        activeOvertimePowerups[powerUpIndex] = false;
    }
    public IEnumerator PlayerClone(int powerUpIndex) {
        Vector3 temp = new Vector3(UnityEngine.Random.Range(-MAX_BORDER, MAX_BORDER), UnityEngine.Random.Range(-MAX_BORDER, MAX_BORDER), 0);
        currentPlayer = Instantiate(playerPrefab, temp, Quaternion.identity);
        rf.enemyTarget = currentPlayer.transform;
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }
        rf.enemyTarget = rf.player.transform;
        Destroy(currentPlayer);
        activeOvertimePowerups[powerUpIndex] = false;
    }
    public IEnumerator HealthRegen(int powerUpIndex) {
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            rf.ps.InstantHeal((generalPowerupMultiplier / Application.targetFrameRate), false);
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }
        activeOvertimePowerups[powerUpIndex] = false;
    }
    public IEnumerator ZoomOut(int powerUpIndex) {
        rf.camFollowPlayer.maxZoomOutFactor = Mathf.Clamp(generalPowerupMultiplier, 0, 2);
        rf.camFollowPlayer.zoomOutEffectActive = true;
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= Time.deltaTime;
            yield return null;
        }
        activeOvertimePowerups[powerUpIndex] = false;
        effectValue = 0f;
        rf.camFollowPlayer.zoomOutEffectActive = false;
        rf.camFollowPlayer.smoothSpeed /= (1 + generalPowerupMultiplier);
        rf.camFollowPlayer.maxZoomOutFactor = generalPowerupMultiplier;
    }
    public IEnumerator Shield(int powerUpIndex) {
        Vector3 initialScale = rf.shield.transform.localScale;
        float sizeIncrease = Mathf.Max(PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1), 1);
        shield = Instantiate(rf.shield, rf.player.transform.localPosition, Quaternion.identity);
        shield.transform.localScale = initialScale * sizeIncrease;
        shield.transform.parent = rf.player.transform;
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }
        activeOvertimePowerups[powerUpIndex] = false;
        Destroy(shield);
    }
    public IEnumerator UnleashEnemyChaos(int powerUpIndex) {
        ews = rf.spawnedEnemies.GetComponentsInChildren<EnemyWeapon>();

        foreach (EnemyWeapon ew in ews) {
            //Debug.Log("Range before doubling = "+ ew.range);
            ew.range *= 2;
            //Debug.Log("Range after doubling = " + ew.range);
        }

        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }

        foreach (EnemyWeapon ew in ews)
            ew.range /= 2;
    }

    public IEnumerator RepulseEnemies(int powerUpIndex) {
        PointEffector2D effector = rf.player.GetComponent<PointEffector2D>();
        effector.enabled = true;
        effector.colliderMask |= 1 << LayerMask.NameToLayer(Utility.Layers.ENEMY_LAYER_NAME); //tick enemy layer
        effector.colliderMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.POWERUPS_LAYER_NAME)); //untick powerups layer
        effector.forceMagnitude = 10000 * generalPowerupMultiplier;
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }

        effector.colliderMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.ENEMY_LAYER_NAME)); //untick enemy layer
        effector.forceMagnitude = 0;
        effector.enabled = false;
    }

    public IEnumerator AttractPowerups(int powerUpIndex) {
        PointEffector2D effector = rf.player.GetComponent<PointEffector2D>();
        effector.enabled = true;

        effector.colliderMask |= 1 << LayerMask.NameToLayer(Utility.Layers.POWERUPS_LAYER_NAME); //tick powerups layer
        effector.colliderMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.ENEMY_LAYER_NAME)); //untick enemy layer
        effector.drag = 10;
        effector.angularDrag = 5;

        effector.forceMagnitude = -500 * generalPowerupMultiplier;
        while (overTimePowerUpsTimers[powerUpIndex] > 0f) {
            overTimePowerUpsTimers[powerUpIndex] -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }

        effector.colliderMask &= ~(1 << LayerMask.NameToLayer(Utility.Layers.POWERUPS_LAYER_NAME)); //untick powerups layer
        effector.forceMagnitude = 0;
        effector.enabled = false;
    }

    public void InstantiateParticles(int powerUpIndex, Color color) {
        Vector3 initialScale = rf.healingParticles.transform.localScale;
        float sizeIncrease = Mathf.Max(PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1), 1);

        particles[powerUpIndex] = Instantiate(rf.healingParticles, rf.player.transform.localPosition, Quaternion.identity);
        ParticleSystem.MainModule main = particles[powerUpIndex].main;
        main.duration = overTimePowerUpsTimers[powerUpIndex];
        main.startColor = new ParticleSystem.MinMaxGradient(color);
        particles[powerUpIndex].Play();
        particles[powerUpIndex].transform.localScale = initialScale * sizeIncrease;
        particles[powerUpIndex].transform.parent = rf.player.transform;
    }

    public void DisableAllPowerups() {
        StopAllCoroutines();
        for (int i = 0; i < activeOvertimePowerups.Length; i++) {
            activeOvertimePowerups[i] = false;
            overTimePowerUpsTimers[i] = -1f;
        }
    }
}