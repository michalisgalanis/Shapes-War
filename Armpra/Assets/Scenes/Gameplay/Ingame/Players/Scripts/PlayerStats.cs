using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //General Stats
    public int playerLevel;                     //input from PlayerExperience
    public double XP;                           //input from PlayerExperience
    public float movementSpeed;                 //input from PlayerMovement

    //Attack Stats
    public float rangedDamage;                  //generated here
    public float meleeDamage;                   //generated here
    public float attackSpeed;                   //generated here

    //Defense Stats
    public int maxHealth;                       //generated here
    public float damageReduction;               //generated here - 0 equals to full damage taken, 1 equals to zero damage taken
    
    //Other Essential Real Time Stats
    private float currentHealth;
    private bool markedForDestruction;

    //Needed References
    private GameObject gameManager;
    public SpriteRenderer playerBorder;
    private List<SpriteRenderer> playerHeads;
    public GameObject shockwavePrefab;
    public ParticleSystem playerDeathExplosionParticles;
    


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        currentHealth = maxHealth;
        markedForDestruction = false;
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(13, 14);
        GameObject shockwave = Instantiate(shockwavePrefab, transform.localPosition, Quaternion.identity);
        shockwave.transform.parent = gameObject.transform;
        playerHeads = new List<SpriteRenderer>();
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).childCount; i++)
           playerHeads.Add(GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>());
    }
    void Update()
    {
        EstimateGeneralStats();
        EstimateAttackStats();
        EstimateDefenceStats();

    }

    void EstimateGeneralStats()
    {

    }

    void EstimateAttackStats()
    {

    }

    void EstimateDefenceStats()
    {
        float h, s, v;
        Color.RGBToHSV(playerBorder.color, out h, out s, out v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        playerBorder.color = (Color.HSVToRGB(h, s, v));
        foreach (SpriteRenderer playerHead in playerHeads)
        {
            playerHead.color = (Color.HSVToRGB(h, s, v));
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (damage - damage * damageReduction);
        if (currentHealth <= 0 && !markedForDestruction)
        {
            markedForDestruction = true;
            playerDeathExplosionParticles = Instantiate(playerDeathExplosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.GetComponent<GameplayManager>().Lose();
            markedForDestruction = false;
        }
    }

    public void RefillStats()
    {
        currentHealth = maxHealth;
       
    }
}
