using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration {
    //References
    private static readonly Referencer rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();

    //Runtime Variables
    private static List<GameObject> activeEnemyTypes = new List<GameObject>();
    private static List<float> enemyPropabilities = new List<float>();
    private static List<float> lowLevelPropArray = new List<float>();
    public static bool bossLevel = false;

    public static void EstimateLevel() {
        activeEnemyTypes.Clear();
        for (int i = 0; i < rf.enemyTypes.Length; i++) {
            if (RuntimeSpecs.mapLevel >= Utility.Functions.EnemySpawn.getNewEnemyGenerationLevel(i)) {
                activeEnemyTypes.Add(rf.enemyTypes[i]);
                RuntimeSpecs.newestEnemy = Mathf.Max(RuntimeSpecs.newestEnemy, i);
            } else if (RuntimeSpecs.mapLevel == Utility.Functions.EnemySpawn.getNewBossGenerationLevel(i)) {
                bossLevel = (i >= RuntimeSpecs.newestBoss);
                if (i <= rf.bossTypes.Length)
                    RuntimeSpecs.newestBoss = Mathf.Max(RuntimeSpecs.newestBoss, i);
                break;
            }
        }
        RuntimeSpecs.maxEnemyCount = bossLevel ? 1 : Utility.Functions.EnemySpawn.getMaxEnemyCount(RuntimeSpecs.mapLevel);
        //Debug.Log("Level " + RuntimeSpecs.mapLevel + ", Newest Enemy: " + RuntimeSpecs.newestEnemy + ", Newest Boss: " + RuntimeSpecs.newestBoss + ", BOSS LEVEL = " + ((rf.es.bossLevel) ? "YES" : "NO"));
        //Estimate Propabilities
        enemyPropabilities.Clear();
        float sumEnemyProp = 0f;
        for (int i = 0; i < activeEnemyTypes.Count; i++) {
            float tempEnemyProp;
            if (i == 0) {
                tempEnemyProp = (float)(Utility.Functions.EnemySpawn.getDecreaseCurve(Utility.Functions.EnemySpawn.getNewEnemyGenerationLevel(i)) + 0.025 * RuntimeSpecs.mapLevel);
            } else if (i == 1) {
                tempEnemyProp = Utility.Functions.EnemySpawn.getDecreaseCurve(Utility.Functions.EnemySpawn.getNewEnemyGenerationLevel(i)) + Mathf.Pow(RuntimeSpecs.mapLevel, 1.4f);
            } else {
                tempEnemyProp = Utility.Functions.EnemySpawn.getDecreaseCurve(Utility.Functions.EnemySpawn.getNewEnemyGenerationLevel(i)) + 0.05f * Mathf.Pow(2, -i - 1) * Mathf.Pow(RuntimeSpecs.mapLevel, 2f + (0.2f) * (i + 1));
            }

            enemyPropabilities.Add(tempEnemyProp);
            sumEnemyProp += tempEnemyProp;
        }
        //Normalize Array
        for (int i = 0; i < enemyPropabilities.Count; i++) {
            enemyPropabilities[i] = enemyPropabilities[i] / sumEnemyProp;
        }
        BuildPropabilityArray();
    }

    private static void BuildPropabilityArray() {
        lowLevelPropArray.Clear();
        for (int i = 0; i < enemyPropabilities.Count; i++) {
            for (int j = 0; j < 100 * enemyPropabilities[i]; j++) {
                lowLevelPropArray.Add(enemyPropabilities[i]);
            }
        }
    }

    public static int PickRandomEnemy() {
        int random = Random.Range(0, lowLevelPropArray.Count);
        float randomPropability = lowLevelPropArray[random];
        int index;
        List<int> indexes = new List<int>();
        for (int i = 0; i < enemyPropabilities.Count; i++) {
            if (enemyPropabilities[i] == randomPropability) {
                index = i;
                indexes.Add(index);
            }
        }
        index = indexes[Random.Range(0, indexes.Count)];
        return index;
    }
}
