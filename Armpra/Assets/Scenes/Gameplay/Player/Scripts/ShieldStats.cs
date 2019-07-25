using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStats : MonoBehaviour
{
    public float maxShieldHealth;
    public float shieldDamageReduction; //0 equals to full damage taken, 1 equals to zero damage taken
    public int shieldDamage;

    public ParticleSystem shieldDestroyExplosionParticlesPrefab;
    private ParticleSystem shieldDestroyExplosionParticles;
    public ParticleSystem trails;

    private float currentHealth;

    public SpriteRenderer outerShield;
    public SpriteRenderer innerShield;

    void Start(){
        currentHealth = maxShieldHealth;
        Physics2D.IgnoreLayerCollision(8, 9);
    }


    void Update(){
        Debug.Log("Shield: " + currentHealth);
        if (shieldDestroyExplosionParticles && !shieldDestroyExplosionParticles.IsAlive())
            Destroy(shieldDestroyExplosionParticles);
        float h = (currentHealth / maxShieldHealth) / 3.6f;
        float outerS = 1f, outerV = 1f, outerA = 0.7f;
        float innerS = 1f, innerV = 0.7f, innerA = 0.2f;
        float outerR, outerG, outerB;
        outerR = Color.HSVToRGB(h, outerS, outerV).r;
        outerG = Color.HSVToRGB(h, outerS, outerV).g;
        outerB = Color.HSVToRGB(h, outerS, outerV).b;
        outerShield.color = new Color(outerR, outerG, outerB, outerA);
        float innerR, innerG, innerB;
        innerR = Color.HSVToRGB(h, innerS, innerV).r;
        innerG = Color.HSVToRGB(h, innerS, innerV).g;
        innerB = Color.HSVToRGB(h, innerS, innerV).b;
        innerShield.color = new Color(innerR, innerG, innerB, innerA);

        ParticleSystem.MainModule ps = trails.main;
        ps.startColor = new ParticleSystem.MinMaxGradient(outerShield.color);
    }

    public void TakeDamage(float damage){
        currentHealth -= (damage - damage * shieldDamageReduction);
        if (currentHealth <= 0){
            shieldDestroyExplosionParticles = Instantiate(shieldDestroyExplosionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(shieldDamage);
        }
    }
}
