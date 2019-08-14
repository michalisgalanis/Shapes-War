using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour {
    private const int MAX_COUNTER = 100;
    private CoinSystem cs;

    //Player Upgrades
    public int meleeDamageUpgradeCounter = 1;
    public int attackSpeedUpgradeCounter = 1;
    public int maxHealthUpgradeCounter = 1;
    public int damageReductionUpgradeCounter = 1;
    public int movementSpeedUpgradeCounter = 1;

    //Powerups Upgrades
    public int powerupEffectCounter = 100;
    public int powerupDurationCounter = 1;
    public int powerupSpawnFrequencyCounter = 100;

    //Needed Objects
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

    private Button tempButton;
    private TextMeshProUGUI tempLevelText;
    private int tempCounter;

    private void Start() {
        cs = GetComponent<CoinSystem>();
        meleeDamageUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(meleeDamageUpgradeCounter).ToString();
        attackSpeedUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(attackSpeedUpgradeCounter).ToString();
        maxHealthUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(maxHealthUpgradeCounter).ToString();
        damageReductionUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(damageReductionUpgradeCounter).ToString();
        movementSpeedUpgradeButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = estimateCost(movementSpeedUpgradeCounter).ToString();
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


    private void UpgradeComponent() {
        TextMeshProUGUI text = tempButton.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        if (cs.canRemoveCoins(int.Parse(text.text))) {
            cs.removeCoins(int.Parse(text.text));
            tempCounter++;
            tempLevelText.text = "Level " + tempCounter;
        }
        if (tempCounter >= MAX_COUNTER) { //if is maxed out{
            tempButton.gameObject.SetActive(false);
            tempLevelText.text = "MAXED OUT";
            /*tempButton.interactable = false;
            tempButton.transform.GetChild(1).gameObject.SetActive(false);//button image
            tempButton.transform.GetChild(2).gameObject.SetActive(false); //button cost
            tempButton.transform.GetChild(3).gameObject.SetActive(true);//maxed out text*/
        } else {
            text.text = estimateCost(tempCounter).ToString();
        }
    }

    private int estimateCost(int counter) {
        return Mathf.RoundToInt(5 + 4 * Mathf.Pow(counter, 1.22f));
    }

}
