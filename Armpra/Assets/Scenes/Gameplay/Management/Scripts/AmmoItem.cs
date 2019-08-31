using UnityEngine;
using UnityEngine.UI;


public class AmmoItem {
    //References
    private readonly Referencer rf;

    //Setup Variables
    public Button ammoButton;
    public BulletSpecs bullet;
    public Constants.Gameplay.Bullet.bulletTypes bulletType;

    //Runtime Variables
    public int ammo;
    public bool enabled;
    public bool selected;


    public AmmoItem(Button ammoButton, BulletSpecs bullet, Constants.Gameplay.Bullet.bulletTypes bulletType) {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        this.ammoButton = ammoButton;
        this.bullet = bullet;
        this.bulletType = bulletType;
        ammo = 0;
        enabled = false;
        selected = false;
    }

    public void UpdateAmmo() {
        switch (bulletType) {
            case Constants.Gameplay.Bullet.bulletTypes.NORMAL:
            ammo = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.NORMAL).counter;
            break;
            case Constants.Gameplay.Bullet.bulletTypes.HV:
            ammo = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.HV).counter;
            //ammoButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ammo.ToString();
            break;
            case Constants.Gameplay.Bullet.bulletTypes.EXPLOSIVE:
            ammo = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.EXPLOSIVE).counter;
            break;
            case Constants.Gameplay.Bullet.bulletTypes.POISONOUS:
            ammo = rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POISONOUS).counter;
            break;
        }
        enabled = (ammo >= 1);
    }

    public bool ConsumeAmmo() {
        switch (bulletType) {
            case Constants.Gameplay.Bullet.bulletTypes.NORMAL:
            return true;
            case Constants.Gameplay.Bullet.bulletTypes.HV:
            if (rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.HV).counter >= 1) {
                rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.HV).counter--;
                return true;
            } else return false;
            case Constants.Gameplay.Bullet.bulletTypes.EXPLOSIVE:
            if (rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.EXPLOSIVE).counter >= 1) {
                rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.EXPLOSIVE).counter--;
                return true;
            } else return false;
            case Constants.Gameplay.Bullet.bulletTypes.POISONOUS:
            if (rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POISONOUS).counter >= 1) {
                rf.ss.findStoreItemByType(Constants.Gameplay.Store.storeItem.POISONOUS).counter--;
                return true;
            } else return false;
        }
        UpdateAmmo();
        return false;
    }
}
