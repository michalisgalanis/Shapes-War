using UnityEngine;

public class EffectOverTime : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    public Constants.Gameplay.Powerups.overTimePowerupTypes typeSelected;
    [HideInInspector] public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectStoreCounter;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }
    public void Start() {
        effectStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
    }

    public void EnableEffect() {
        powerupMultiplier = Constants.Functions.getPowerupEffectMultiplier(effectStoreCounter);
        //DisplayEffectStats();
    }

    public void DisableEffect() {
        powerupMultiplier = 0f;
        //DisplayEffectStats();
    }

    public void ResetEffect() {
        EnableEffect();
    }

    public void DisplayEffectStats() {
        Debug.Log(typeSelected.ToString() + ": " + powerupMultiplier);
    }
}
