using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootingTime=1f;
    private float currentTimer;
    public GameObject bulletPrefab;
    public Transform firepoint;

    void Update(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime)
            Shoot();
    }
    void Shoot(){
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        currentTimer = 0;
    }
}
