using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{ 
    public float powerupDuration = 5.0f;
    public float powerupMultiplier = 0.4f;        //0f is (100% effect), 1f is (200% effect)
    private float timeLeft;
    private bool isActive;
    private PlayerMovement player;

    void Start()
    {
        isActive = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isActive)
        {
            timeLeft -= Time.deltaTime; //Countdown
            if (timeLeft <= 0)  //The powerup's effect is over
            {
                player.velocityFactor *= powerupMultiplier;
                isActive = false;
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
