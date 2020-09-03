using UnityEngine;

public class Shield : MonoBehaviour {
    //References
    private Referencer rf;
    private SpriteRenderer outerShield;
    private SpriteRenderer innerShield;
    private ParticleSystem movingTrails;
    private ParticleSystem localTrails;

    //Setup Stats
    [Header("Setup Stats")]
    [HideInInspector] public float maxShieldHealth;
    [HideInInspector] public float shieldDamage;

    //Runtime Variables
    private float currentHealth;
    private bool markedForDestroy = false;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        GameObject shield = GameObject.FindGameObjectWithTag(Utility.Tags.SHIELD_TAG);
        outerShield = shield.transform.GetChild(0).GetComponent<SpriteRenderer>();
        innerShield = shield.transform.GetChild(1).GetComponent<SpriteRenderer>();
        movingTrails = shield.transform.GetChild(2).GetComponent<ParticleSystem>();
        localTrails = shield.transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    public void Start() {
        maxShieldHealth = 0.2f * rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MAX_HEALTH);
        shieldDamage = 0.1f * rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MELEE_DAMAGE);
        RestoreShieldStats();
    }

    public void RefreshVisuals() {
        float h = (currentHealth / maxShieldHealth) / 3.6f;
        outerShield.color = Utility.Functions.GeneralFunctions.generateColor(h, 1f, 1f, 0.7f);
        innerShield.color = Utility.Functions.GeneralFunctions.generateColor(h, 1f, 0.7f, 0.2f);
        ParticleSystem.MainModule ps = movingTrails.main;
        ps.startColor = new ParticleSystem.MinMaxGradient(outerShield.color);
        ps = localTrails.main;
        ps.startColor = new ParticleSystem.MinMaxGradient(outerShield.color);
    }

    public void TakeDamage(float damage) {
        currentHealth -= Mathf.Min(damage, currentHealth);
        RefreshVisuals();
        if (currentHealth <= 0 && !markedForDestroy) {
            markedForDestroy = true;
            ParticleSystem explosion = Instantiate(rf.shieldDestroyExplosionParticles, transform.position, Quaternion.identity);
            explosion.transform.parent = rf.spawnedParticles.transform;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag(Utility.Tags.ENEMY_TAG)) {
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(shieldDamage, false);
        }
    }

    public void RestoreShieldStats() {
        currentHealth = maxShieldHealth;
        RefreshVisuals();
    }
}