using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitExplosionParticlesPrefab;
    private ParticleSystem hitExplosionParticles;

    public float maxSpeed = 20f;
    public float acceleration;
    private float currentSpeed;
    public Rigidbody2D rb;
    public int damage;
    public bool playerFired;

    void Start(){
        Physics2D.IgnoreLayerCollision(10,8); //Ignore Collisions with bg particles
        currentSpeed = 0;
    }

    void Update()
    {
        currentSpeed += acceleration;
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
        rb.velocity = transform.up * currentSpeed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
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
                Shield shield = hitInfo.GetComponent<Shield>();
                if (shield != null)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), hitInfo);
                }
            }  
        }
        else
        {
            if (hitInfo.CompareTag("Shield")){
                Shield shield = hitInfo.GetComponent<Shield>();
                if (shield != null)
                {
                    hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                    shield.TakeDamage(damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag("Player")) {
                PlayerStats player = hitInfo.GetComponent<PlayerStats>();
                if (player != null)
                {
                    hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                    player.TakeDamage(damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag("Enemy"))
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), hitInfo);
        }
        if (hitInfo.CompareTag("MapBounds"))
            Destroy(gameObject);
    }
}
