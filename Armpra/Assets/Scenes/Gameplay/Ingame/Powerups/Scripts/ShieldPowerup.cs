using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    public GameObject shield;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(shield, player.GetComponent<Transform>().localPosition, Quaternion.identity).transform.parent = player.transform;
        Destroy(gameObject);
    }
}
