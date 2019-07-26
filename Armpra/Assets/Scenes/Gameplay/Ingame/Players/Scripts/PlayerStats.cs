using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int playerLevel;
    public double XP;
    public Weapon weapon;
    public float weaponDamage;
    public int maxHealth;
    public int attackSpeed;
    public float movementSpeed;
    public float damageReduction; //0 equals to full damage taken, 1 equals to zero damage taken

    private float currentHealth;
    private bool markedForDestruction;


    public SpriteRenderer playerBorder;
    public SpriteRenderer[] playerHeads;

    public ParticleSystem playerDeathExplosionParticles;
    public GameObject gm;
    void Start()
    {
        currentHealth = maxHealth;
        markedForDestruction = false;
        Physics2D.IgnoreLayerCollision(8, 13);
        gm = GameObject.FindGameObjectWithTag("GameController");
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
        }
    }
}
