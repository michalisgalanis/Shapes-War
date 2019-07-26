using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitExplosionParticlesPrefab;
    private ParticleSystem hitExplosionParticles;

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public bool playerFired;

    void Start(){
        rb.velocity = transform.up * speed;
        Physics2D.IgnoreLayerCollision(10,8); //Ignore Collisions with bg particles
        if (playerFired)
        {
            Physics2D.IgnoreLayerCollision(10, 13); //Ignore Player Bullet Colliding with Player
            Physics2D.IgnoreLayerCollision(10, 9);  //Ignore Player Bullet Colliding with Shield
        }
        else
        {
            Physics2D.IgnoreLayerCollision(10, 12); //Ignore Enemy Bullet Colliding with other Enemies
            /*Physics2D.IgnoreLayerCollision(10, 13, false); //Ignore Enemy Bullet Colliding with other Enemies
            Physics2D.IgnoreLayerCollision(10, 9, false); //Ignore Enemy Bullet Colliding with other Enemies*/
        }
        Debug.Log(playerFired);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        if (playerFired)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
                enemy.TakeDamage(damage);
            }
        }
        else
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
