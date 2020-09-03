using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Stats
    [HideInInspector] public float shootingTime;
    [HideInInspector] public float damageFactor;

    //Runtime Variables
    [HideInInspector] public List<Transform> firepoints;
    [HideInInspector] public Bullet activeBullet;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        firepoints = new List<Transform>();
    }

    public IEnumerator Shoot() {
        yield return new WaitForSeconds(1f);
        while (true) {
            rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.SFX, 0);
            foreach (Transform firepoint in firepoints) {
                rf.asy.ConsumeAmmo();
                RuntimeSpecs.bulletsFired++;
                Bullet bullet = Instantiate(activeBullet, firepoint.position, firepoint.rotation);
                bullet.transform.parent = rf.spawnedProjectiles.transform;
                bullet.playerFired = true;
                bullet.externalDamageFactor = damageFactor;
            }
            yield return new WaitForSeconds(shootingTime);
        }
    }

    public void ResetShooting() {
        activeBullet = rf.asy.activeAmmoItem.bullet;
        firepoints.Clear();
        foreach (GameObject head in rf.playerHeads) {
            if (head.activeInHierarchy)
                firepoints.Add(head.transform.GetChild(0));
        }
    }
}
