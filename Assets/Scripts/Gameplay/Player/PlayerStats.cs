using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    private bool markedForDestruction = false;
    [HideInInspector] public bool godMode = false;
    private List<StatItem> statItems;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        statItems = new List<StatItem>();
    }

    public void Start() {
        for (int i = 0; i < Enum.GetValues(typeof(Utility.Gameplay.Player.playerStatTypes)).Length; i++) {
            statItems.Add(new StatItem((Utility.Gameplay.Player.playerStatTypes)Enum.GetValues(typeof(Utility.Gameplay.Player.playerStatTypes)).GetValue(i)));
        }
        GameObject shockwave = Instantiate(rf.shockwave, transform.localPosition, Quaternion.identity) as GameObject;
        shockwave.transform.parent = rf.spawnedParticles.transform;
        EstimateStats();
        RefillStats();
        RefreshVisuals();
    }

    public void RefreshVisuals() {
        float s = 1f - RuntimeSpecs.currentPlayerHealth / GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        foreach (GameObject playerBorder in rf.playerWings) playerBorder.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Utility.Functions.GeneralFunctions.generateColor(0f, s, 0.95f, 1f);
        foreach (GameObject playerHead in rf.playerHeads) playerHead.GetComponent<SpriteRenderer>().color = Utility.Functions.GeneralFunctions.generateColor(0f, s, 0.95f, 1f);
    }

    public void EstimateStats() {
        foreach (StatItem stat in statItems) stat.EstimateStat();
    }

    public float GetStatValueOf(Utility.Gameplay.Player.playerStatTypes statType) {
        foreach (StatItem stat in statItems)
            if (stat.statType == statType) {
                stat.EstimateStat();
                return stat.statValue;
            }
        return 0f;
    }

    public void EstimateStat(Utility.Gameplay.Player.playerStatTypes statType) {
        foreach (StatItem stat in statItems) if (stat.statType == statType) stat.EstimateStat();
    }

    public void TakeDamage(float damage, bool trueDamage, bool ranged) {
        float realDamage = godMode ? 0 : trueDamage ? damage : Mathf.Clamp(damage * (1 - GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION)), 0f, damage);
        if (GetComponent<HitEffect>() != null) GetComponent<HitEffect>().Hit(1f, 0.3f);
        RefreshVisuals();
        RuntimeSpecs.damageDealtToPlayer += RuntimeSpecs.currentPlayerHealth - realDamage >= 0 ? realDamage : RuntimeSpecs.currentPlayerHealth;
        RuntimeSpecs.currentPlayerHealth -= realDamage;
        //Debug.Log("Current player health: " + RuntimeSpecs.currentPlayerHealth);
        if (RuntimeSpecs.currentPlayerHealth <= 0 && !markedForDestruction) {
            if (!RuntimeSpecs.toBeRevived) {
                ParticleSystem particles = Instantiate(rf.playerDeathExplosionParticles, transform.position, Quaternion.identity);
                particles.transform.parent = rf.spawnedParticles.transform;
                markedForDestruction = true;
                Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_PLAYER_DEATH);
                RuntimeSpecs.timesDead++;
                Destroy(gameObject);
                rf.gm.Lose();
            } else {
                Debug.Log("Powerup worked");
                RuntimeSpecs.currentPlayerHealth = GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH) * rf.gameManager.GetComponent<PowerUpManager>().generalPowerupMultiplier;
                RuntimeSpecs.toBeRevived = false;
            }

        } else if (ranged) Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_PLAYER_HIT);
    }

    public void RefillStats() {
        RuntimeSpecs.currentPlayerHealth = GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        RefreshVisuals();
    }


    public void InstantHeal(float percentageMaxHeal, bool instant) {
        float maxHP = instant ? 0.8f : 1f;
        maxHP *= GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        RuntimeSpecs.currentPlayerHealth = Mathf.Clamp(RuntimeSpecs.currentPlayerHealth + percentageMaxHeal * maxHP, 0, maxHP);
        RuntimeSpecs.hpHealed += maxHP * GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        RefreshVisuals();
    }

    public void UpdatePowerupMultiplier(Utility.Gameplay.Player.playerStatTypes statType, float multiplier) {
        foreach (StatItem stat in statItems)
            if (stat.statType == statType) {
                stat.powerupFactor = multiplier;
            }
    }
}
