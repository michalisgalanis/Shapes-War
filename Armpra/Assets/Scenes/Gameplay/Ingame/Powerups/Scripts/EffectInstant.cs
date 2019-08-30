using UnityEngine;

public class EffectInstant : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    public Constants.Gameplay.Powerups.instantPowerupTypes typeSelected;
    public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectStoreCounter;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }

    public void Start() {
        effectStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
        powerupMultiplier = Constants.Functions.getPowerupEffectMultiplier(effectStoreCounter);
    }

    public void EnableEffect() {
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.instantPowerupTypes.INSTANT_HEAL_POWERUP:
            rf.ps.InstantHeal(powerupMultiplier);
            break;
        }
        //DisplayEffectStats();
        DisableEffect();
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
