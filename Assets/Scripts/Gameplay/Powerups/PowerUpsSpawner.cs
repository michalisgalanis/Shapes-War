using UnityEngine;
using System.Collections;

public class PowerUpsSpawner : MonoBehaviour {

    //References
    private Referencer rf;

    //Constants
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    void Start() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        MIN_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
    }

    public void ReEvaluatePowerups() {
        StopCoroutine(SpawnPowerups());
        StartCoroutine(SpawnPowerups());
    }

    private IEnumerator SpawnPowerups() {
        if (rf == null) Start();
        int spawnTimerStoreCounter = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POWERUP_SPAWN_FREQUENCY).counter;
        float spawnTimer = Utility.Functions.PowerupStats.getPowerupSpawnTimer(spawnTimerStoreCounter);
        while (true) {
            UnlockSystem.CheckPowerupsUnlockProgress();
            SpawnPowerup(-1);
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    public void SpawnPowerup(int powerupIndex) {
        Vector3 temp = powerupIndex < 0 ? new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0) : new Vector3(rf.player.transform.position.x + Random.Range(-2f, 2f), rf.player.transform.position.x + Random.Range(-2f, 2f), 0);
        Instantiate(powerupIndex < 0 ? rf.activePowerupTypes[Random.Range(0, rf.activePowerupTypes.Count)] : rf.powerupTypes[powerupIndex], temp, Quaternion.identity).transform.parent = transform;
    }
}

