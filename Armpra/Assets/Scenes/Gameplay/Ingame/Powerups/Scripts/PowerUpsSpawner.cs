using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    private float spawnTimer;
    private float currentTimer;
    private int spawnTimerStoreCounter;

    //Constants
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }

    public void Start() {
        MIN_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * 0.5f;
        spawnTimerStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_SPAWN_FREQUENCY).counter;
        spawnTimer = Constants.Functions.getPowerupSpawnTimer(spawnTimerStoreCounter);
        currentTimer = spawnTimer;
    }

    public void Update() {
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer) {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp() {
        //Generating Powerup
        Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
        //GameObject newEnemy = Instantiate(rf.powerupTypes[Random.Range(0, rf.powerupTypes.Length)], temp, Quaternion.identity);
        GameObject newEnemy = Instantiate(rf.powerupTypes[2], temp, Quaternion.identity);
        newEnemy.transform.parent = transform;
        //Resetting timer
        currentTimer = 0f;
    }
}
