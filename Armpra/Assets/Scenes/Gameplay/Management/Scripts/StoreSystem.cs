using System;
using TMPro;
using UnityEngine;

public class StoreSystem : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    [HideInInspector] public const int MAX_COUNTER = 100;
    [HideInInspector] public StoreItem[] upgrades;

    public void Awake() {
        rf = GetComponent<Referencer>();
        upgrades = new StoreItem[rf.storeUpgradeButtons.Length];

    }
    public void Start() {
        for (int i = 0; i < upgrades.Length; i++)
            upgrades[i] = new StoreItem((Constants.Gameplay.Store.storeItem)Enum.GetValues(typeof(Constants.Gameplay.Store.storeItem)).GetValue(i), rf.storeUpgradeButtons[i], rf.storeLevelTexts[i].GetComponent<TextMeshProUGUI>(), rf.cs, MAX_COUNTER);
        //displayStats();
    }

    public void upgradeComponent(Constants.Gameplay.Store.storeItem item) {
        for (int i = 0; i < upgrades.Length; i++) {
            if (upgrades[i].item == item) {
                upgrades[i].upgradeItem();
                rf.storeUpgradeButtons[i] = upgrades[i].button;
                rf.storeLevelTexts[i] = upgrades[i].levelText.gameObject;
            }
        }
    }

    public void forceRefresh() {
        for (int i = 0; i < upgrades.Length; i++) {
            rf.storeUpgradeButtons[i] = upgrades[i].button;
            rf.storeLevelTexts[i] = upgrades[i].levelText.gameObject;
        }
    }

    public StoreItem findStoreItemByType(Constants.Gameplay.Store.storeItem item) {
        foreach (StoreItem storeItem in upgrades) {
            if (storeItem.item == item && (storeItem != null)) {
                return storeItem;
            }
        }
        return null;
    }

    public void displayStats() {
        foreach (StoreItem storeItem in upgrades) {
            Debug.Log(storeItem + "-> " + "item: " + storeItem.item + ", counter: " + storeItem.counter + ", isAmmo: " + storeItem.isAmmo);
        }
    }
}
