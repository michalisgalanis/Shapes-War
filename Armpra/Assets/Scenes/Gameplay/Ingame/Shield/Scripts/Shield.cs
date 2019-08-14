using UnityEngine;

public class Shield : MonoBehaviour {
    //Shield Stats
    public float maxShieldHealth;
    public float shieldDamage;

    //Dynamic Stats
    public float currentHealth;
    private bool markedForDestroy;

    //Essential References
    private SpriteRenderer outerShield;
    private SpriteRenderer innerShield;
    private ParticleSystem movingTrails;
    private ParticleSystem localTrails;
    public GameObject shieldShockwavePrefab;
    public ParticleSystem shieldDestroyExplosionParticlesPrefab;

    private void Start() {
        outerShield = GameObject.FindGameObjectWithTag("Shield").transform.GetChild(0).GetComponent<SpriteRenderer>();
        innerShield = GameObject.FindGameObjectWithTag("Shield").transform.GetChild(1).GetComponent<SpriteRenderer>();
        movingTrails = GameObject.FindGameObjectWithTag("Shield").transform.GetChild(2).GetComponent<ParticleSystem>();
        localTrails = GameObject.FindGameObjectWithTag("Shield").transform.GetChild(3).GetComponent<ParticleSystem>();
        currentHealth = maxShieldHealth;

        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(9, 14);
        markedForDestroy = false;
    }

    private void Update() {
        if (outerShield && innerShield) {
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
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0 && !markedForDestroy) {
            markedForDestroy = true;
            //Instantiate(shieldShockwavePrefab, transform.localPosition, Quaternion.identity);
            Instantiate(shieldDestroyExplosionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            markedForDestroy = false;
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("Enemy")) {
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