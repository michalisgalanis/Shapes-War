using UnityEngine;

public class EffectInstant : MonoBehaviour {
    //Enums & InstantiationTypes
    public enum PowerupType { InstantHeal }
    public PowerupType typeSelected;

    //Effect Multiplier
    [HideInInspector]
    public float powerupMultiplier;        //0f is 100% stock + 0%, 1f is 100% stock + 100% total
    private int effectLevel;

    //Needed References
    private StoreSystem ss;
    private PlayerStats ps;
    private GameObject gameManager;

    public void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        ps = gameManager.GetComponent<GameplayManager>().FindActualPlayer().GetComponent<PlayerStats>();
        ss = gameManager.GetComponent<StoreSystem>();
        effectLevel = ss.powerupEffectCounter;
        powerupMultiplier = 0.2f + effectLevel / 125f;
    }

    public void EnableEffect() {
        switch (typeSelected) {
            case PowerupType.InstantHeal:
                ps.InstantHeal(powerupMultiplier);
                break;
        }
        DisplayEffectStats();
        DisableEffect();
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
