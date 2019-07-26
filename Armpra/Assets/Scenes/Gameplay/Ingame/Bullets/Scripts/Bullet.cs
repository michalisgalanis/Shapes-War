using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitExplosionParticlesPrefab;
    private ParticleSystem hitExplosionParticles;

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage;
    public bool playerFired;

    void Start(){
        rb.velocity = transform.up * speed;
        Physics2D.IgnoreLayerCollision(10,8); //Ignore Collisions with bg particles
        if (playerFired)
        {
            Physics2D.IgnoreLayerCollision(10, 9);  //Ignore Player Bullet Colliding with Shield
        }
        else
        {
            Physics2D.IgnoreLayerCollision(10, 12); //Ignore Enemy Bullet Colliding with other Enemies
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log(gameObject.name + " SAYS i've been fired by " + ((playerFired) ? "a PLAYER" : "an ENEMY") + " and i hit " + hitInfo.name);
        if (playerFired && hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                enemy.TakeDamage(damage);
            }
        }
        if (!playerFired && hitInfo.CompareTag("Shield"))
        {
            ShieldPowerUp shield = hitInfo.GetComponent<ShieldPowerUp>();
            if (shield != null)
            {
                hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                shield.TakeDamage(damage);
            }
        }
        if (!playerFired && hitInfo.CompareTag("Player"))
        {
            PlayerStats player = hitInfo.GetComponent<PlayerStats>();
            if (player != null)
            {
                hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                player.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
