using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Stats
    public float shootingTime;

    //Runtime Variables
    private List<Transform> firepoints;
    public GameObject activeBullet;
    private GameObject target;
    private float currentTimer;
    public float range;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        firepoints = new List<Transform>();
    }

    void Start() {
        target = rf.player;
        Transform headSystem = gameObject.transform.GetChild(0);
        for (int i = 0; i < headSystem.childCount; i++) {
            if (headSystem.GetChild(i).gameObject.activeInHierarchy && headSystem.GetChild(i).childCount == 1) {
                Transform fp = headSystem.GetChild(i).GetChild(0);
                if (fp)
                    firepoints.Add(fp);
            }
        }
    }

    void Update() {
        currentTimer += Time.deltaTime;
        if (currentTimer >= shootingTime)
            Shoot();
    }

    public void Shoot() {
        if (!(Vector2.Distance(target.transform.position, transform.position) <= range))
            return;
        foreach (Transform firepoint in firepoints) {
            GameObject bullet = Instantiate(activeBullet, firepoint.position, firepoint.rotation);
            bullet.transform.parent = rf.spawnedProjectiles.transform;
            bullet.GetComponent<Bullet>().playerFired = false;
        }
        currentTimer = 0;
    }
}
