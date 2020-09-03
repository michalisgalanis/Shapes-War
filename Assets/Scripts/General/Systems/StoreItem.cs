using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem {
    //References
    private readonly Referencer rf;

    //Setup Variables
    public GameObject storeUpgradeUI;
    public Utility.Gameplay.Store.storeItem item;
    public readonly bool isAmmo = false;
    public readonly bool isSkin = false;

    //Runtime Variables
    public int counter = 1;
    public int cost = 0;
    public bool locked = true;
    public bool selected = false;
    public int unlockableAt;

    public StoreItem(Utility.Gameplay.Store.storeItem item, GameObject storeUpgradeUI) {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        this.item = item;
        this.storeUpgradeUI = storeUpgradeUI;
        if (this.item.Equals(Utility.Gameplay.Store.storeItem.HV) || this.item.Equals(Utility.Gameplay.Store.storeItem.EXPLOSIVE) || this.item.Equals(Utility.Gameplay.Store.storeItem.POISONOUS) || this.item.Equals(Utility.Gameplay.Store.storeItem.SNOW) || this.item.Equals(Utility.Gameplay.Store.storeItem.PENETRATION) || this.item.Equals(Utility.Gameplay.Store.storeItem.ELECTRICITY)) {
            isAmmo = true;
            counter = 0;
        } else if (this.item.Equals(Utility.Gameplay.Store.storeItem.NORMAL)) {
            isAmmo = true;
            counter = 1;
        } else if (this.item.Equals(Utility.Gameplay.Store.storeItem.BOX_3D_SKIN) || this.item.Equals(Utility.Gameplay.Store.storeItem.CIRCLES_SKIN) || this.item.Equals(Utility.Gameplay.Store.storeItem.DOTS_SKIN)) {
            isSkin = true;
            counter = 0;
        }
        estimateCost();
        estimateUnlockLevel();
        refreshItem();
    }

    public void upgradeItem() {
        if (CoinSystem.CanSpendCoins(cost)) {
            rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
            CoinSystem.SpendCoins(cost);
            if (!isAmmo) {
                counter++;
                RuntimeSpecs.itemUpgradesMade++;
            }
            else counter += rf.wp.firepoints.Count;
            refreshItem();
            CoinSystem.RefreshCoins();
        }
        if (!isAmmo  && !isSkin && !locked)
            rf.ps.EstimateStats();
    }

    public void refreshItem() {
        if (storeUpgradeUI == null) return;
        storeUpgradeUI.transform.GetChild(0).gameObject.SetActive(!locked);
        storeUpgradeUI.transform.GetChild(1).gameObject.SetActive(locked);
        AnimationManager.ResetAnimation(storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(2).gameObject, true);
        
        //Subtitle Text
        if (isAmmo  && item != Utility.Gameplay.Store.storeItem.NORMAL) {
            storeUpgradeUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Ammo (x" + rf.wp.firepoints.Count + "): " + counter;
        } else if (isSkin) {
            if (selected) {
                storeUpgradeUI.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().position = new Vector3(0, 15, 0);
                storeUpgradeUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Selected";
            } else {
                storeUpgradeUI.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().position = Vector3.zero;
                storeUpgradeUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
        } else if (!isAmmo && !isSkin)
            storeUpgradeUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Level " + counter;
        //Button Cost
        estimateCost();
        if (!isAmmo && !isSkin && counter >= Utility.Gameplay.Store.MAX_COUNTER) {
            storeUpgradeUI.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
            //Make Button Image Red, with MAX text
            Color imageColor = storeUpgradeUI.transform.GetChild(0).GetChild(4).GetComponent<Button>().transform.GetChild(1).GetComponent<Image>().color;
            Color.RGBToHSV(imageColor, out float hColor, out float sColor, out float vColor);
            storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Image>().color = Color.HSVToRGB(0, sColor, vColor);
            storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = "MAX";
        } else if (isSkin && counter == 1) {
            //Make Button Image Blue and centered if selected, otherwise gray
            if (selected) {
                storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<RectTransform>().position = new Vector3(0, 15, 0);
                storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0.51f, 0f, 0f);
            } else {
                storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<RectTransform>().position = Vector3.zero;
                storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0f, 1f, 0.6f);
            }
            storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        } else
            storeUpgradeUI.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = cost.ToString();
    }

    private void estimateCost() {
        switch (item) {
            case Utility.Gameplay.Store.storeItem.NORMAL:
            cost = 0;
            break;
            case Utility.Gameplay.Store.storeItem.HV:
            cost = 10 * rf.wp.firepoints.Count;
            break;
            case Utility.Gameplay.Store.storeItem.EXPLOSIVE:
            cost = 25 * rf.wp.firepoints.Count;
            break;
            case Utility.Gameplay.Store.storeItem.POISONOUS:
            cost = 25 * rf.wp.firepoints.Count;
            break;
            case Utility.Gameplay.Store.storeItem.SNOW:
            cost = 20 * rf.wp.firepoints.Count;
            break;
            case Utility.Gameplay.Store.storeItem.PENETRATION:
            cost = 45 * rf.wp.firepoints.Count;
            break;
            case Utility.Gameplay.Store.storeItem.BOX_3D_SKIN:
            cost = 2500;
            break;
            case Utility.Gameplay.Store.storeItem.CIRCLES_SKIN:
            cost = 5000;
            break;
            case Utility.Gameplay.Store.storeItem.DOTS_SKIN:
            cost = 7500;
            break;
            default:
            cost = Mathf.RoundToInt(5 + 4 * Mathf.Pow(counter - 1, 1.22f));
            break;
        }
    }

    private void estimateUnlockLevel() {
        switch (item) {
            case Utility.Gameplay.Store.storeItem.MELEE_DAMAGE:
                unlockableAt = 1;
                break;
            case Utility.Gameplay.Store.storeItem.RANGED_DAMAGE:
                unlockableAt = 3;
                break;
            case Utility.Gameplay.Store.storeItem.ATTACK_SPEED:
                unlockableAt = 5;
                break;
            case Utility.Gameplay.Store.storeItem.MAX_HEALTH:
                unlockableAt = 10;
                break;
            case Utility.Gameplay.Store.storeItem.DAMAGE_REDUCTION:
                unlockableAt = 15;
                break;
            case Utility.Gameplay.Store.storeItem.MOVEMENT_SPEED:
                unlockableAt = 20;
                break;
            case Utility.Gameplay.Store.storeItem.NORMAL:
                unlockableAt = 1;
                break;
            case Utility.Gameplay.Store.storeItem.HV:
                unlockableAt = 5;
                break;
            case Utility.Gameplay.Store.storeItem.EXPLOSIVE:
                unlockableAt = 15;
                break;
            case Utility.Gameplay.Store.storeItem.POISONOUS:
                unlockableAt = 15;
                break;
            case Utility.Gameplay.Store.storeItem.SNOW:
                unlockableAt = 20;
                break;
            case Utility.Gameplay.Store.storeItem.PENETRATION:
                unlockableAt = 25;
                break;
            case Utility.Gameplay.Store.storeItem.ELECTRICITY:
                unlockableAt = 30;
                break;
            case Utility.Gameplay.Store.storeItem.POWERUP_EFFECT:
                unlockableAt = 5;
                break;
            case Utility.Gameplay.Store.storeItem.POWERUP_DURATION:
                unlockableAt = 10;
                break;
            case Utility.Gameplay.Store.storeItem.POWERUP_SPAWN_FREQUENCY:
                unlockableAt = 15;
                break;
            case Utility.Gameplay.Store.storeItem.BOX_3D_SKIN:
                unlockableAt = 50;
                break;
            case Utility.Gameplay.Store.storeItem.CIRCLES_SKIN:
                unlockableAt = 50;
                break;
            case Utility.Gameplay.Store.storeItem.DOTS_SKIN:
                unlockableAt = 50;
                break;
        }
    }
}
