using UnityEngine;

public class Bullet : MonoBehaviour {
    //References
    private Referencer rf;
    private BulletSpecs bs;
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

    public void Update() {
        currentSpeed += bs.acceleration;
        if (currentSpeed > bs.maxSpeed) {
            currentSpeed = bs.maxSpeed;
        }
        rb.velocity = transform.up * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (playerFired) {
            if (hitInfo.CompareTag(Constants.Tags.ENEMY_TAG)) {
                Enemy enemy = hitInfo.GetComponent<Enemy>();
                if (enemy != null) {
                    Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform;
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
                    Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform; ;
                    shield.TakeDamage(bs.damage);
                }
                Destroy(gameObject);
            } else if (hitInfo.CompareTag(Constants.Tags.PLAYER_TAG)) {
                PlayerStats player = hitInfo.GetComponent<PlayerStats>();
                if (player != null) {
                    Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity).transform.parent = rf.spawnedParticles.transform; ;
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
