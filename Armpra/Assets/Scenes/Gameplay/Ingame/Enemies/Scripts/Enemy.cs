using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Stats
    public float maxHealth;
    public float meleeDamage;
    public int xpOnDeath;
    public int coinsOnDeath;

    //Essential Assets
    public SpriteRenderer enemyBody;
    public SpriteRenderer enemyBorder;
    public SpriteRenderer[] enemyHeads;
    public ParticleSystem deathExplosionParticlesPrefab;
    private ParticleSystem deathExplosionParticles;
    public ParticleSystem trails;
    public ParticleSystem frictionParticles;
    public ParticleSystem friction;

    //Internal variables
    private float currentTimer;
    private float currentHealth;
    private bool isEmittingFriction;
    private bool markedForDestruction;

    void Start(){
        currentTimer = 0f;
        ParticleSystem.MainModule settings = trails.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(enemyBody.color);
        currentHealth = maxHealth;
        markedForDestruction = false;
        isEmittingFriction = false;
        Physics2D.IgnoreLayerCollision(8, 12);
    }
    void Update(){
        currentTimer += Time.deltaTime;
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
            manager.GetComponent<EnemySpawner>().enemyCounter--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>().addXP(xpOnDeath);
            manager.GetComponent<CoinSystem>().addCoins(coinsOnDeath);
        }
    }

    void OnTriggerStay2D(Collider2D hitInfo){
        if (hitInfo.CompareTag("Player")){
            PlayerStats player = hitInfo.GetComponent<PlayerStats>();
            if (!player)
            {
                player.TakeDamage(meleeDamage);
               /* if (!isEmittingFriction)
                {
                    Debug.Log("SPAWNED FRICTIOn");
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    friction = Instantiate(frictionParticles, pos, Quaternion.identity);
                    //isEmittingFriction = true;
                }*/
            }  
        }
        else if (hitInfo.CompareTag("Shield")){
            Shield shield = hitInfo.GetComponent<Shield>();
            if (shield != null)
                shield.TakeDamage(meleeDamage);
        } /*else if (!friction)
        {
            if (currentTimer >= 2f)
                Destroy(friction);
            isEmittingFriction = false;
        }*/
    }

}
