using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootingTime;
    private float currentTimer;
    public GameObject bulletPrefab;
    public Transform[] firepoints;
    private bool playerFires;
    public int bulletDamage;
    public float bulletSpeed;
    public float range;
    private GameObject target;

    void Start()
    {
        playerFires = gameObject.CompareTag("Player");
        if (!playerFires) target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime)
            Shoot();
    }
    void Shoot(){
        if (playerFires || (!playerFires && Vector2.Distance(target.GetComponent<Transform>().position, transform.position) <= range))
        {
            foreach (Transform firepoint in firepoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                bullet.GetComponent<Bullet>().playerFired = playerFires;
                bullet.GetComponent<Bullet>().damage = bulletDamage;
                bullet.GetComponent<Bullet>().maxSpeed = bulletSpeed;
                currentTimer = 0;
            }
            
        }
    }
}
