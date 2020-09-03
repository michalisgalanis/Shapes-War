using System;
using UnityEngine;

public class StoreSystem : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    [HideInInspector] public StoreItem[] upgrades;

    void Awake() {
        rf = GetComponent<Referencer>();
        upgrades = new StoreItem[rf.storeUpgrades.Length];
    }
    void Start() {
        for (int i = 0; i < upgrades.Length; i++) {
            upgrades[i] = new StoreItem((Utility.Gameplay.Store.storeItem)Enum.GetValues(typeof(Utility.Gameplay.Store.storeItem)).GetValue(i), rf.storeUpgrades[i]);
        }
        //displayStats();
    }

    public void upgradeComponent(Utility.Gameplay.Store.storeItem item) {
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        Utility.Functions.GeneralFunctions.Vibrate(Utility.Gameplay.Vibrations.VIBRATION_BUTTON_PRESS);
        for (int i = 0; i < upgrades.Length; i++) {
            if (upgrades[i].item == item) {
                upgrades[i].upgradeItem();
                rf.storeUpgrades[i] = upgrades[i].storeUpgradeUI;
                SavingSystem.getInstance().Save(Utility.Data.dataTypes.STORE_DATA);
            }
        }
    }

    public void upgradeComponent(string stringItem) {
        Enum.TryParse(stringItem, out Utility.Gameplay.Store.storeItem item);
        upgradeComponent(item);
    }

    public void forceRefresh() {
        for (int i = 0; i < upgrades.Length; i++) {
            upgrades[i].refreshItem();
            rf.storeUpgrades[i] = upgrades[i].storeUpgradeUI;
        }
    }

    public StoreItem findStoreItemByType(Utility.Gameplay.Store.storeItem item) {
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
