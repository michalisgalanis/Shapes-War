using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public ParticleSystem deathExplosionParticlesPrefab;
    private ParticleSystem deathExplosionParticles;
    public int health;

    public void Update()
    {
        if (deathExplosionParticles)
        {
            if (!deathExplosionParticles.IsAlive())
            {
                Destroy(deathExplosionParticles);
            }
        }
    }
    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0){
            deathExplosionParticles = Instantiate(deathExplosionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
