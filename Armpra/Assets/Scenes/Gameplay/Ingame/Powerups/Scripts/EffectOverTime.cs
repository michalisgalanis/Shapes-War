using UnityEngine;

public class EffectOverTime : MonoBehaviour {
    //References
    private Referencer rf;
    private ParticleSystem healingParticles;

    //Runtime Variables
    public Constants.Gameplay.Powerups.overTimePowerupTypes typeSelected;
    [HideInInspector] public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectStoreCounter;
    [HideInInspector] public bool used;
    [HideInInspector] public float effectValue;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }
    public void Start() {
        effectStoreCounter = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POWERUP_EFFECT).counter;
        powerupMultiplier = 0f;
        used = false;
    }

    public void FixedUpdate() {
        if (used) {
            switch (typeSelected) {
                case Constants.Gameplay.Powerups.overTimePowerupTypes.HEALTH_REGEN_POWERUP:
                RuntimeSpecs.currentPlayerHealth = Mathf.Clamp(RuntimeSpecs.currentPlayerHealth + effectValue, 0, rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH));
                Debug.Log("CurrentPlayerHealth = " + RuntimeSpecs.currentPlayerHealth + " PowerupMultiplier= " + powerupMultiplier);
                break;
            }
        }
    }
           

    public void EnableEffect() {
        used = true;
        powerupMultiplier = Constants.Functions.getPowerupEffectMultiplier(effectStoreCounter);
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.overTimePowerupTypes.HEALTH_REGEN_POWERUP:
            effectValue = (1 + powerupMultiplier) * rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH) / 3000; //Effect Value is Health Boost
            Vector3 initialScale = rf.healingParticles.transform.localScale;
            float sizeIncrease = Mathf.Max(PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1), 1);
            healingParticles = Instantiate(rf.healingParticles, rf.player.transform.localPosition, Quaternion.identity);
            var main = healingParticles.main;
            main.duration = GetComponent<PowerupDuration>().GetTimeLeft();
            main.startColor = new ParticleSystem.MinMaxGradient(rf.powerupTypes[1].transform.GetChild(0).GetComponent<SpriteRenderer>().color);
            healingParticles.Play();
            healingParticles.transform.localScale = initialScale * sizeIncrease;
            healingParticles.transform.parent = rf.player.transform;
            break;
            case Constants.Gameplay.Powerups.overTimePowerupTypes.ZOOM_OUT_POWERUP:
            rf.camFollowPlayer.maxZoomOutFactor = Mathf.Clamp(powerupMultiplier, 0, 2);
            rf.camFollowPlayer.zoomOutEffectActive = true;
            break;
        }
        rf.ps.EstimateStats();
    }

    public void DisableEffect() {
        powerupMultiplier = 0f;
        effectValue = 0f;
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.overTimePowerupTypes.ZOOM_OUT_POWERUP:
            rf.camFollowPlayer.zoomOutEffectActive = false;
            break;
        }
        rf.ps.EstimateStats();

        Destroy(gameObject);
    }

    public void ResetEffect() {
        EnableEffect();
    }

    public void DisplayEffectStats() {
        Debug.Log(typeSelected.ToString() + ": " + powerupMultiplier);
    }
}
