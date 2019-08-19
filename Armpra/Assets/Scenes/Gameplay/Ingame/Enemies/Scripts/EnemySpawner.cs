﻿using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    //access to other objects and components
    public GameObject[] enemies;
    public GameObject enemiesRemainingText;
    public Camera camera;
    public LevelGeneration lg;

    //public properties
    //public float difficulty;
    public float spawnTimer;
    private bool spawningTime;

    //constants
    private const float MIN_BORDER = -19;
    private const float MAX_BORDER = 19;
    private const float HORIZONTAL_CAMERA_OFFSET = 4;
    private const float VERTICAL_CAMERA_OFFSET = 6;

    //other essential variables
    public int enemyCounter;
    public float maxEnemyCount;
    private float currentTimer;
    private bool positionConflict;


    public void BeginSpawning() {
        spawningTime = true;
        positionConflict = false;
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
            positionConflict = false;
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
