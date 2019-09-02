using UnityEngine;

public class Enemy : MonoBehaviour {
    //References
    private Referencer rf;
    private SpriteRenderer enemyBorder;
    private SpriteRenderer enemyBody;
    private SpriteRenderer[] enemyHeads;
    private ParticleSystem trails;

    //Enemy Setup Stats
    [Header("Setup Stats")]
    public float maxHealth;
    public float meleeDamage;
    public int xpOnDeath;
    public int coinsOnDeath;

    //Runtime Variables
    [HideInInspector] public float currentHealth;
    private bool markedForDestruction;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        enemyHeads = new SpriteRenderer[transform.GetChild(0).childCount];
        enemyBorder = transform.GetChild(1).GetComponent<SpriteRenderer>();
        enemyBody = transform.GetChild(2).GetComponent<SpriteRenderer>();
        trails = transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    public void Start() {
        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            enemyHeads[i] = transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>();
        }
        ParticleSystem.MainModule settings = trails.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(enemyBody.color);
        currentHealth = maxHealth;
        markedForDestruction = false;
    }

    private void Update() {
        Color.RGBToHSV(enemyBorder.color, out float h, out float s, out float v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        enemyBorder.color = (Color.HSVToRGB(h, s, v));
        foreach (SpriteRenderer enemyHead in enemyHeads) {
            enemyHead.color = (Color.HSVToRGB(h, s, v));
        }
    }
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0 && !markedForDestruction) {
            markedForDestruction = true;
            Instantiate(rf.enemyDeathExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform;
            Destroy(gameObject);
            RuntimeSpecs.enemiesKilled++;
            rf.pe.addXP(xpOnDeath);
            rf.cs.addCoins(coinsOnDeath);
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag(Constants.Tags.PLAYER_TAG)) {
            PlayerStats player = hitInfo.GetComponent<PlayerStats>();
            if (player != null) {
                player.TakeDamage(meleeDamage);
            }
        } else if (hitInfo.CompareTag(Constants.Tags.SHIELD_TAG)) {
            Shield shield = hitInfo.GetComponent<Shield>();
            if (shield != null) {
                shield.TakeDamage(meleeDamage);
            }
        }
    }
}
