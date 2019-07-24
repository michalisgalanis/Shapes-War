using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage=40;

    void Start(){
        rb.velocity = transform.up * speed;
        Physics2D.IgnoreLayerCollision(8, 10);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
