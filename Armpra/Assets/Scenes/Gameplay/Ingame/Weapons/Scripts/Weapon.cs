using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Stats
    [HideInInspector] public float shootingTime;
    private bool playerFires;

    //Runtime Variables
    private List<Transform> firepoints;
    private GameObject activeBullet;
    private GameObject target;
    private float currentTimer;
    public float range;


    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        firepoints = new List<Transform>();
    }

    public void Start() {
        playerFires = gameObject.CompareTag(Constants.Tags.PLAYER_TAG);
        if (!playerFires) {
            target = rf.player;
        }
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
        if (playerFires) {
            foreach (GameObject head in rf.playerHeads) {
                if (head.activeSelf)
                    firepoints.Add(head.transform.GetChild(0));
            }
        } else if (Vector2.Distance(target.GetComponent<Transform>().position, transform.position) <= range) {
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
        if (playerFires)
            rf.gameManager.GetComponent<AudioManager>().Play("ShootingSound");
        foreach (Transform firepoint in firepoints) {
            rf.asy.ConsumeAmmo();
            GameObject bullet = Instantiate(activeBullet, firepoint.position, firepoint.rotation);
            bullet.transform.parent = rf.spawnedProjectiles.transform;
            bullet.GetComponent<Bullet>().playerFired = playerFires;
            currentTimer = 0;
        }
    }
}
