﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //References
    private Referencer rf;
    private SpriteRenderer playerBorder;
    private List<SpriteRenderer> playerHeads;

    //Runtime Variables
    private bool markedForDestruction = false;
    private List<StatItem> statItems;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        playerBorder = rf.player.transform.GetChild(1).GetComponent<SpriteRenderer>();
        statItems = new List<StatItem>();
        playerHeads = new List<SpriteRenderer>();
    }

    public void Start() {
        for (int i = 0; i < Enum.GetValues(typeof(Constants.Gameplay.Player.playerStatTypes)).Length; i++) {
            statItems.Add(new StatItem(RuntimeSpecs.playerLevel, (Constants.Gameplay.Player.playerStatTypes)Enum.GetValues(typeof(Constants.Gameplay.Player.playerStatTypes)).GetValue(i)));
        }
        for (int i = 0; i < rf.player.transform.GetChild(0).childCount; i++) {
            playerHeads.Add(rf.player.transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>());
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
        Color.RGBToHSV(playerBorder.color, out float h, out float s, out float v);
        h = 0f; v = 1f; s = 1f - RuntimeSpecs.currentPlayerHealth / GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        playerBorder.color = (Color.HSVToRGB(h, s, v));
        foreach (SpriteRenderer playerHead in playerHeads) {
            playerHead.color = (Color.HSVToRGB(h, s, v));
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
        float realDamage = damage * (1 - GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.DAMAGE_REDUCTION));
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
        foreach (GameObject powerup in rf.powerupTypes) {
            if (powerup.GetComponent<EffectOverTime>() != null && powerup.GetComponent<EffectOverTime>().typeSelected == powerupType) {
                return powerup.GetComponent<EffectOverTime>().powerupMultiplier;
            }
        }
        return 0f;
    }
}
