using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float shootingTime;
    private float currentTimer;
    private GameObject bulletPrefab;        //input
    private List<Transform> firepoints;
    private bool playerFires;
    public float range;
    private GameObject target;
    private AmmoSystem asys;    //input

    public void Start() {
        playerFires = gameObject.CompareTag("Player");
        firepoints = new List<Transform>();
        if (!playerFires) target = GameObject.FindGameObjectWithTag("Player");
        asys = GameObject.FindGameObjectWithTag("GameController").GetComponent<AmmoSystem>();
        SetupFirepoints();
    }

    public void Update() {
        bulletPrefab = asys.currentActiveBullet.gameObject;
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime) Shoot();
    }

    public void SetupFirepoints() {
        firepoints.Clear();
        if (playerFires || Vector2.Distance(target.GetComponent<Transform>().position, transform.position) <= range) {
            Transform headSystem = gameObject.transform.GetChild(0);
            for (int i = 0; i < headSystem.childCount; i++) {
                if (headSystem.GetChild(i).gameObject.activeInHierarchy) {
                    Transform fp = headSystem.GetChild(i).GetChild(0);
                    if (fp) firepoints.Add(fp);
                }
            }
        }
    }

    private void Shoot() {
        foreach (Transform firepoint in firepoints) {
            if (asys.ConsumeAmmo()) {
                GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                bullet.GetComponent<Bullet>().playerFired = playerFires;
                currentTimer = 0;
            } else Update();
        }
    }
}
