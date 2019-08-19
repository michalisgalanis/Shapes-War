using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour {
    //Player Upgrades
    [HideInInspector] public int meleeDamageUpgradeCounter = 1;
    [HideInInspector] public int attackSpeedUpgradeCounter = 1;
    [HideInInspector] public int maxHealthUpgradeCounter = 1;
    [HideInInspector] public int damageReductionUpgradeCounter = 1;
    [HideInInspector] public int movementSpeedUpgradeCounter = 1;

    public Button meleeDamageUpgradeButton;
    public Button attackSpeedUpgradeButton;
    public Button maxHealthUpgradeButton;
    public Button damageReductionUpgradeButton;
    public Button movementSpeedUpgradeButton;

    public TextMeshProUGUI meleeDamageLevelText;
    public TextMeshProUGUI attackSpeedLevelText;
    public TextMeshProUGUI maxHealthLevelText;
    public TextMeshProUGUI damageReductionLevelText;
    public TextMeshProUGUI movementSpeedLevelText;

    //Bullet Upgrades
    [HideInInspector] public int normalBulletAmmo = 999999;
    [HideInInspector] public int highVelocityBulletAmmo = 0;
    [HideInInspector] public int explosiveBulletAmmo = 0;
    [HideInInspector] public int poisonousBulletAmmo = 0;
    [HideInInspector] public int bulletSpeedUpgradeCounter = 1;
    [HideInInspector] public int bulletEffectUpgradeCounter = 1;

    public Button highVelocityAmmoButton;
    public Button explosiveAmmoButton;
    public Button poisonousAmmoButton;
    public Button bulletSpeedUpgradeButton;
    public Button bulletEffectUpgradeButton;

    public TextMeshProUGUI highVelocityAmmoText;
    public TextMeshProUGUI explosiveAmmoText;
    public TextMeshProUGUI poisonousAmmoText;
    public TextMeshProUGUI bulletSpeedLevelText;
    public TextMeshProUGUI bulletEffectLevelText;

    //Powerups Upgrades
    [HideInInspector] public int powerupEffectCounter = 100;
    [HideInInspector] public int powerupDurationCounter = 1;
    [HideInInspector] public int powerupSpawnFrequencyCounter = 100;

    public Button powerupEffectButton;
    public Button powerupDurationButton;
    public Button powerupSpawnFrequencyButton;

    public TextMeshProUGUI powerupEffectLevelText;
    public TextMeshProUGUI powerupDurationLevelText;
    public TextMeshProUGUI powerupSpawnFrequencyLevelText;

    //Needed Objects
    private Button tempButton;
    private TextMeshProUGUI tempLevelText;
    private int tempCounter;
    private const int MAX_COUNTER = 100;
    private CoinSystem cs;

    private void Start() {
        cs = GetComponent<CoinSystem>();
        meleeDamageUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(meleeDamageUpgradeCounter).ToString();
        attackSpeedUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(attackSpeedUpgradeCounter).ToString();
        maxHealthUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(maxHealthUpgradeCounter).ToString();
        damageReductionUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(damageReductionUpgradeCounter).ToString();
        movementSpeedUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(movementSpeedUpgradeCounter).ToString();
        bulletSpeedUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(bulletSpeedUpgradeCounter).ToString();
        bulletEffectUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(bulletEffectUpgradeCounter).ToString();
        powerupEffectButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(powerupEffectCounter).ToString();
        powerupDurationButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(powerupDurationCounter).ToString();
        powerupSpawnFrequencyButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(powerupSpawnFrequencyCounter).ToString();
    }

    public void RefreshOnStoreEnter() {
        highVelocityAmmoText.text = "Ammo: " + highVelocityBulletAmmo;
        explosiveAmmoText.text = "Ammo: " + explosiveBulletAmmo;
        poisonousAmmoText.text = "Ammo: " + poisonousBulletAmmo;
    }

    private void Update() {
        normalBulletAmmo = 999999;
    }

    //Player Upgrades Functions
    public void UpgradeAttackSpeed() {
        tempButton = attackSpeedUpgradeButton; tempCounter = attackSpeedUpgradeCounter; tempLevelText = attackSpeedLevelText;
        UpgradeComponent();
        attackSpeedUpgradeButton = tempButton; attackSpeedUpgradeCounter = tempCounter; attackSpeedLevelText = tempLevelText;
    }

    public void UpgradeMeleeDamage() {
        tempButton = meleeDamageUpgradeButton; tempCounter = meleeDamageUpgradeCounter; tempLevelText = meleeDamageLevelText;
        UpgradeComponent();
        meleeDamageUpgradeButton = tempButton; meleeDamageUpgradeCounter = tempCounter; meleeDamageLevelText = tempLevelText;
    }

    public void UpgradeMaxHealth() {
        tempButton = maxHealthUpgradeButton; tempCounter = maxHealthUpgradeCounter; tempLevelText = maxHealthLevelText;
        UpgradeComponent();
        maxHealthUpgradeButton = tempButton; maxHealthUpgradeCounter = tempCounter; maxHealthLevelText = tempLevelText;
    }

    public void UpgradeDamageReduction() {
        tempButton = damageReductionUpgradeButton; tempCounter = damageReductionUpgradeCounter; tempLevelText = damageReductionLevelText;
        UpgradeComponent();
        damageReductionUpgradeButton = tempButton; damageReductionUpgradeCounter = tempCounter; damageReductionLevelText = tempLevelText;
    }

    public void UpgradeMovementSpeed() {
        tempButton = movementSpeedUpgradeButton; tempCounter = movementSpeedUpgradeCounter; tempLevelText = movementSpeedLevelText;
        UpgradeComponent();
        movementSpeedUpgradeButton = tempButton; movementSpeedUpgradeCounter = tempCounter; movementSpeedLevelText = tempLevelText;
    }

    //Bullet Upgrades Functions

    public void AddHighVelocityBulletAmmo() {
        tempButton = highVelocityAmmoButton; tempCounter = highVelocityBulletAmmo; tempLevelText = highVelocityAmmoText;
        AddAmmo();
        highVelocityAmmoButton = tempButton; highVelocityBulletAmmo = tempCounter; highVelocityAmmoText = tempLevelText;
    }

    public void AddExplosiveBulletAmmo() {
        tempButton = explosiveAmmoButton; tempCounter = explosiveBulletAmmo; tempLevelText = explosiveAmmoText;
        AddAmmo();
        explosiveAmmoButton = tempButton; explosiveBulletAmmo = tempCounter; explosiveAmmoText = tempLevelText;
    }

    public void AddPoisonousBulletAmmo() {
        tempButton = poisonousAmmoButton; tempCounter = poisonousBulletAmmo; tempLevelText = poisonousAmmoText;
        AddAmmo();
        poisonousAmmoButton = tempButton; poisonousBulletAmmo = tempCounter; poisonousAmmoText = tempLevelText;
    }

    public void UpgradeBulletSpeed() {
        tempButton = bulletSpeedUpgradeButton; tempCounter = bulletSpeedUpgradeCounter; tempLevelText = bulletSpeedLevelText;
        UpgradeComponent();
        bulletSpeedUpgradeButton = tempButton; bulletSpeedUpgradeCounter = tempCounter; bulletSpeedLevelText = tempLevelText;
    }

    public void UpgradeBulletEffect() {
        tempButton = bulletEffectUpgradeButton; tempCounter = bulletEffectUpgradeCounter; tempLevelText = bulletEffectLevelText;
        UpgradeComponent();
        bulletEffectUpgradeButton = tempButton; bulletEffectUpgradeCounter = tempCounter; bulletEffectLevelText = tempLevelText;
    }

    //Powerups Upgrades Functions
    public void UpgradePowerupEffect() {
        tempButton = powerupEffectButton; tempCounter = powerupEffectCounter; tempLevelText = powerupEffectLevelText;
        UpgradeComponent();
        powerupEffectButton = tempButton; powerupEffectCounter = tempCounter; powerupEffectLevelText = tempLevelText;
    }

    public void UpgradePowerupDuration() {
        tempButton = powerupDurationButton; tempCounter = powerupDurationCounter; tempLevelText = powerupDurationLevelText;
        UpgradeComponent();
        powerupDurationButton = tempButton; powerupDurationCounter = tempCounter; powerupDurationLevelText = tempLevelText;
    }

    public void UpgradePowerupSpawnFrequency() {
        tempButton = powerupSpawnFrequencyButton; tempCounter = powerupSpawnFrequencyCounter; tempLevelText = powerupSpawnFrequencyLevelText;
        UpgradeComponent();
        powerupSpawnFrequencyButton = tempButton; powerupSpawnFrequencyCounter = tempCounter; powerupSpawnFrequencyLevelText = tempLevelText;
    }


    //General Upgrades Functions
    private void UpgradeComponent() {
        TextMeshProUGUI text = tempButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        if (cs.canRemoveCoins(int.Parse(text.text))) {
            cs.removeCoins(int.Parse(text.text));
            tempCounter++;
            tempLevelText.text = "Level: " + tempCounter;
        }
        if (tempCounter >= MAX_COUNTER) { //if is maxed out{
            tempButton.interactable = false;
            //Make Button Image Red, with MAX text
            Color imageColor = tempButton.transform.GetChild(1).GetComponent<Image>().color;
            Color.RGBToHSV(imageColor, out float hColor, out float sColor, out float vColor);
            tempButton.transform.GetChild(1).GetComponent<Image>().color = Color.HSVToRGB(0, sColor, vColor);
            tempButton.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "MAX"; //button cost
        } else {
            text.text = estimateCost(tempCounter).ToString();
        }
    }

    private void AddAmmo() {
        TextMeshProUGUI text = tempButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        if (cs.canRemoveCoins(int.Parse(text.text))) {
            cs.removeCoins(int.Parse(text.text));
            tempCounter++;
            tempLevelText.text = "Ammo: " + tempCounter;
        }
    }

    private int estimateCost(int counter) {
        return Mathf.RoundToInt(5 + 4 * Mathf.Pow(counter, 1.22f));
    }

}
