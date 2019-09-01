using UnityEngine;

public class EffectInstant : MonoBehaviour {
    //References
    private Referencer rf;
    private ParticleSystem healingParticles;

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
            Vector3 initialScale = rf.healingParticles.transform.localScale;
            float sizeIncrease = Mathf.Max(PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1), 1);
            healingParticles = Instantiate(rf.healingParticles, rf.player.transform.localPosition, Quaternion.identity);
            var main = healingParticles.main;
            main.duration = 0.5f;
            main.startColor = new ParticleSystem.MinMaxGradient(rf.powerupTypes[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color);
            healingParticles.Play();
            healingParticles.transform.localScale = initialScale * sizeIncrease;
            healingParticles.transform.parent = rf.player.transform;
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
