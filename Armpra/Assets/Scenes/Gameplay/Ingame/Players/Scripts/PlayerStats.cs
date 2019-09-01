using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    private bool markedForDestruction = false;
    private List<StatItem> statItems;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        statItems = new List<StatItem>();
    }

    public void Start() {
        for (int i = 0; i < Enum.GetValues(typeof(Constants.Gameplay.Player.playerStatTypes)).Length; i++) {
            statItems.Add(new StatItem((Constants.Gameplay.Player.playerStatTypes)Enum.GetValues(typeof(Constants.Gameplay.Player.playerStatTypes)).GetValue(i)));
        }
        GameObject shockwave = Instantiate(rf.shockwave, transform.localPosition, Quaternion.identity) as GameObject;
        shockwave.transform.parent = rf.spawnedParticles.transform;
        EstimateStats();
        RefillStats();
    }

    public void Update() {
        EstimateStats();
    }

    public void FixedUpdate() {
        float h = 0f, v = 1f, s = 1f - RuntimeSpecs.currentPlayerHealth / GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        foreach (GameObject playerBorder in rf.playerWings) {
            playerBorder.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color.HSVToRGB(h, s, v));
        }
        foreach (GameObject playerHead in rf.playerHeads) {
            playerHead.GetComponent<SpriteRenderer>().color = (Color.HSVToRGB(h, s, v));
        }
    }

    public void EstimateStats() {
        foreach (StatItem stat in statItems) {
            stat.EstimateStat();
        }
    }

    public float GetStatValueOf(Constants.Gameplay.Player.playerStatTypes statType) {
        foreach (StatItem stat in statItems) {
            if (stat.statType == statType) {
                stat.EstimateStat();
                return stat.statValue;
            }
        }
        return 0f;
    }

    public void EstimateStat(Constants.Gameplay.Player.playerStatTypes statType) {
        foreach (StatItem stat in statItems) {
            if (stat.statType == statType) {
                stat.EstimateStat();
            }
        }
    }

    public void TakeDamage(float damage) {
        float realDamage = Mathf.Clamp(damage * (1 - GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION)), 0f, damage);
        RuntimeSpecs.currentPlayerHealth -= realDamage;
        if (RuntimeSpecs.currentPlayerHealth <= 0 && !markedForDestruction) {
            markedForDestruction = true;
            ParticleSystem particles = Instantiate(rf.playerDeathExplosionParticles, transform.position, Quaternion.identity);
            particles.transform.parent = rf.spawnedParticles.transform;
            Destroy(gameObject);
            rf.gm.Lose();
        }
    }

    public void RefillStats() {
        RuntimeSpecs.currentPlayerHealth = GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH);
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("Enemy")) {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MELEE_DAMAGE));
            }
        }
    }

    public void InstantHeal(float percentageMaxHeal) {
        RuntimeSpecs.currentPlayerHealth = Mathf.Clamp(RuntimeSpecs.currentPlayerHealth + percentageMaxHeal * GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH), 0, GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH));
    }

    public float GetPowerupMultiplier(Constants.Gameplay.Powerups.overTimePowerupTypes powerupType) {
        foreach (GameObject tempPowerup in GameObject.FindGameObjectsWithTag(Constants.Tags.POWERUPS_TAG)) {
            if  (tempPowerup.activeInHierarchy && tempPowerup.GetComponent<EffectOverTime>() != null  && tempPowerup.GetComponent<EffectOverTime>().typeSelected == powerupType && tempPowerup.GetComponent<EffectOverTime>().used) {
                return tempPowerup.GetComponent<EffectOverTime>().powerupMultiplier;
            }
        }
        return 0f;
    }
}
