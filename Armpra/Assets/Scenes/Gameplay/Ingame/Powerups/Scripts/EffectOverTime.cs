using UnityEngine;

public class EffectOverTime : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    public Constants.Gameplay.Powerups.overTimePowerupTypes typeSelected;
    [HideInInspector] public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectStoreCounter;
    [HideInInspector] public bool used;

    private bool hpu=false;
    private float healthBoost;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }
    public void Start() {
        effectStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
        powerupMultiplier = 0f;
        used = false;
        if (typeSelected == Constants.Gameplay.Powerups.overTimePowerupTypes.HEALTH_REGEN_POWERUP) {
            hpu = true;
            healthBoost = (1+powerupMultiplier)* rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH) / 3000; 
        }
    }

    public void FixedUpdate() {
        if (hpu&&used) {
            RuntimeSpecs.currentPlayerHealth = Mathf.Clamp(RuntimeSpecs.currentPlayerHealth + healthBoost, 0, rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH));
            Debug.Log("CurrentPlayerHealth = " + RuntimeSpecs.currentPlayerHealth + " PowerupMultiplier= " + powerupMultiplier);
        }
    }
           

    public void EnableEffect() {
        powerupMultiplier = Constants.Functions.getPowerupEffectMultiplier(effectStoreCounter);
        used = true;
        rf.ps.EstimateStats();
        //DisplayEffectStats();
    }

    public void DisableEffect() {
        powerupMultiplier = 0f;
        rf.ps.EstimateStats();
        Destroy(gameObject);
        //DisplayEffectStats();
    }

    public void ResetEffect() {
        EnableEffect();
    }

    public void DisplayEffectStats() {
        Debug.Log(typeSelected.ToString() + ": " + powerupMultiplier);
    }
}
