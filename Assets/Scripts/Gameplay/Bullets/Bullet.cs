using UnityEngine;

public class Bullet : MonoBehaviour {
    //References
    private Rigidbody2D rb;
    private Referencer rf;

    //Setup Variables
    [HideInInspector] public bool playerFired;
    public Utility.Gameplay.Bullet.bulletTypes bulletType;
    [HideInInspector] public float externalDamageFactor;
    private bool aoe;
    private float blastRadius;
    private float blastDamage;
    private float damage;
    [HideInInspector] public float acceleration;
    [HideInInspector] public float maxSpeed;

    //Runtime Variables
    private float currentSpeed = 0f;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
    }

    public void Start() {
        EstimateStats();
    }

    public void Update() {
        currentSpeed = Mathf.Min(currentSpeed + acceleration, maxSpeed);
    }

    public void FixedUpdate() {
        rb.velocity = transform.up * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        bool enable = (playerFired && hitInfo.CompareTag(Utility.Tags.ENEMY_TAG) && hitInfo.isTrigger)
            || (!playerFired && hitInfo.CompareTag(Utility.Tags.SHIELD_TAG) && !hitInfo.isTrigger)
            || (!playerFired && hitInfo.CompareTag(Utility.Tags.PLAYER_TAG) && !hitInfo.isTrigger)
            || (!playerFired && hitInfo.CompareTag(Utility.Tags.ENEMY_TAG) && rf.gameManager.GetComponent<PowerUpManager>().activeOvertimePowerups[10]);
        //Debug.Log(((playerFired) ? "Player" : "Enemy") + " Fired. Will " + ((!enable) ? "not " : "") + "apply damage to " + hitInfo.name);
        Hit(hitInfo, enable);
    }

    private void Hit(Collider2D hitInfo, bool enable) {
        if (!enable) {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hitInfo);
            if (hitInfo.CompareTag(Utility.Tags.MAP_BOUNDS_TAG))
                Destroy(gameObject);
            return;
        }
        CreateEffect(hitInfo);
        DealDamage(hitInfo);
        if (bulletType != Utility.Gameplay.Bullet.bulletTypes.PENETRATION && bulletType != Utility.Gameplay.Bullet.bulletTypes.ELECTRICITY)
            Destroy(gameObject);
    }

    private void CreateEffect(Collider2D hitInfo) {
        switch (hitInfo.tag) {
            case Utility.Tags.PLAYER_TAG:
                switch (bulletType) {
                    case Utility.Gameplay.Bullet.bulletTypes.SNOW:
                        rf.pm.startSlowDown();
                        break;
                }
                break;
            case Utility.Tags.ENEMY_TAG:
                switch (bulletType) {
                    case Utility.Gameplay.Bullet.bulletTypes.SNOW:
                        hitInfo.GetComponentInParent<EnemyFollowPlayer>().startSlowDown();
                        break;
                }
                break;
        }
    }

    private void DealDamage(Collider2D hitInfo) {
        ParticleSystem aoeExplosion = null;
        if (aoe) {
            aoeExplosion = Instantiate(rf.aoeExplosion, hitInfo.transform.position, Quaternion.identity);
            aoeExplosion.transform.parent = rf.spawnedParticles.transform;
            ParticleSystem.ColorOverLifetimeModule colm = aoeExplosion.colorOverLifetime;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(GetComponentInChildren<SpriteRenderer>().color, 0.0f), new GradientColorKey(GetComponentInChildren<SpriteRenderer>().color, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f)}
        );
            colm.color = gradient;
            aoeExplosion.Play();
        }
        if (hitInfo.CompareTag(Utility.Tags.ENEMY_TAG)) {
            RuntimeSpecs.bulletsHit++;
            hitInfo.GetComponentInParent<Enemy>().TakeDamage(damage, true);
            if (aoe) {
                GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag(Utility.Tags.ENEMY_TAG);
                foreach (GameObject activeEnemy in activeEnemies) {
                    if (activeEnemy.activeInHierarchy && activeEnemy.GetComponent<Enemy>() != null && blastRadius >= Vector2.Distance(activeEnemy.transform.position, aoeExplosion.transform.position))
                        activeEnemy.GetComponent<Enemy>().TakeDamage(blastDamage, true);
                }
            }
        } else if (hitInfo.CompareTag(Utility.Tags.SHIELD_TAG)) {
            hitInfo.GetComponent<Shield>().TakeDamage(damage + blastDamage);
        } else if (hitInfo.CompareTag(Utility.Tags.PLAYER_TAG)) {
            rf.ps.TakeDamage(damage + blastDamage, false, true);
        }
        ParticleSystem ps = Instantiate(rf.hitExplosionParticles, transform.position, Quaternion.identity);
        ps.transform.parent = rf.spawnedParticles.transform;
        ParticleSystem.MainModule main = ps.main;
        main.startColor = GetComponentInChildren<SpriteRenderer>().color;
    }


    public void EstimateStats() {
        float initialBulletDamage = 0f, initialBlastDamage = 0f;
        switch (bulletType) {
            case Utility.Gameplay.Bullet.bulletTypes.NORMAL:
                aoe = false;
                maxSpeed = 20f;
                acceleration = 1.5f;
                initialBulletDamage = 80f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.HV:
                aoe = false;
                maxSpeed = 60f;
                acceleration = 3f;
                initialBulletDamage = 120f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.EXPLOSIVE:
                aoe = true;
                maxSpeed = 10f;
                acceleration = 1f;
                initialBulletDamage = 100f;
                initialBlastDamage = initialBulletDamage * 0.2f;
                blastRadius = 2.5f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.POISONOUS:
                aoe = true;
                maxSpeed = 20f;
                acceleration = 1.5f;
                initialBulletDamage = 90f;
                initialBlastDamage = initialBulletDamage * 0.1f;
                blastRadius = 1.5f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.SNOW:
                aoe = true;
                maxSpeed = 20f;
                acceleration = 2f;
                initialBulletDamage = 50f;
                initialBlastDamage = initialBulletDamage * 0.1f;
                blastRadius = 2f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.PENETRATION:
                aoe = false;
                maxSpeed = 40f;
                acceleration = 2.2f;
                initialBulletDamage = 80f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.SHOTGUN_CELL:
                aoe = false;
                maxSpeed = 15f;
                acceleration = 1.3f;
                initialBulletDamage = 120f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.MINIGUN_CELL:
                aoe = false;
                maxSpeed = 25f;
                acceleration = 1.5f;
                initialBulletDamage = 30f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.MISSILE:
                aoe = true;
                maxSpeed = 100f;
                acceleration = 0.25f;
                initialBulletDamage = 250f;
                initialBlastDamage = initialBulletDamage * 0.4f;
                blastRadius = 5f;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.ELECTRICITY:
                aoe = true;
                maxSpeed = 100f;
                acceleration = 4f;
                initialBulletDamage = 40f;
                initialBlastDamage = initialBulletDamage * 0.6f;
                blastRadius = 2f;
                break;
        }
        damage = initialBulletDamage * externalDamageFactor;
        if (aoe)
            blastDamage = (initialBlastDamage / initialBulletDamage) * damage;
    }
}