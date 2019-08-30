using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem {
    public Constants.Gameplay.Store.storeItem item;
    public readonly bool isAmmo;

    public Button button;
    public TextMeshProUGUI levelText;
    public int counter=1;
    private int cost;

    private readonly CoinSystem cs;
    private readonly int MAX_COUNTER;

    public StoreItem(Constants.Gameplay.Store.storeItem item, Button button, TextMeshProUGUI levelText, CoinSystem cs, int MAX_COUNTER) {
        this.item = item;
        this.button = button;
        this.levelText = levelText;
        this.cs = cs;
        this.MAX_COUNTER = MAX_COUNTER;
        this.counter = 1;
        isAmmo = (this.item.Equals(Constants.Gameplay.Store.storeItem.NORMAL) || this.item.Equals(Constants.Gameplay.Store.storeItem.HV) || this.item.Equals(Constants.Gameplay.Store.storeItem.EXPLOSIVE) || this.item.Equals(Constants.Gameplay.Store.storeItem.POISONOUS));
        estimateCost();
    }

    public void upgradeItem() {
        if (cs.canSpendCoins(cost)) {
            cs.spendCoins(cost);
            counter++;
            levelText.text = ((isAmmo) ? "Ammo: " : "Level: ") + counter;
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
