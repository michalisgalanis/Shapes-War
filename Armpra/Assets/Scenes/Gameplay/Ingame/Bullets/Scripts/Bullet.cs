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
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log(gameObject.name + ": " + ((playerFired) ? "a PLAYER" : "an ENEMY") + " -> " + hitInfo.name);
        if (playerFired)
        {
            if (hitInfo.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.GetComponent<Enemy>();
                if (enemy != null)
                {
                    hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                    enemy.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
            else if (hitInfo.CompareTag("Shield"))
            {
                ShieldPowerUp shield = hitInfo.GetComponent<ShieldPowerUp>();
                if (shield != null)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), hitInfo);
                }
            }  
        }
        else
        {
            if (hitInfo.CompareTag("Shield")){
                ShieldPowerUp shield = hitInfo.GetComponent<ShieldPowerUp>();
                if (shield != null)
                {
                    hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                    shield.TakeDamage(damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag("Player"))
            {
                PlayerStats player = hitInfo.GetComponent<PlayerStats>();
                if (player != null)
                {
                    hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                    player.TakeDamage(damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag("Enemy"))
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), hitInfo);
        }
        if (hitInfo.CompareTag("MapBounds"))
            Destroy(gameObject);
    }
}
