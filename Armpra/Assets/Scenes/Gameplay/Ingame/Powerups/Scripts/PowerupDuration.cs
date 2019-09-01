using UnityEngine;

public class PowerupDuration : MonoBehaviour {
    //References
    private Referencer rf;

    //Class Variables
    private float powerupDuration;
    private float timeLeft;
    private int effectDurationStoreCounter;
    private bool isActive = false;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }
    public void Start() {
        effectDurationStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_DURATION).counter;
        powerupDuration = Constants.Functions.getPowerupDuration(effectDurationStoreCounter);
    }

    //Running Timer
    public void Update() {
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

    public float GetTimeLeft() {
        return timeLeft;
    }
}
