﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem deathExplosionParticlesPrefab;
    private ParticleSystem deathExplosionParticles;

    public SpriteRenderer enemyBody;
    public SpriteRenderer enemyBorder;
    public SpriteRenderer[] enemyHeads;
    public ParticleSystem trails;

    public float maxHealth;
    public float meleeDamage;
    private float currentHealth;
    private bool markedForDestruction;
    public int points;
    public int coins;

    void Start(){
        ParticleSystem.MainModule settings = trails.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(enemyBody.color);
        currentHealth = maxHealth;
        markedForDestruction = false;
        Physics2D.IgnoreLayerCollision(8, 12);
    }
    void Update(){
        if (deathExplosionParticles && !deathExplosionParticles.IsAlive())
                Destroy(deathExplosionParticles);
        float h, s, v;
        Color.RGBToHSV(enemyBorder.color, out h, out s, out v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        enemyBorder.color = (Color.HSVToRGB(h, s, v));
        foreach  (SpriteRenderer enemyHead in enemyHeads){
            enemyHead.color = (Color.HSVToRGB(h, s, v));
        }
    }
    public void TakeDamage(float damage){
        currentHealth -= damage;
        if (currentHealth <= 0 && !markedForDestruction){
            markedForDestruction = true;
            deathExplosionParticles = Instantiate(deathExplosionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject manager = GameObject.FindWithTag("GameController");
            manager.gameObject.GetComponent<ScoreSystem>().addPoints(points);
            manager.gameObject.GetComponent<CoinSystem>().addCoins(coins);
        }
    }

    void OnTriggerStay2D(Collider2D hitInfo){
        if (hitInfo.CompareTag("Player")){
            PlayerStats player = hitInfo.GetComponent<PlayerStats>();
            if (player != null)
                    player.TakeDamage(meleeDamage);
        }else if (hitInfo.CompareTag("Shield")){
            ShieldPowerUp shield = hitInfo.GetComponent<ShieldPowerUp>();
            if (shield != null)
                shield.TakeDamage(meleeDamage);
        }
    }

}
