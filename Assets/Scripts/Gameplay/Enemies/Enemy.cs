using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //References
    private Referencer rf;
    private SpriteRenderer[] enemyBorders;
    private SpriteRenderer[] enemyBodies;
    private SpriteRenderer[] enemyHeads;
    private ParticleSystem trails;
    private List<SpriteMask> damageMasks;

    //Enemy Setup Stats
    [Header("Setup Stats")]
    public bool isBoss;
    [Range(1, 15)] public int enemyLevel;
    private Utility.Gameplay.Enemy.enemyTypes enemyType;
    private Utility.Gameplay.Enemy.bossTypes bossType;
    //public bool explosiveEnemy;
    private float maxHealth;
    private float meleeDamage;
    private int xpGather;
    private int coinGather;

    //Setup Constant Variables
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    //Runtime Variables
    [HideInInspector] public float currentHealth;
    private bool markedForDestruction;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        damageMasks = new List<SpriteMask>();
        enemyHeads = new SpriteRenderer[transform.GetChild(0).childCount];
        enemyBorders = new SpriteRenderer[transform.GetChild(1).childCount];
        enemyBodies = new SpriteRenderer[transform.GetChild(2).childCount];
        trails = transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    void Start() {
        MIN_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
        /*GameObject shockwave = Instantiate(rf.shockwave, transform.localPosition, Quaternion.identity);
        shockwave.transform.parent = transform;
        Destroy(shockwave, 0.01f);*/

        for (int i = 0; i < transform.GetChild(0).childCount; i++) {
            enemyHeads[i] = transform.GetChild(0).GetChild(i).GetComponent<SpriteRenderer>();
            enemyHeads[i].maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        for (int i = 0; i < transform.GetChild(1).childCount; i++) {
            enemyBorders[i] = transform.GetChild(1).GetChild(i).GetComponent<SpriteRenderer>();
            enemyBorders[i].maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        for (int i = 0; i < transform.GetChild(2).childCount; i++) {
            enemyBodies[i] = transform.GetChild(2).GetChild(i).GetComponent<SpriteRenderer>();
            enemyBodies[i].maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }

        if (isBoss) {
            bossType = (Utility.Gameplay.Enemy.bossTypes)(enemyLevel - 1);
        } else {
            enemyType = (Utility.Gameplay.Enemy.enemyTypes)(enemyLevel - 1);
        }
        EstimateEnemySpecs();

        ParticleSystem.MainModule settings = trails.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(enemyBodies[0].color);
        currentHealth = maxHealth;
        markedForDestruction = false;
        RefreshVisuals();

        float randX = Random.value > 0.5f ? Random.Range(MIN_BORDER, rf.player.transform.localPosition.x - 2f) : Random.Range(rf.player.transform.localPosition.x + 2f, MAX_BORDER);
        float randY = Random.value > 0.5f ? Random.Range(MIN_BORDER, rf.player.transform.localPosition.y - 2f) : Random.Range(rf.player.transform.localPosition.y + 2f, MAX_BORDER);
        Vector3 temp = new Vector3(randX, randY, 0f);
        transform.position = temp;
    }

    private void RefreshVisuals() {
        Color color;
        if (!isBoss) {
            color = Utility.Functions.GeneralFunctions.generateColor(0f, 1f - currentHealth / maxHealth, 1f, 1f);
            if (enemyType == Utility.Gameplay.Enemy.enemyTypes.NINJA) {
                color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 0f, 0.1f);
                foreach (SpriteRenderer sr in enemyBodies)
                    sr.color = color;
            } else {
                foreach (SpriteRenderer sr in enemyBorders)
                    sr.color = color;
                foreach (SpriteRenderer sr in enemyHeads)
                    sr.color = color;
            }
            rf.bossProgressBar.SetActive(false);
        } else {
            float percentageValue = currentHealth / maxHealth;
            rf.bossProgressBar.transform.GetChild(2).GetComponent<ProgressbarAnimation>().ApproachTarget(percentageValue);
            rf.bossProgressBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (int)currentHealth + "/" + (int)maxHealth;
            rf.bossProgressBar.SetActive(true);
        }
        GenerateDamageSpriteMask(5);
    }

    private void GenerateDamageSpriteMask(int maxMasks) {
        int targetMasks = (int)((1f - (currentHealth / maxHealth)) * maxMasks);
        //Debug.Log("Target: " + targetMasks + ", To be Added: " + (targetMasks - damageMasks.Count));
        for (int i = 0; i < targetMasks - damageMasks.Count; i++) {
            SpriteMask sm = Instantiate(rf.damageTypes[Random.Range(0, rf.damageTypes.Length)], transform.position, Quaternion.identity);
            sm.transform.parent = transform;
            sm.transform.localPosition = new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(-0.25f, 0.25f), 0f);
            sm.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-90f, 90f));
            sm.transform.localScale = new Vector3(Random.Range(0.1f, 0.3f), Random.Range(0.1f, 0.3f), 0f);
            damageMasks.Add(sm);
        }
    }

    public void TakeDamage(float damage, bool ranged) {
        RuntimeSpecs.damageDealtToEnemies += currentHealth - damage >= 0 ? damage : currentHealth;
        currentHealth -= Mathf.Min(damage, currentHealth);
        if (GetComponent<HitEffect>() != null) GetComponent<HitEffect>().Hit(1f, 0.3f);
        RefreshVisuals();
        if (currentHealth <= 0 && !markedForDestruction) {
            markedForDestruction = true;
            ParticleSystem deathExplosion = Instantiate(isBoss ? rf.bossDeathExplosionParticles : rf.enemyDeathExplosionParticles, transform.position, Quaternion.identity);
            ParticleSystem.ExternalForcesModule deathExplosionForces = deathExplosion.externalForces;
            ParticleSystem.MainModule deathExplosionMain = deathExplosion.main;
            deathExplosionForces.enabled = true;
            deathExplosionMain.startColor = enemyBodies[0].GetComponent<SpriteRenderer>().color;
            deathExplosion.transform.parent = rf.spawnedParticles.transform;
            ParticleSystem debris = Instantiate(rf.enemyDebris, transform.position, Quaternion.identity);
            debris.transform.localScale = rf.enemyDebris.transform.localScale.x * transform.localScale;
            debris.transform.parent = rf.spawnedParticles.transform;
            ParticleSystem.MainModule debrisMain = debris.main;
            debrisMain.startColor = enemyBodies[0].GetComponent<SpriteRenderer>().color;
            rf.camFollowPlayer.EnableShake(0.12f, 2.5f);
            Destroy(gameObject);
            rf.pe.AddXP(xpGather);
            CoinSystem.AddCoins(coinGather);
            RuntimeSpecs.enemiesKilled++;
            RuntimeSpecs.totalEnemiesKilled++;
            rf.enemiesRemainingText.text = (RuntimeSpecs.enemiesSpawned - RuntimeSpecs.enemiesKilled).ToString();
            //AnimationManager.ResetAnimation(rf.enemiesRemainingText.gameObject, true);
            rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.SFX, isBoss ? 4 : 3);
            if (RuntimeSpecs.enemiesKilled == RuntimeSpecs.enemiesSpawned)
                rf.gm.CompleteLevel();
        } else {
            rf.camFollowPlayer.EnableShake(ranged ? 0.06f : 0.02f, ranged ? 1.2f : 0.2f);
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag(Utility.Tags.PLAYER_TAG) && !hitInfo.usedByEffector) {
            TakeDamage(!isBoss && enemyType == Utility.Gameplay.Enemy.enemyTypes.KAMIKAZE ? currentHealth : rf.ps.GetStatValueOf(Utility.Gameplay.Player.playerStatTypes.MELEE_DAMAGE), false);
            rf.ps.TakeDamage(meleeDamage + ((!isBoss && enemyType == Utility.Gameplay.Enemy.enemyTypes.KAMIKAZE) ? RuntimeSpecs.currentPlayerHealth * 0.15f : 0f), false, false);
        } else if (hitInfo.CompareTag(Utility.Tags.SHIELD_TAG) && hitInfo.GetComponent<Shield>() != null) {
            hitInfo.GetComponent<Shield>().TakeDamage(meleeDamage);
        }
    }

    private void EstimateEnemySpecs() {
        float initialMaxHealth = 0f, initialMeleeDamage = 0f, initialAttackSpeed = 0f, initialXpGather = 0f, initialCoinsGather = 0f;
        if (!isBoss) {
            switch (enemyType) {
                case Utility.Gameplay.Enemy.enemyTypes.SCOUT:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 2.5f;
                    initialMaxHealth = 100f;
                    initialMeleeDamage = 0.1f;
                    initialXpGather = 10f;
                    initialCoinsGather = 2f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.BASIC:
                    GetComponent<EnemyWeapon>().range = 1.5f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 2f;
                    initialMaxHealth = 150f;
                    initialMeleeDamage = 0.15f;
                    initialAttackSpeed = 1f;
                    initialXpGather = 20f;
                    initialCoinsGather = 4f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.DOUBLE_HEAD:
                    GetComponent<EnemyWeapon>().range = 2f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.8f;
                    initialMaxHealth = 200f;
                    initialMeleeDamage = 0.25f;
                    initialAttackSpeed = 1.2f;
                    initialXpGather = 50f;
                    initialCoinsGather = 8f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.BOLT_ACTION:
                    GetComponent<EnemyWeapon>().range = 6f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.4f;
                    initialMaxHealth = 100f;
                    initialMeleeDamage = 0.1f;
                    initialAttackSpeed = 10f;
                    initialXpGather = 60f;
                    initialCoinsGather = 14f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.SHOTGUN:
                    GetComponent<EnemyWeapon>().range = 1.5f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.7f;
                    initialMaxHealth = 150f;
                    initialMeleeDamage = 0.2f;
                    initialAttackSpeed = 7f;
                    initialXpGather = 60f;
                    initialCoinsGather = 12f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.MACHINE_GUN:
                    GetComponent<EnemyWeapon>().range = 2.5f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.9f;
                    initialMaxHealth = 200f;
                    initialMeleeDamage = 0.15f;
                    initialAttackSpeed = 0.2f;
                    initialXpGather = 100f;
                    initialCoinsGather = 25f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.SLOWER:
                    GetComponent<EnemyWeapon>().range = 2f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 2f;
                    initialMaxHealth = 150f;
                    initialMeleeDamage = 0.05f;
                    initialAttackSpeed = 2f;
                    initialXpGather = 50f;
                    initialCoinsGather = 8f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.KAMIKAZE:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 3f;
                    initialMaxHealth = 10f;
                    initialMeleeDamage = 0f;
                    initialXpGather = 60f;
                    initialCoinsGather = 0f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.TANK:
                    GetComponent<EnemyWeapon>().range = 3f;
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1f;
                    initialMaxHealth = 500f;
                    initialMeleeDamage = 0.3f;
                    initialAttackSpeed = 3f;
                    initialXpGather = 200f;
                    initialCoinsGather = 40f;
                    break;
                case Utility.Gameplay.Enemy.enemyTypes.NINJA:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 2.5f;
                    initialMaxHealth = 200f;
                    initialMeleeDamage = 0.35f;
                    initialXpGather = 80f;
                    initialCoinsGather = 40f;
                    break;
            }
            maxHealth = initialMaxHealth * Utility.Functions.EnemyStats.getEnemyMaxHealth(RuntimeSpecs.mapLevel);
            meleeDamage = initialMeleeDamage * Utility.Functions.EnemyStats.getEnemyMeleeDamageFactor(RuntimeSpecs.mapLevel);
            if (GetComponent<EnemyWeapon>() != null) GetComponent<EnemyWeapon>().damageFactor = Utility.Functions.EnemyStats.getEnemyRangedDamageFactor(RuntimeSpecs.mapLevel);
            if (GetComponent<EnemyWeapon>() != null) GetComponent<EnemyWeapon>().shootingTime = initialAttackSpeed / Utility.Functions.EnemyStats.getEnemyAttackSpeed(RuntimeSpecs.mapLevel);
            xpGather = Mathf.RoundToInt(initialXpGather * Utility.Functions.EnemyStats.getEnemyXpGather(RuntimeSpecs.mapLevel));
            coinGather = Mathf.RoundToInt(initialCoinsGather * Utility.Functions.EnemyStats.getEnemyCoinGather(RuntimeSpecs.mapLevel));
        } else {
            switch (bossType) {
                case Utility.Gameplay.Enemy.bossTypes.BOSS_L1_SCOUT:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 2f;
                    maxHealth = 500f;
                    meleeDamage = 1f;
                    xpGather = 200;
                    coinGather = 80;
                    break;
                case Utility.Gameplay.Enemy.bossTypes.BOSS_L2_BASIC:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.5f;
                    GetComponent<EnemyWeapon>().range = 4f;
                    GetComponent<EnemyWeapon>().shootingTime = 4f;
                    GetComponent<EnemyWeapon>().damageFactor = 1.5f;
                    maxHealth = 2000f;
                    meleeDamage = 2f;
                    xpGather = 350;
                    coinGather = 125;
                    break;
                case Utility.Gameplay.Enemy.bossTypes.BOSS_L3_DOUBLE_HEAD:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.4f;
                    GetComponent<EnemyWeapon>().range = 5f;
                    GetComponent<EnemyWeapon>().shootingTime = 6f;
                    GetComponent<EnemyWeapon>().damageFactor = 2f;
                    maxHealth = 2500f;
                    meleeDamage = 2.5f;
                    xpGather = 500;
                    coinGather = 200;
                    break;
                case Utility.Gameplay.Enemy.bossTypes.BOSS_L4_BOLT_ACTION:
                    GetComponent<EnemyFollowPlayer>().speedFactor = 1.4f;
                    GetComponent<EnemyWeapon>().range = 6f;
                    GetComponent<EnemyWeapon>().shootingTime = 12f;
                    GetComponent<EnemyWeapon>().damageFactor = 2.5f;
                    maxHealth = 2800f;
                    meleeDamage = 1.5f;
                    xpGather = 800;
                    coinGather = 300;
                    break;
            }
        }
    }
}
