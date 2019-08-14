using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float shootingTime;
    private float currentTimer;
    public GameObject bulletPrefab;
    private List<Transform> firepoints;
    private bool playerFires;
    public int bulletDamage;
    public float bulletSpeed;
    public float range;
    private GameObject target;

    private void Start() {
        playerFires = gameObject.CompareTag("Player");
        firepoints = new List<Transform>();
        if (!playerFires) {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        SetupFirepoints();
    }

    private void Update() {
        currentTimer += Time.deltaTime;
        SetupFirepoints();
        if (currentTimer >= shootingTime) {
            Shoot();
        }
    }

    private void SetupFirepoints() {
        firepoints.Clear();
        if (playerFires || Vector2.Distance(target.GetComponent<Transform>().position, transform.position) <= range) {
            Transform headSystem = gameObject.transform.GetChild(0);
            for (int i = 0; i < headSystem.childCount; i++) {
                if (headSystem.GetChild(i).gameObject.activeInHierarchy) {
                    Transform fp = headSystem.GetChild(i).GetChild(0);
                    if (fp) {
                        firepoints.Add(fp);
                    }
                }
            }
        }
    }

    private void Shoot() {
        foreach (Transform firepoint in firepoints) {
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<Bullet>().playerFired = playerFires;
            bullet.GetComponent<Bullet>().damage = bulletDamage;
            bullet.GetComponent<Bullet>().maxSpeed = bulletSpeed;
            currentTimer = 0;
        }
    }
}
