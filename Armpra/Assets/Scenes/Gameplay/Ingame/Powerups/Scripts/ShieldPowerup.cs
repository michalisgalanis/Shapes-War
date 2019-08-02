using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    public GameObject shield;
    private GameObject exShield;
    private GameObject player;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        exShield = GameObject.FindGameObjectWithTag("Shield");
        if (collision.tag == "Player") {
            if (exShield == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                Instantiate(shield, player.GetComponent<Transform>().localPosition, Quaternion.identity).transform.parent = player.transform;
            }
            else
            {
                exShield.GetComponent<Shield>().RestoreShieldStats();
                /*shield.GetComponent<Shield>().timeLeft = shield.GetComponent<Shield>().duration;
                shield.GetComponent<Shield>().currentHealth = shield.GetComponent<Shield>().maxShieldHealth;*/

            }

            Destroy(gameObject);
        }
    }
}
