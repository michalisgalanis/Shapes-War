﻿using UnityEngine;

public class PowerupColliderTrigger : MonoBehaviour {
    private void Start() {
        Physics2D.IgnoreLayerCollision(12, 15);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("Player")) //The player gets the powerup
        {
            bool hasDuration = (GetComponent<PowerupDuration>() != null);
            if (hasDuration) {
                GetComponent<PowerupDuration>().StartTimer();
                //Disabling all internal gameobjects (Can't delete the gameobject itself until we want the powerup's effect to stop)
                for (int i = 0; i < transform.childCount; i++) {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            } else {
                GetComponent<EffectInstant>().EnableEffect();
                Destroy(gameObject);
            }
        }
    }
}