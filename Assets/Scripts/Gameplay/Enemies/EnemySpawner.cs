using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Constant Variables
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    public List<GameObject> preWarmedEnemies;

    void Awake() {
        preWarmedEnemies = new List<GameObject>();
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        for (int i = 0; i < rf.enemyTypes.Length; i++)
            new GameObject("Level " + (i + 1)).transform.parent = rf.spawnedEnemies.transform;
    }

    void Start() {
        MIN_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
    }

    public void PreWarmEnemies(int count, bool bossLevel) {
        for (int i = 0; i < count; i++) {
            GameObject newEnemy = Instantiate(bossLevel ? rf.bossTypes[RuntimeSpecs.newestBoss - 1] : rf.enemyTypes[LevelGeneration.PickRandomEnemy()]);
            newEnemy.transform.parent = bossLevel ? rf.spawnedEnemies.transform : rf.spawnedEnemies.transform.GetChild(newEnemy.GetComponent<Enemy>().enemyLevel - 1);
            if (!bossLevel) {
                float h = Random.Range(0f, 1f), s = Random.Range(0.7f, 0.9f), v = Random.Range(0.6f, 0.8f), a = Random.Range(0.5f, 0.7f);
                for (int j = 0; j < newEnemy.transform.GetChild(2).childCount; j++)
                    newEnemy.transform.GetChild(2).GetChild(j).GetComponent<SpriteRenderer>().color = Utility.Functions.GeneralFunctions.generateColor(h, s, v, a);
            }
            newEnemy.SetActive(false);
            preWarmedEnemies.Add(newEnemy);
        }
    }

    public IEnumerator SpawnEnemies() {
        RuntimeSpecs.enemiesSpawned = 0;
        RuntimeSpecs.enemiesKilled = 0;
        RuntimeSpecs.ResetTempStats();
        LevelGeneration.EstimateLevel();
        bool bossLevel = LevelGeneration.bossLevel;
        if (bossLevel) rf.audioManagerComp.PlayBossMusic();
        rf.bossProgressBar.SetActive(bossLevel);
        rf.backgroundScript.ChangeScenery(bossLevel);
        PreWarmEnemies(RuntimeSpecs.maxEnemyCount, bossLevel);
        while (RuntimeSpecs.enemiesSpawned < RuntimeSpecs.maxEnemyCount) {
            if (RuntimeSpecs.enemiesSpawned - RuntimeSpecs.enemiesKilled < Utility.Gameplay.Enemy.MAX_ENEMIES_SPAWNED) {
                preWarmedEnemies[RuntimeSpecs.enemiesSpawned].SetActive(true);
                RuntimeSpecs.enemiesSpawned++;
                rf.enemiesRemainingText.text = (RuntimeSpecs.enemiesSpawned - RuntimeSpecs.enemiesKilled).ToString();
                //AnimationManager.ResetAnimation(rf.enemiesRemainingText.gameObject, true);
            }
            yield return new WaitForSeconds(Utility.Gameplay.Enemy.ENEMY_SPAWN_TIMER);
        }
        preWarmedEnemies.Clear();
        //Debug.Log("Max:" + RuntimeSpecs.maxEnemyCount + ", Spawned: " + enemiesSpawned + ", Killed: " + RuntimeSpecs.enemiesKilled + ", Bap: " + RuntimeSpecs.bap);
    }
}
