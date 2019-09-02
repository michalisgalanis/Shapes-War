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
    public float maxShieldHealth;
    public float shieldDamage;

    //Runtime Variables
    private float currentHealth;
    private bool markedForDestroy = false;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        outerShield = rf.shield.transform.GetChild(0).GetComponent<SpriteRenderer>();
        innerShield = rf.shield.transform.GetChild(1).GetComponent<SpriteRenderer>();
        movingTrails = rf.shield.transform.GetChild(2).GetComponent<ParticleSystem>();
        localTrails = rf.shield.transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    public void Start() {
        RestoreShieldStats();
    }

    public void FixedUpdate() {
        float h = (currentHealth / maxShieldHealth) / 3.6f;
        float outerS = 1f, outerV = 1f, outerA = 0.7f;
        float innerS = 1f, innerV = 0.7f, innerA = 0.2f;
        float outerR, outerG, outerB;
        outerR = Color.HSVToRGB(h, outerS, outerV).r;
        outerG = Color.HSVToRGB(h, outerS, outerV).g;
        outerB = Color.HSVToRGB(h, outerS, outerV).b;
        outerShield.color = new Color(outerR, outerG, outerB, outerA);
        float innerR, innerG, innerB;
        innerR = Color.HSVToRGB(h, innerS, innerV).r;
        innerG = Color.HSVToRGB(h, innerS, innerV).g;
        innerB = Color.HSVToRGB(h, innerS, innerV).b;
        innerShield.color = new Color(innerR, innerG, innerB, innerA);
        ParticleSystem.MainModule ps = movingTrails.main;
        ps.startColor = new ParticleSystem.MinMaxGradient(outerShield.color);
        ps = localTrails.main;
        ps.startColor = new ParticleSystem.MinMaxGradient(outerShield.color);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0 && !markedForDestroy) {
            markedForDestroy = true;
            //Instantiate(shieldShockwavePrefab, transform.localPosition, Quaternion.identity);
            ParticleSystem explosion = Instantiate(rf.shieldDestroyExplosionParticles, transform.position, Quaternion.identity);
            explosion.transform.parent = rf.spawnedParticles.transform;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag(Constants.Tags.ENEMY_TAG)) {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(shieldDamage);
            }
        }
    }

    public void RestoreShieldStats() {
        currentHealth = maxShieldHealth;
    }
}