using UnityEngine;

public class PowerUp : MonoBehaviour {
    public Utility.Gameplay.Powerups.powerUpTypes powerUpType;
    public Utility.Gameplay.Powerups.overTimePowerupTypes overTimePowerupType;
    public Utility.Gameplay.Powerups.instantPowerupTypes instantPowerupType;
    public Utility.Gameplay.Powerups.powerupTiers powerupTier;

    private AudioManager audioManager;
    private PowerUpManager powerUpManager;
    private Color color;

    public void Start() {
        powerUpManager = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<PowerUpManager>();
        color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        audioManager = GameObject.FindGameObjectWithTag(Utility.Tags.AUDIO_MANAGER_TAG).GetComponent<AudioManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(Utility.Tags.PLAYER_TAG) && !collision.usedByEffector) {
            RuntimeSpecs.powerupsActivated++;
            audioManager.PlayByNumber(Utility.Audio.soundTypes.SFX, 5);
            switch (powerUpType) {
                case Utility.Gameplay.Powerups.powerUpTypes.overTimePowerupTypes:
                    powerUpManager.OverTimePowerUpEnable(overTimePowerupType, color);
                    break;
                case Utility.Gameplay.Powerups.powerUpTypes.instantPowerupTypes:
                        powerUpManager.InstantEffectPowerupEnable(instantPowerupType, color);
                        break;
            }
            Destroy(gameObject);
        }
    }
}