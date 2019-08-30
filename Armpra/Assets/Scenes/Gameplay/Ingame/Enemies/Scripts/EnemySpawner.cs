using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Constant Variables
    private readonly float spawnTimer = Constants.Timers.ENEMY_SPAWN_TIMER;

    //Runtime Variables
    private float currentTimer;
    private bool spawningTime;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }

    public void Update() {
        if (spawningTime == true && RuntimeSpecs.enemiesSpawned < RuntimeSpecs.maxEnemyCount) {
            SpawnEnemies();
        } else if (spawningTime == false && RuntimeSpecs.enemiesKilled == RuntimeSpecs.maxEnemyCount) {
            spawningTime = true;
            rf.gm.CompleteLevel();
        }
        //Outputting Texts
        rf.enemiesRemainingText.text = (RuntimeSpecs.maxEnemyCount - RuntimeSpecs.enemiesKilled).ToString();
        RuntimeSpecs.ap = (float)Mathf.Round(((float)RuntimeSpecs.enemiesKilled / RuntimeSpecs.maxEnemyCount) * 100f);
        rf.bapText.text = RuntimeSpecs.ap.ToString();
        //Debug.Log("Max:" + RuntimeSpecs.maxEnemyCount + ", Spawned: " + enemiesSpawned + ", Killed: " + RuntimeSpecs.enemiesKilled + ", Bap: " + RuntimeSpecs.bap);
    }

    public void BeginSpawning() {
        spawningTime = true;
        currentTimer = spawnTimer;
        RuntimeSpecs.enemiesSpawned = 0;
        RuntimeSpecs.enemiesKilled = 0;
        rf.lg.EstimateLevel();
    }

    private void SpawnEnemies() {
        float MIN_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        float MAX_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * 0.5f;
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && RuntimeSpecs.enemiesSpawned < RuntimeSpecs.maxEnemyCount) {
            //Generate Enemy
            Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
            GameObject newEnemy = Instantiate(rf.enemyTypes[rf.lg.PickRandomEnemy()], temp, Quaternion.identity) as GameObject;
            newEnemy.transform.parent = rf.spawnedEnemies.transform;
            //Customizing Color
            SpriteRenderer sr = newEnemy.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = Random.Range(0.6f, 0.8f), v = Random.Range(0.5f, 0.7f);
            sr.color = Color.HSVToRGB(h, s, v);
            RuntimeSpecs.enemiesSpawned++;
            //Resetting Timers & Counters
            if (RuntimeSpecs.enemiesSpawned == RuntimeSpecs.maxEnemyCount) {
                spawningTime = false;
            }

            currentTimer = 0f;
        }
    }
}
