using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    private List<GameObject> activeEnemyTypes;
    private List<float> enemyPropabilities;
    private List<float> lowLevelPropArray;

    public void Awake() {
        rf = GetComponent<Referencer>();
        activeEnemyTypes = new List<GameObject>();
        enemyPropabilities = new List<float>();
        lowLevelPropArray = new List<float>();
    }

    public void EstimateLevel() {
        RuntimeSpecs.maxEnemyCount = (int)Mathf.Round(4f + Mathf.Pow(RuntimeSpecs.mapLevel, 1.2f));
        activeEnemyTypes.Clear();
        for (int i = 0; i < rf.enemyTypes.Length; i++) {
            if (RuntimeSpecs.mapLevel >= 5 * i) {
                activeEnemyTypes.Add(rf.enemyTypes[i]);
            }
        }
        //Estimate Propabilities
        enemyPropabilities.Clear();
        float sumEnemyProp = 0f;
        for (int i = 0; i < activeEnemyTypes.ToArray().Length; i++) {
            float tempEnemyProp;
            if (i == 0) {
                tempEnemyProp = (float)(Constants.Functions.getDecreaseCurve(5 * i) + 0.025 * RuntimeSpecs.mapLevel);
            } else if (i == 1) {
                tempEnemyProp = Constants.Functions.getDecreaseCurve(5 * i) + Mathf.Pow(RuntimeSpecs.mapLevel, 1.4f);
            } else {
                tempEnemyProp = Constants.Functions.getDecreaseCurve(5 * i) + 0.05f * Mathf.Pow(2, -i - 1) * Mathf.Pow(RuntimeSpecs.mapLevel, 2f + (0.2f) * (i + 1));
            }

            enemyPropabilities.Add(tempEnemyProp);
            sumEnemyProp += tempEnemyProp;
        }
        //Normalize Array
        for (int i = 0; i < enemyPropabilities.ToArray().Length; i++) {
            enemyPropabilities[i] = enemyPropabilities[i] / sumEnemyProp;
        }
        BuildPropabilityArray();
    }

    private void BuildPropabilityArray() {
        for (int i = 0; i < enemyPropabilities.ToArray().Length; i++) {
            for (int j = 0; j < 100 * enemyPropabilities[i]; j++) {
                lowLevelPropArray.Add(enemyPropabilities[i]);
            }
        }
    }

    public int PickRandomEnemy() {
        int random = Random.Range(0, lowLevelPropArray.ToArray().Length);
        //Debug.Log("Random: " + random);
        float randomPropability = lowLevelPropArray[random];
        //Debug.Log("Random Prop: " + randomPropability);
        int index = 0;
        List<int> indexes = new List<int>();
        for (int i = 0; i < enemyPropabilities.ToArray().Length; i++) {
            if (enemyPropabilities[i] == randomPropability) {
                index = i;
                indexes.Add(index);
            }
        }
        index = indexes[Random.Range(0, indexes.ToArray().Length)];
        //Debug.Log("Index: " + index);
        return index;
    }

    public void DisplayStats() {
        for (int i = 0; i < enemyPropabilities.ToArray().Length; i++) {
            //Debug.Log("Enemy Propability of enemy " + i + ": " + enemyPropabilities[i]);
        }

        for (int i = 0; i < lowLevelPropArray.ToArray().Length; i++) {
            //Debug.Log("Propability Array " + i + ": " + propabilityArray[i]);
        }
    }
}
