using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    //access to other objects and components
    public GameObject[] enemies;
    public GameObject enemiesRemainingText;
    public GameObject background;
    public LevelGeneration lg;

    //public properties
    //public float difficulty;
    public float spawnTimer;
    private bool spawningTime;

    //constants
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    //other essential variables
    public int enemyCounter;
    public float maxEnemyCount;
    private float currentTimer;

    void Start() {
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        MIN_BORDER = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit) * (-0.5f);
        MAX_BORDER = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit) * 0.5f;
    }

    public void BeginSpawning() {
        spawningTime = true;
        currentTimer = spawnTimer;
        enemyCounter = 0;
        lg = gameObject.GetComponent<LevelGeneration>();
        //lg.currentLevel = 1;
        //lg.currentLevel = currentLevel;
        lg.EstimateLevel();
        maxEnemyCount = lg.enemyCount;
    }

    // Update is called once per frame
    private void Update() {
        enemiesRemainingText.GetComponent<TextMeshProUGUI>().text = enemyCounter.ToString();
        if (spawningTime == true && enemyCounter < maxEnemyCount) {
            SpawnEnemies();
        } else if (spawningTime == false && enemyCounter == 0) {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>().CompleteLevel();
            lg.currentLevel++;
            lg.EstimateLevel();
            maxEnemyCount = lg.enemyCount;
            spawningTime = true;
        }
    }

    private void SpawnEnemies() {
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && enemyCounter < maxEnemyCount) {
            //Generate Enemy
            Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER),Random.Range(MIN_BORDER, MAX_BORDER), 0);
            GameObject newEnemy = Instantiate(enemies[lg.PickRandomEnemy()], temp, Quaternion.identity);
            newEnemy.transform.parent = transform;
            //Customizing Color
            SpriteRenderer sr = newEnemy.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = 0.75f, v = 0.6f;
            sr.color = Color.HSVToRGB(h, s, v);
            //Resetting timer & counter
            if (++enemyCounter == maxEnemyCount) {
                spawningTime = false;
            }

            currentTimer = 0f;
        }
    }
}
