using UnityEngine;

public class Bullet : MonoBehaviour {
    //References
    private Referencer rf;
    [HideInInspector] public BulletSpecs bs;
    private Rigidbody2D rb;

    //Setup Variables
    [Header("Setup")]
    public bool playerFired;

    //Runtime Variables
    private float currentSpeed = 0f;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        bs = GetComponent<BulletSpecs>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start() {
        if (bs.aoe) {
            bs.blastDamage = 0.2f * bs.damage;
            bs.blastRadius = 1.5f;
        }
    }

    public void Update() {
        currentSpeed = Mathf.Min(currentSpeed + bs.acceleration, bs.maxSpeed);
        rb.velocity = transform.up * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (playerFired) {
            if (hitInfo.CompareTag(Constants.Tags.ENEMY_TAG)) {
                Enemy enemy = hitInfo.GetComponent<Enemy>();
                if (enemy != null) {
                    Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform;
                    if (bs.aoe) {
                        ParticleSystem aoeExplosion = Instantiate(rf.aoeExplosion, enemy.transform.position, Quaternion.identity);
                        aoeExplosion.transform.parent = rf.spawnedParticles.transform;
                        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag(Constants.Tags.ENEMY_TAG);
                        foreach (GameObject activeEnemy in activeEnemies) {
                            if (activeEnemy.activeInHierarchy && activeEnemy.GetComponent<Enemy>() != null && bs.blastRadius >= Vector2.Distance(activeEnemy.transform.position, aoeExplosion.transform.position))
                                activeEnemy.GetComponent<Enemy>().TakeDamage(bs.blastDamage);
                        }
                    }
                    enemy.TakeDamage(bs.damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag(Constants.Tags.SHIELD_TAG)) {
                Shield shield = hitInfo.GetComponent<Shield>();
                if (shield != null) {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), hitInfo);
                }
            }
        } else {
            if (hitInfo.CompareTag(Constants.Tags.SHIELD_TAG)) {
                Shield shield = hitInfo.GetComponent<Shield>();
                if (shield != null) {
                    Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform;
                    shield.TakeDamage(bs.damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag(Constants.Tags.PLAYER_TAG)) {
                PlayerStats player = hitInfo.GetComponent<PlayerStats>();
                if (player != null) {
                    Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform;
                    ;
                    player.TakeDamage(bs.damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag(Constants.Tags.ENEMY_TAG)) {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), hitInfo);
            }
        }
        if (hitInfo.CompareTag(Constants.Tags.MAP_BOUNDS_TAG)) {
            Destroy(gameObject);
        }
    }
}
