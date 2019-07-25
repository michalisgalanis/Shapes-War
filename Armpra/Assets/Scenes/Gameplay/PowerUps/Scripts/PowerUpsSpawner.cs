using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{

    public float spawnTimer;
    private float currentTimer;
    public GameObject[] powerUps;

    private float effectMultiplier; //min is 1X, max is 4X

    private const float MIN_BORDER = -19;
    private const float MAX_BORDER = 19;

    void Start(){
        currentTimer = spawnTimer;
    }

    void Update(){
        SpawnPowerUp();
    }

    void SpawnPowerUp(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer)
        {
            //Generate Power Up
            Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
            GameObject newEnemy = Instantiate(powerUps[Random.Range(0, powerUps.Length)], temp, Quaternion.identity);
            newEnemy.transform.parent = transform;
            //Customizing Body Color
            SpriteRenderer sr = newEnemy.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            float h = Random.Range(90f, 360f)/ 360f, s = 0.50f, v = 1f;
            sr.color = Color.HSVToRGB(h, s, v);
            effectMultiplier = h * 4;
            //Resetting timer
            currentTimer = 0f;
        }
    }

    float GenerateHue(){
        float temp_Hue = Random.Range(90f, 360f);
        if (temp_Hue > 235 && temp_Hue < 275)
        {
            temp_Hue = GenerateHue();
        }
        return temp_Hue;
    }
}
