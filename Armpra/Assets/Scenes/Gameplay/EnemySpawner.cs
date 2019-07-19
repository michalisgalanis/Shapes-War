using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float difficulty;
    public GameObject[] enemy_level1;

    public float spawn_timer = 0.3f;
    private float current_timer;
    //public float min_X = -19, max_X = 19, min_Y = -19, max_Y = 19;
    private int enemyCounter;
    private float maxEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        current_timer = spawn_timer;
        enemyCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EstimateWave();
        SpawnEnemies();
    }

    void EstimateWave()
    {
        maxEnemyCount = difficulty * 100;
    }

    void SpawnEnemies()
    {
        current_timer += Time.deltaTime;

        if (current_timer >= spawn_timer && enemyCounter < maxEnemyCount)
        {
            Vector3 temp = new Vector3(0, 0, 0);
            temp.x = (Random.Range(1, 3) == 1) ? Random.Range(-19, -4) : Random.Range(4, 19);
            temp.y = (Random.Range(1, 3) == 1) ? Random.Range(-19, -6) : Random.Range(6, 19);
            GameObject newEnemy = null;
            newEnemy = Instantiate(enemy_level1[Random.Range(0, enemy_level1.Length)], temp, Quaternion.identity);
            enemyCounter++;
            newEnemy.transform.parent = transform;
            current_timer = 0f;
        }
    }
}
