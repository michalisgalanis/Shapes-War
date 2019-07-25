using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{ 
    private float initialMovementSpeed; //Used to reset player stat after powerup ends
    public float powerupDuration = 5.0f;
    public float powerupMultiplier=1.5f;
    private float timeLeft;
    private bool isActive;
    PlayerStats stats;

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
                stats.movementSpeed = initialMovementSpeed; //Resetting speed
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name=="Player") //The player gets the powerup
        {
            timeLeft = powerupDuration;
            stats = hitInfo.GetComponent<PlayerStats>();
            initialMovementSpeed = stats.movementSpeed; //Saves initial stat (speed) of the player so that it can be reset to it after the powerup's effect is over
            stats.movementSpeed *= powerupMultiplier;   //Modifying player's stat (speed) to start powerup's effect
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
