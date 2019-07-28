using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int currentLevel;
    public int enemyCount;
    public GameObject[] enemyTypes;

    private int[] enemyPropabilities;
    private List<int> propabilityArray;

    void Start()
    {
        currentLevel = 0;
        enemyPropabilities = new int[enemyTypes.Length];
        propabilityArray = new List<int>();
    }

    public void EstimateLevel() {
        enemyCount = (int)Mathf.Round(4f + Mathf.Pow(currentLevel, 1.5f));
        Debug.Log("Current Level: " + currentLevel + ", Enemy Count: " + enemyCount);
        int totalTypesOfEnemies = enemyTypes.Length;
        if (enemyCount < 15) {
            enemyPropabilities[0] = 1;
            for (int i = 1; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = 0;
            }
        }
        else if (enemyCount < 100) {
            int sumFactors = 0;
            for (int i = 0; i < enemyTypes.Length; i++) {
                int factor = enemyTypes.Length - i;
                enemyPropabilities[i] = factor / enemyTypes.Length;
                sumFactors += factor;
            }
            for (int i = 0; i < enemyTypes.Length; i++)
                enemyPropabilities[i] *= sumFactors;
        }
        else if (enemyCount < 500)
            for (int i = 0; i < enemyTypes.Length; i++)
                enemyPropabilities[i] = 1 / enemyTypes.Length;
        else {
            int sumFactors = 0;
            for (int i = 0; i < enemyTypes.Length; i++) {
                enemyPropabilities[i] = i / enemyTypes.Length;
                sumFactors += i;
            }
            for (int i = 0; i < enemyTypes.Length; i++)
                enemyPropabilities[i] *= sumFactors;
        }
        BuildPropabilityArray();
    }

    private void BuildPropabilityArray()
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            for (int j = 0; j < 10 * enemyPropabilities[i]; j++)
            {
                propabilityArray.Add(enemyPropabilities[i]);
            }
        }
        DisplayStats();
    }

    public int PickRandomEnemy()
    {
        int random = Random.Range(0, propabilityArray.ToArray().Length);
        Debug.Log("Random: " + random);
        int randomPropability = propabilityArray[random];
        Debug.Log("Random Prop: " + randomPropability);
        int index = 0;
        for (int i = 0; i < enemyPropabilities.Length; i++)
        {
            if (enemyPropabilities[i] == randomPropability)
            {
                index = i;
            }
        }
        Debug.Log("Index: " + index);
        return index;
    }

    public void DisplayStats(){
        for (int i = 0; i < enemyPropabilities.Length; i++)
        {
            Debug.Log("Enemy Propability of enemy " + i + ": " + enemyPropabilities[i]);
        }

        for (int i = 0; i < propabilityArray.ToArray().Length; i++)
        {
            Debug.Log("Propability Array " + i + ": " + propabilityArray[i]);
        }
    }
}
