using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    public int currentLevel = 1;
    public int enemyCount;
    public GameObject[] enemyTypes;

    private float[] enemyPropabilities;
    private List<float> propabilityArray;

    public void EstimateLevel() {
        enemyPropabilities = new float[enemyTypes.Length];
        propabilityArray = new List<float>();
        enemyCount = (int)Mathf.Round(4f + Mathf.Pow(currentLevel, 1.2f));
        //Debug.Log("Current Level: " + currentLevel + ", Enemy Count: " + enemyCount);
        int totalTypesOfEnemies = enemyTypes.Length;
        if (enemyCount < 15) {
            enemyPropabilities[0] = 1f;
            for (int i = 1; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = 0f;
            }
        } else if (enemyCount < 25) {
            enemyPropabilities[0] = 0.8f;
            enemyPropabilities[1] = 0.2f;
            for (int i = 2; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = 0;
            }
        } else if (enemyCount < 50) {
            enemyPropabilities[0] = 0.5f;
            enemyPropabilities[1] = 0.3f;
            enemyPropabilities[2] = 0.1f;
            for (int i = 3; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = 0;
            }
        } else if (enemyCount < 100) {
            float sumFactors = 0;
            for (int i = 0; i < enemyTypes.Length; i++) {
                float factor = enemyTypes.Length - i;
                //Debug.Log("Factor: " + factor);
                enemyPropabilities[i] = factor / enemyTypes.Length;
                //Debug.Log("Enemy Prop: " + enemyPropabilities[i]);
                sumFactors += factor;
            }
            for (int i = 0; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] *= sumFactors;
                //Debug.Log("New Enemy Prop: " + enemyPropabilities[i]);
            }
        } else if (enemyCount < 500) {
            for (int i = 0; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = 1f / enemyTypes.Length;
                //Debug.Log("Enemy Prop: " + enemyPropabilities[i]);
            }
        } else {
            int sumFactors = 0;
            for (int i = 0; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = i / enemyTypes.Length;
                sumFactors += i;
            }
            for (int i = 0; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] *= sumFactors;
            }
        }
        BuildPropabilityArray();
    }

    private void BuildPropabilityArray() {
        for (int i = 0; i < enemyTypes.Length; i++) {
            for (int j = 0; j < 10 * enemyPropabilities[i]; j++) {
                propabilityArray.Add(enemyPropabilities[i]);
            }
        }
    }

    public int PickRandomEnemy() {
        int random = Random.Range(0, propabilityArray.ToArray().Length);
        //Debug.Log("Random: " + random);
        float randomPropability = propabilityArray[random];
        //Debug.Log("Random Prop: " + randomPropability);
        int index = 0;
        List<int> indexes = new List<int>();
        for (int i = 0; i < enemyPropabilities.Length; i++) {
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
        for (int i = 0; i < enemyPropabilities.Length; i++) {
            //Debug.Log("Enemy Propability of enemy " + i + ": " + enemyPropabilities[i]);
        }

        for (int i = 0; i < propabilityArray.ToArray().Length; i++) {
            //Debug.Log("Propability Array " + i + ": " + propabilityArray[i]);
        }
    }
}
