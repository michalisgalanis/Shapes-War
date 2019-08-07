using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //General Stats
    public int playerLevel;                     //input from PlayerExperience
    public double XP;                           //input from PlayerExperience

    //Stats
    public float attackSpeed;                   //generated here
    public float meleeDamage;                   //generated here
    public float maxHealth;                     //generated here
    public float damageReduction;               //generated here - 0 equals to full damage taken, 1 equals to zero damage taken
    public float movementSpeed;                 //generated here

    //Store Upgrades
    float attackSpeedUpgrades = 0f;
    float meleeDamageUpgrades = 0f;
    float maxHealthUpgrades = 0f;
    float damageReductionUpgrades = 0f;
    float movementSpeedUpgrades = 0f;

    //Other Essential Real Time Stats
    private float currentHealth;
    private bool markedForDestruction;
    public bool storeRefresh;

    //Needed References
    private GameObject gameManager;
    public SpriteRenderer playerBorder;
    private List<SpriteRenderer> playerHeads;
    public GameObject shockwavePrefab;
    public ParticleSystem playerDeathExplosionParticles;

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        currentHealth = maxHealth;
        markedForDestruction = false;
        storeRefresh = false;
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(13, 14);
        GameObject shockwave = Instantiate(shockwavePrefab, transform.localPosition, Quaternion.identity);
        shockwave.transform.parent = gameObject.transform;
        playerHeads = new List<SpriteRenderer>();
        Transform headSystem = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        for (int i = 0; i < headSystem.childCount; i++)
            playerHeads.Add(headSystem.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
    }
    void Update() {
        EstimateStats();

        Color.RGBToHSV(playerBorder.color, out float h, out float s, out float v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        playerBorder.color = (Color.HSVToRGB(h, s, v));
        foreach (SpriteRenderer playerHead in playerHeads)
            playerHead.color = (Color.HSVToRGB(h, s, v));
    }

    public void UpdateEveryLevel()
    {
        attackSpeedUpgrades = 0f;               //input from StoreSystem
        meleeDamageUpgrades = 0f;               //input from StoreSystem
        maxHealthUpgrades = 0f;                 //input from StoreSystem
        damageReductionUpgrades = 0f;           //input from StoreSystem
        movementSpeedUpgrades = 0f;             //input from StoreSystem
    }

    void EstimateStats() {
        //Estimating Attack Speed
        float attackSpeedPowerupFactor = 0f;
        attackSpeed = (0.8f + 0.1f * Mathf.Pow(playerLevel, 1.1f * (1 + attackSpeedUpgrades))) * (1 + attackSpeedPowerupFactor);
        GetComponent<Weapon>().shootingTime = attackSpeed;

        //Estimating Melee Damage
        float meleeDamagePowerupFactor = 0f, meleeDamageEnemyPenalty = 0f;
        meleeDamage = 0.05f * Mathf.Pow(playerLevel, 0.98f * (1 + meleeDamageUpgrades)) * (1 + meleeDamagePowerupFactor) * (1 - meleeDamageEnemyPenalty);

        //Estimating Max Health
        float maxHealthPowerupFactor = 0f;
        maxHealth = (70f + 30f * Mathf.Pow(playerLevel, 1.5f * (1 + maxHealthUpgrades))) * (1 + maxHealthPowerupFactor);

        //Estimating Damage Reduction
        float damageReductionPowerupFactor = 0f;
        damageReduction = (Mathf.Sqrt(Mathf.Pow(playerLevel, 0.2f * (1 + damageReductionUpgrades))) - 1f) * (1 + damageReductionPowerupFactor);

        //Estimating Movement Speed
        float movementSpeedPowerupFactor = 0f, movementSpeedEnemyPenalty = 0f;
        movementSpeed = (2.5f + Mathf.Pow(playerLevel, 0.099f * (1 + movementSpeedUpgrades))) * (1 + movementSpeedPowerupFactor) * (1 - movementSpeedEnemyPenalty);
        GetComponent<PlayerMovement>().velocityFactor = movementSpeed;
    }

    public void TakeDamage(float damage){
        currentHealth -= damage * (1 - damageReduction);
        if (currentHealth <= 0 && !markedForDestruction)
        {
            markedForDestruction = true;
            playerDeathExplosionParticles = Instantiate(playerDeathExplosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.GetComponent<GameplayManager>().Lose();
            markedForDestruction = false;
        }
    }

    public void RefillStats(){
        currentHealth = maxHealth;
    }

    void OnTriggerStay2D(Collider2D hitInfo){
        if (hitInfo.CompareTag("Enemy")){
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(meleeDamage);
        }
    }
}
