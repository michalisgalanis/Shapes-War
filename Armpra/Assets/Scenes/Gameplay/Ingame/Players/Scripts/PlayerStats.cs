using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //General Stats
    public int playerLevel;
    public double xP;

    //Attack Stats
    public float rangedDamage;
    public float meleeDamage;
    public float attackSpeed;

    //Defense Stats
    public int maxHealth;
    public float damageReduction; //0 equals to full damage taken, 1 equals to zero damage taken

    //Neutral Stats
    public float movementSpeed;

    //Other Essential Real Time Stats
    private float currentHealth;
    private bool markedForDestruction;

    //Needed References
    public SpriteRenderer playerBorder;
    public SpriteRenderer[] playerHeads;
    public GameObject shockwavePrefab;
    //public GameObject shieldPrefab;
    public ParticleSystem playerDeathExplosionParticles;
    public GameObject gm;
    void Start()
    {
        currentHealth = maxHealth;
        markedForDestruction = false;
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(13, 14);
        gm = GameObject.FindGameObjectWithTag("GameController");
        GameObject shockwave = Instantiate(shockwavePrefab, transform.localPosition, Quaternion.identity);
        shockwave.transform.parent = gameObject.transform;
        //Instantiate(shieldPrefab, transform.localPosition, Quaternion.identity).transform.parent = gameObject.transform;
        
    }

    // Update is called once per frame
    void Update()
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
            gm.GetComponent<GameplayManager>().Lose();
            markedForDestruction = false;
        }
    }

    public void RefillStats()
    {
        currentHealth = maxHealth;
       
    }
}
