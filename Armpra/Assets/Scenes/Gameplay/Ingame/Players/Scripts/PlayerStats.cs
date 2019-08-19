using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //General Stats
    public int playerLevel;                     //input from PlayerExperience
    public double XP;                           //input from PlayerExperience

    //Stats
    public float attackSpeed;                   //generated here
    public float meleeDamage;                   //generated here
    public float maxHealth;                     //generated here
    public float damageReduction;               //generated here - 0 equals to full damage taken, 1 equals to zero damage taken
    public float movementSpeed;                 //generated here

    private const float UPGRADES_FACTOR = 0.001f;

    //Other Essential Real Time Stats
    public float currentHealth;
    private bool markedForDestruction;

    //Needed References
    private GameplayManager gm;
    private StoreSystem ss;
    private GameObject[] powerups;

    private List<SpriteRenderer> playerHeads;
    public SpriteRenderer playerBorder;
    public GameObject shockwavePrefab;
    public ParticleSystem playerDeathExplosionParticles;

    private void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>();
        ss = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoreSystem>();
        powerups = GameObject.FindGameObjectsWithTag("Powerups");
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(13, 14);
        markedForDestruction = false;
        GameObject shockwave = Instantiate(shockwavePrefab, transform.localPosition, Quaternion.identity);
        shockwave.transform.parent = gameObject.transform;
        playerHeads = new List<SpriteRenderer>();
        Transform headSystem = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        for (int i = 0; i < headSystem.childCount; i++) {
            playerHeads.Add(headSystem.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
        }
    }

    private void Update() {
        EstimateStats();

        Color.RGBToHSV(playerBorder.color, out float h, out float s, out float v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        playerBorder.color = (Color.HSVToRGB(h, s, v));
        foreach (SpriteRenderer playerHead in playerHeads) {
            playerHead.color = (Color.HSVToRGB(h, s, v));
        }
    }

    public void ForceUpdate() {
        Update();
    }

    public void EstimateStats() {
        //Estimating Attack Speed
        float attackSpeedPowerupFactor = GetPowerupMultiplier(EffectOverTime.PowerupType.AttackSpeed);
        attackSpeed = (1f - 0.1f * Mathf.Pow(playerLevel, 0.4f * (1 + ss.attackSpeedUpgradeCounter * UPGRADES_FACTOR))) * (1 - attackSpeedPowerupFactor);
        GetComponent<Weapon>().shootingTime = attackSpeed;

        //Estimating Melee Damage
        float meleeDamagePowerupFactor = GetPowerupMultiplier(EffectOverTime.PowerupType.MeleeDamage), meleeDamageEnemyPenalty = 0f;
        meleeDamage = 0.05f * Mathf.Pow(playerLevel, 0.98f * (1 + ss.meleeDamageUpgradeCounter * UPGRADES_FACTOR)) * (1 + meleeDamagePowerupFactor) * (1 - meleeDamageEnemyPenalty);

        //Estimating Max Health
        maxHealth = (70f + 30f * Mathf.Pow(playerLevel, 1.5f * (1 + ss.maxHealthUpgradeCounter * UPGRADES_FACTOR)));

        //Estimating Damage Reduction
        float damageReductionPowerupFactor = GetPowerupMultiplier(EffectOverTime.PowerupType.Immunity);
        damageReduction = (Mathf.Sqrt(Mathf.Pow(playerLevel, 0.15f * (1 + ss.damageReductionUpgradeCounter * UPGRADES_FACTOR))) - 1f) * (1 + damageReductionPowerupFactor);

        //Estimating Movement Speed
        float movementSpeedPowerupFactor = GetPowerupMultiplier(EffectOverTime.PowerupType.MovementSpeed), movementSpeedEnemyPenalty = 0f;
        movementSpeed = (2.5f + Mathf.Pow(playerLevel, 0.099f * (1 + ss.movementSpeedUpgradeCounter * UPGRADES_FACTOR))) * (1 + movementSpeedPowerupFactor) * (1 - movementSpeedEnemyPenalty);
        GetComponent<PlayerMovement>().velocityFactor = movementSpeed;
        
        //Debug.Log("Att.Sp: " + attackSpeed + " | Mel.Dm: " + meleeDamage + " | Curr.Hlth/Max.Hlth: " + currentHealth + "/" + maxHealth + " | Dm.Red: " + damageReduction + " | Mov.Sp: " + movementSpeed);
        //Debug.Log("Att.Sp.Fact: " + attackSpeedPowerupFactor + " | Mel.Dm.Fact: " + meleeDamagePowerupFactor + " | Dm.Rd.Fact: " + damageReductionPowerupFactor + " | Mov.Sp.Fact: " + movementSpeedPowerupFactor);
    }

    public void TakeDamage(float damage) {
        float realDamage = damage * (1 - damageReduction);
        currentHealth -= realDamage;
        if (currentHealth <= 0 && !markedForDestruction) {
            markedForDestruction = true;
            playerDeathExplosionParticles = Instantiate(playerDeathExplosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gm.Lose();
        }
    }

    public void RefillStats() {
        currentHealth = maxHealth;
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("Enemy")) {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(meleeDamage);
            }
        }
    }

    public void InstantHeal(float percentageMaxHeal) {
        currentHealth = Mathf.Clamp(currentHealth + percentageMaxHeal * maxHealth, 0, maxHealth);
    }

    private float GetPowerupMultiplier(EffectOverTime.PowerupType powerupType) {
        foreach (GameObject powerup in powerups) {
            if (powerup.GetComponent<EffectOverTime>() != null && powerup.GetComponent<EffectOverTime>().typeSelected == powerupType) {
                return powerup.GetComponent<EffectOverTime>().powerupMultiplier;
            }
        }

        return 0f;
    }
}
