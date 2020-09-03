using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Stats
    [HideInInspector] public float shootingTime;
    [HideInInspector] public float damageFactor;

    //Runtime Variables
    private List<Transform> firepoints;
    public GameObject activeBullet;
    private GameObject target;
    [HideInInspector] public float range;

    void Awake() { 
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        firepoints = new List<Transform>();
    }

    void Start() {
        target = rf.player;
        Transform headSystem = gameObject.transform.GetChild(0);
        for (int i = 0; i < headSystem.childCount; i++) {
            if (headSystem.GetChild(i).gameObject.activeInHierarchy && headSystem.GetChild(i).childCount == 1) {
                Transform fp = headSystem.GetChild(i).GetChild(0);
                if (fp) firepoints.Add(fp);
            }
        }
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot() {
        while (true) {
            if (target != null && Vector2.Distance(target.transform.position, transform.position) <= range) {
                foreach (Transform firepoint in firepoints) {
                    GameObject bullet = Instantiate(activeBullet, firepoint.position, firepoint.rotation);
                    bullet.transform.parent = rf.spawnedProjectiles.transform;
                    bullet.GetComponent<Bullet>().playerFired = false;
                    bullet.GetComponent<Bullet>().externalDamageFactor = damageFactor;
                }
            }
            yield return new WaitForSeconds(shootingTime);
        }
    }
}
