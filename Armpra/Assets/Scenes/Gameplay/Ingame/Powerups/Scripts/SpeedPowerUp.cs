using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{ 
    public float powerupDuration;
    public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private float timeLeft;
    private bool isActive;

    void Start()
    {
        isActive = false;
    }

    void Update()
    {
        if (isActive)
        {
            timeLeft -= Time.deltaTime; //Countdown
            if (timeLeft <= 0)  //The powerup's effect is over
            {
                isActive = false;
                powerupMultiplier = 0f;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player")) //The player gets the powerup
        {
            timeLeft = powerupDuration;
            isActive = true;
            //Disabling all sprite renderers (Can't delete the gameobject itself until we want the powerup's effect to stop)
            for (int i = 0; i < 3; i++)
            {
                SpriteRenderer temp = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                temp.enabled = false;
            }
            ParticleSystemRenderer temp2 = gameObject.transform.GetChild(3).GetComponent<ParticleSystemRenderer>();
            temp2.enabled = false;
        }
    }
}
