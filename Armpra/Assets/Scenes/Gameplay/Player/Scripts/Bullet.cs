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

    void Start(){
        rb.velocity = transform.up * speed;
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    void Update(){
        if (hitExplosionParticles && !hitExplosionParticles.IsAlive())
            Destroy(hitExplosionParticles);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null) {
            hitExplosionParticles = Instantiate(hitExplosionParticlesPrefab, transform.position, Quaternion.identity);
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
