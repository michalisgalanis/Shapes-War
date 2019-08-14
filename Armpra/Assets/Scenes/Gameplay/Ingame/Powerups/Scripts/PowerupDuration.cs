using UnityEngine;

public class PowerupDuration : MonoBehaviour {
    //Class Variables
    private float powerupDuration;
    private float timeLeft;
    private bool isActive = false;
    private int durationLevel;

    //Needed References
    private StoreSystem ss;

    private void Start() {
        ss = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoreSystem>();
        durationLevel = ss.powerupDurationCounter;
        powerupDuration = 5f + 0.1f * durationLevel;
    }

    //Running Timer
    private void Update() {
        if (isActive) {
            timeLeft -= Time.deltaTime; //Countdown
            if (timeLeft <= 0)  //The powerup's effect is over
            {
                isActive = false;
                SendMessage("DisableEffect");
                Destroy(gameObject);
            }
        }
    }

    public void StartTimer() {
        if (!isActive) {
            SendMessage("EnableEffect");
        } else {
            SendMessage("ResetEffect");
        }

        isActive = true;
        timeLeft = powerupDuration;
    }

}
