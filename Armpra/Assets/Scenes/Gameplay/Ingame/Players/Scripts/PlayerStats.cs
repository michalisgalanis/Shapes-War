using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //General Stats
    public int playerLevel;                     //input from PlayerExperience
    public double XP;                           //input from PlayerExperience

    //Attack Stats
    public float meleeDamage;                   //generated here
    public float attackSpeed;                   //generated here

    //Defense Stats
    public float maxHealth;                     //generated here
    public float damageReduction;               //generated here - 0 equals to full damage taken, 1 equals to zero damage taken

    //Neutral Stats
    public float movementSpeed;                 //generatedHere

    //Other Essential Real Time Stats
    private float currentHealth;
    private bool markedForDestruction;

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
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(13, 14);
        GameObject shockwave = Instantiate(shockwavePrefab, transform.localPosition, Quaternion.identity);
        shockwave.transform.parent = gameObject.transform;
        playerHeads = new List<SpriteRenderer>();
        Transform headSystem = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
        for (int i = 0; i < headSystem.childCount; i++)
           playerHeads.Add(headSystem.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
    }
    void Update(){
        EstimateAttackStats();
        EstimateDefenceStats();
        EstimateNeutralStats();
    }

    void UpdateOnLevel()
    {

    }

    void EstimateAttackStats()
    {
        //Estimating Attack Speed
        
        attackSpeed = (0.8f + 0.1f * Mathf.Pow(playerLevel, 1.1f + attackSpeedUpgrades)) * (1 + attackSpeedPowerupFactor);
        GetComponent<Weapon>().shootingTime = attackSpeed;

        //Estimating Melee Damage
        float meleeDamageUpgrades = 0f;          //input from Store System
        float meleeDamagePowerupFactor = 1f;     //input from Powerup System
        float meleeDamageEnemyPenalty = 0f;      //input from Enemy
        meleeDamage = (0.05f * Mathf.Pow(playerLevel, 0.98f + meleeDamageUpgrades) * (1 + meleeDamagePowerupFactor) * (1 - meleeDamageEnemyPenalty));
    }
    void EstimateDefenceStats()
    {
        //Estimating Max Health
        float maxHealthUpgrades = 0f;               //input from Store System
        float maxHealthPowerupFactor = 1f;          //input from Powerup System
        maxHealth = (70f + 30f * Mathf.Pow(playerLevel, 1.5f + maxHealthUpgrades)) * (1 + maxHealthPowerupFactor);

        float h, s, v;
        Color.RGBToHSV(playerBorder.color, out h, out s, out v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        playerBorder.color = (Color.HSVToRGB(h, s, v));
        foreach (SpriteRenderer playerHead in playerHeads)
            playerHead.color = (Color.HSVToRGB(h, s, v));

        //Estimating Damage Reduction
        float damageReductionUpgrades = 0f;                //input from Store System
        float damageReductionPowerupFactor = 1f;           //input from Powerup System
        damageReduction = (Mathf.Sqrt(Mathf.Pow(playerLevel, 0.2f + damageReductionUpgrades)) - 1f) * (1 + damageReductionPowerupFactor);
    }
    void EstimateNeutralStats(){
        //Estimating Movement Speed
        float movementSpeedUpgrades = 0f;        //input from Store System
        float movementSpeedPowerupFactor = 1f;   //input from Powerup System
        float movementSpeedEnemyPenalty = 0f;    //input from Enemy
        movementSpeed = ((2.5f + Mathf.Pow(playerLevel, 0.099f + movementSpeedUpgrades)) * (1 + movementSpeedPowerupFactor) * (1 - movementSpeedEnemyPenalty));
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
}
