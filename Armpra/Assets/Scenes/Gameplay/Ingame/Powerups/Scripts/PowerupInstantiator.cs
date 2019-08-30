using UnityEngine;

public class PowerupInstantiator : MonoBehaviour {
    //References
    private Referencer rf;
    private GameObject shield;

    //Runtime Variables
    public Constants.Gameplay.Powerups.instantiatorPowerupTypes typeSelected;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }

    public void EnableEffect() {
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.instantiatorPowerupTypes.SHIELD_POWERUP:
            Vector3 initialScale = rf.shield.transform.localScale;
            float sizeIncrease = PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1);
            if (sizeIncrease == 0) {
                sizeIncrease = 1;
            }

            shield = Instantiate(rf.shield, rf.player.GetComponent<Transform>().localPosition, Quaternion.identity);
            shield.transform.localScale = initialScale * sizeIncrease;
            shield.transform.parent = rf.player.transform;
            break;
        }
    }

    public void DisableEffect() {
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.instantiatorPowerupTypes.SHIELD_POWERUP:
            Destroy(shield);
            break;
        }
    }

    public void ResetEffect() {
        switch (typeSelected) {
            case Constants.Gameplay.Powerups.instantiatorPowerupTypes.SHIELD_POWERUP:
            rf.shieldScript.RestoreShieldStats();
            break;
        }
    }

    public void DisplayEffectStats() {
        Debug.Log(typeSelected.ToString());
    }
}
