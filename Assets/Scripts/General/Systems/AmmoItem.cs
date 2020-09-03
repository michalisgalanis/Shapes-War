using UnityEngine;
using UnityEngine.UI;


public class AmmoItem {
    //References
    private readonly Referencer rf;

    //Setup Variables
    public Button ammoButton;
    public Bullet bullet;
    public Utility.Gameplay.Bullet.bulletTypes bulletType;

    //Runtime Variables
    public int ammo;
    public bool enabled;
    public bool selected;


    public AmmoItem(Button ammoButton, Bullet bullet, Utility.Gameplay.Bullet.bulletTypes bulletType) {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        this.ammoButton = ammoButton;
        this.bullet = bullet;
        this.bulletType = bulletType;
        ammo = 0;
        enabled = false;
        selected = false;
    }

    public void UpdateAmmo() {
        switch (bulletType) {
            case Utility.Gameplay.Bullet.bulletTypes.NORMAL:
                //ammo = 1;
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.NORMAL).counter;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.HV:
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.HV).counter;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.EXPLOSIVE:
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.EXPLOSIVE).counter;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.POISONOUS:
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POISONOUS).counter;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.SNOW:
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.SNOW).counter;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.PENETRATION:
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.PENETRATION).counter;
                break;
            case Utility.Gameplay.Bullet.bulletTypes.ELECTRICITY:
                ammo = rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.ELECTRICITY).counter;
                break;
        }
        enabled = (ammo >= 1);
    }

    public bool ConsumeAmmo() {
        switch (bulletType) {
            case Utility.Gameplay.Bullet.bulletTypes.NORMAL:
                return true;
            case Utility.Gameplay.Bullet.bulletTypes.HV:
                if (rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.HV).counter >= 1) {
                    rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.HV).counter--;
                    return true;
                } else
                    return false;
            case Utility.Gameplay.Bullet.bulletTypes.EXPLOSIVE:
                if (rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.EXPLOSIVE).counter >= 1) {
                    rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.EXPLOSIVE).counter--;
                    return true;
                } else
                    return false;
            case Utility.Gameplay.Bullet.bulletTypes.POISONOUS:
                if (rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POISONOUS).counter >= 1) {
                    rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.POISONOUS).counter--;
                    return true;
                } else
                    return false;
            case Utility.Gameplay.Bullet.bulletTypes.SNOW:
                if (rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.SNOW).counter >= 1) {
                    rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.SNOW).counter--;
                    return true;
                } else
                    return false;
            case Utility.Gameplay.Bullet.bulletTypes.PENETRATION:
                if (rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.PENETRATION).counter >= 1) {
                    rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.PENETRATION).counter--;
                    return true;
                } else
                    return false;
            case Utility.Gameplay.Bullet.bulletTypes.ELECTRICITY:
                if (rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.ELECTRICITY).counter >= 1) {
                    rf.ss.findStoreItemByType(Utility.Gameplay.Store.storeItem.ELECTRICITY).counter--;
                    return true;
                } else
                    return false;
        }
        UpdateAmmo();
        return false;
    }
}
