using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Stats
    [HideInInspector] public float shootingTime;

    //Runtime Variables
    private List<Transform> firepoints;
    private GameObject activeBullet;
    private float currentTimer;


    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        firepoints = new List<Transform>();
    }

    public void Start() {
        SetupFirepoints();
    }

    public void Update() {
        activeBullet = rf.asy.activeAmmoItem.bullet.gameObject;
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime) {
            Shoot();
        }
    }

    public void SetupFirepoints() {
        firepoints.Clear();
        foreach (GameObject head in rf.playerHeads) {
            if (head.activeInHierarchy)
                firepoints.Add(head.transform.GetChild(0));
        }
    }

    private void Shoot() {
        rf.audioManagerComp.Play("ShootingSound");
        foreach (Transform firepoint in firepoints) {
            rf.asy.ConsumeAmmo();
            GameObject bullet = Instantiate(activeBullet, firepoint.position, firepoint.rotation);
            bullet.transform.parent = rf.spawnedProjectiles.transform;
            bullet.GetComponent<Bullet>().playerFired = true;
        }
        currentTimer = 0;
    }
}
