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

    public Weapon weapon;
    public float weaponDamage;
    public double XP;
    public int playerLevel;


    public SpriteRenderer playerBorder;
    public SpriteRenderer playerHead;

    public ParticleSystem playerDeathExplosionParticlesPrefab;
    private ParticleSystem playerDeathExplosionParticles;
    public GameObject lostMenu;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeathExplosionParticles && !playerDeathExplosionParticles.IsAlive())
            Destroy(playerDeathExplosionParticles);
        float h, s, v;
        Color.RGBToHSV(playerBorder.color, out h, out s, out v);
        h = 0f; v = 1f; s = 1f - currentHealth / maxHealth;
        playerBorder.color = (Color.HSVToRGB(h, s, v));
        playerHead.color = (Color.HSVToRGB(h, s, v));
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (damage - damage * damageReduction);
        if (currentHealth <= 0)
        {
            playerDeathExplosionParticles = Instantiate(playerDeathExplosionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            lostMenu.SetActive(true);
        }
    }
}
