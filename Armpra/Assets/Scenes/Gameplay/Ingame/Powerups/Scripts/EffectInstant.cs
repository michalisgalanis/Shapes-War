using UnityEngine;

public class EffectInstant : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    public Constants.Gameplay.Powerups.instantPowerupTypes typeSelected;
    [HideInInspector] public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectStoreCounter;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }

    public void Start() {
        effectStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
        powerupMultiplier = Constants.Functions.getPowerupEffectMultiplier(effectStoreCounter);
        rf.ps.EstimateStats();
    }

    public void EnableEffect() {
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.instantPowerupTypes.INSTANT_HEAL_POWERUP:
            rf.ps.InstantHeal(powerupMultiplier);
            rf.ps.EstimateStats();
            break;
            case Constants.Gameplay.Powerups.instantPowerupTypes.COIN_PACK_POWERUP:
            rf.cs.addCoins((int) (20 + 100 * RuntimeSpecs.playerLevel * powerupMultiplier));
            break;
            case Constants.Gameplay.Powerups.instantPowerupTypes.XP_PACK_POWERUP:
            rf.pe.addXP((double)(20 + 100 * RuntimeSpecs.playerLevel * powerupMultiplier));
            break;
        }
        //DisplayEffectStats();
        DisableEffect();
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
