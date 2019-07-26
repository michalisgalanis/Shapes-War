using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootingTime;
    private float currentTimer;
    public GameObject bulletPrefab;
    public Transform firepoint;
    private bool playerFires;
    public int bulletDamage;

    void Start()
    {
        playerFires = gameObject.CompareTag("Player");
    }

    void Update(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime)
            Shoot();
    }
    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Bullet>().playerFired = playerFires;
        bullet.GetComponent<Bullet>().damage = bulletDamage;
        currentTimer = 0;
    }
}
