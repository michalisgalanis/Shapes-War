using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour {
    private float spawnTimer;
    private int spawnTimerLevel;
    private float currentTimer;

    //Needed References
    private StoreSystem ss;
    public GameObject[] powerUps;

    //Other Variables
    private const float MIN_BORDER = -19;
    private const float MAX_BORDER = 19;

    public void Start() {
        ss = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoreSystem>();
        spawnTimerLevel = ss.powerupSpawnFrequencyCounter;
        spawnTimer = 30f - 0.25f * spawnTimerLevel;
        currentTimer = spawnTimer;
    }

    public void Update() {
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer) {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp() {
        //Generate Power Up
        Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
        GameObject newEnemy = Instantiate(powerUps[0/*Random.Range(0, powerUps.Length)*/], temp, Quaternion.identity);
        newEnemy.transform.parent = transform;
        //Resetting timer
        currentTimer = 0f;
    }
}
