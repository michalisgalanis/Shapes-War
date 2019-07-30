using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    public GameObject shield;
    private GameObject player;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { 
            player = GameObject.FindGameObjectWithTag("Player");
            Instantiate(shield, player.GetComponent<Transform>().localPosition, Quaternion.identity).transform.parent = player.transform;
            Destroy(gameObject);
        }
    }
}
