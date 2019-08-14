using UnityEngine;

public class EffectOverTime : MonoBehaviour {
    //Enums & InstantiationTypes
    public enum PowerupType { AttackSpeed, MeleeDamage, HealthRegen, Immunity, RangedDamage, MovementSpeed }
    public PowerupType typeSelected;

    //Effect Multiplier
    [HideInInspector]
    public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectLevel;

    //Needed References
    private StoreSystem ss;

    private void Start() {
        ss = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoreSystem>();
        effectLevel = ss.powerupEffectCounter;
    }

    public void EnableEffect() {
        powerupMultiplier = 0.2f + effectLevel / 125f;
        DisplayEffectStats();
    }

    public void DisableEffect() {
        powerupMultiplier = 0f;
        DisplayEffectStats();
    }

    public void ResetEffect() {
        EnableEffect();
    }

    public void DisplayEffectStats() {
        Debug.Log(typeSelected.ToString() + ": " + powerupMultiplier);
    }
}
