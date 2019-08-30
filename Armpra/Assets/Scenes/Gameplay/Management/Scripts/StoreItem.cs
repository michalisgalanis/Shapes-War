using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem {
    //References
    private Referencer rf;

    //Setup Variables
    public TextMeshProUGUI levelText;
    public Button button;

    //Runtime Variables
    public Constants.Gameplay.Store.storeItem item;
    public readonly bool isAmmo;
    public int counter;
    private int cost;
    private readonly int MAX_COUNTER;

    public StoreItem(Constants.Gameplay.Store.storeItem item, Button button, TextMeshProUGUI levelText,  int MAX_COUNTER) {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        this.item = item;
        this.button = button;
        this.levelText = levelText;
        this.MAX_COUNTER = MAX_COUNTER;
        this.counter = 1;
        isAmmo = (this.item.Equals(Constants.Gameplay.Store.storeItem.NORMAL) || this.item.Equals(Constants.Gameplay.Store.storeItem.HV) || this.item.Equals(Constants.Gameplay.Store.storeItem.EXPLOSIVE) || this.item.Equals(Constants.Gameplay.Store.storeItem.POISONOUS));
        estimateCost();
    }

    public void upgradeItem() {
        if (rf.cs.canSpendCoins(cost)) {
            rf.cs.spendCoins(cost);
            counter++;
            levelText.text = ((isAmmo) ? "Ammo: " : "Level ") + counter;
            button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = cost.ToString();
        }
        if (!isAmmo && counter >= MAX_COUNTER) {
            button.interactable = false;
            //Make Button Image Red, with MAX text
            Color imageColor = button.transform.GetChild(1).GetComponent<Image>().color;
            Color.RGBToHSV(imageColor, out float hColor, out float sColor, out float vColor);
            button.transform.GetChild(1).GetComponent<Image>().color = Color.HSVToRGB(0, sColor, vColor);
            button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "MAX"; //button cost
        } else {
            estimateCost();
        }
        rf.cs.FixedUpdate();
        if (!isAmmo) rf.ps.EstimateStats();
    }

    private void estimateCost() {
        if (isAmmo) {
            switch (item) {
                case Constants.Gameplay.Store.storeItem.NORMAL:
                cost = 0;
                break;
                case Constants.Gameplay.Store.storeItem.HV:
                cost = 10;
                break;
                case Constants.Gameplay.Store.storeItem.EXPLOSIVE:
                cost = 25;
                break;
                case Constants.Gameplay.Store.storeItem.POISONOUS:
                cost = 25;
                break;
            }
        } else {
            cost = Mathf.RoundToInt(5 + 4 * Mathf.Pow(counter, 1.22f));
        }
    }
}
