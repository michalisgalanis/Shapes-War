using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootingTime;
    private float currentTimer;
    public GameObject bulletPrefab;
    public Transform firepoint;
    public bool playerFires;

    void Update(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime)
            Shoot();
    }
    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Bullet>().playerFired = playerFires;
        currentTimer = 0;
    }
}
