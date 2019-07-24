using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //access to other objects and components
    public GameObject[] enemies;
    public Camera camera;

    //public properties
    //public float difficulty;
    public float spawnTimer;
    
    //constants
    private const float MIN_BORDER = -19;
    private const float MAX_BORDER = 19;
    private const float HORIZONTAL_CAMERA_OFFSET = 4;
    private const float VERTICAL_CAMERA_OFFSET = 6;

    //other essential variables
    private int enemyCounter;
    public float maxEnemyCount;
    private float currentTimer;
    private bool positionConflict;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = spawnTimer;
        enemyCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //EstimateWave();
        SpawnEnemies();
    }

    void EstimateWave()
    {
        //maxEnemyCount = difficulty * 10;
    }

    void SpawnEnemies(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && enemyCounter < maxEnemyCount){
            //Generate Enemy
            positionConflict = false;
            Vector3 temp = new Vector3(GenerateX(), GenerateY(), 0);
            GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], temp, Quaternion.identity);
            newEnemy.transform.parent = transform;
            //Customizing Color
            SpriteRenderer sr = newEnemy.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = 0.75f, v = 0.6f;
            sr.color = Color.HSVToRGB(h, s, v);
            //Resetting timer & counter
            enemyCounter++;
            currentTimer = 0f;
        }
    }

    float GenerateX(){
        float temp_X = Random.Range(MIN_BORDER, MAX_BORDER);
        if (temp_X > camera.transform.position.x - HORIZONTAL_CAMERA_OFFSET && temp_X < camera.transform.position.x + HORIZONTAL_CAMERA_OFFSET)
            if (positionConflict)
                temp_X = GenerateX();
            else positionConflict = true;
        return temp_X;
    }

    float GenerateY(){
        float temp_Y = Random.Range(MIN_BORDER, MAX_BORDER);
        if (temp_Y > camera.transform.position.y - VERTICAL_CAMERA_OFFSET && temp_Y < camera.transform.position.y + VERTICAL_CAMERA_OFFSET)
            if (positionConflict)
                temp_Y = GenerateY();
            else positionConflict = true;
        return temp_Y;
    }
}
