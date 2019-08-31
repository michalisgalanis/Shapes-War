using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class AmmoSystem : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    [HideInInspector] public List<AmmoItem> ammoItems;
    [HideInInspector] public AmmoItem activeAmmoItem;

    public void Awake() {
        rf = GetComponent<Referencer>();
        ammoItems = new List<AmmoItem>();
    }

    public void Start() {
        for (int i = 0; i < rf.bulletTypes.Length; i++) {
            ammoItems.Add(new AmmoItem(rf.ammoBulletButtons[i], rf.bulletTypes[i].GetComponent<BulletSpecs>(), (Constants.Gameplay.Bullet.bulletTypes)Enum.GetValues(typeof(Constants.Gameplay.Bullet.bulletTypes)).GetValue(i)));
        }
        ammoItems[0].enabled = true;
        ammoItems[0].selected = true;
        activeAmmoItem = ammoItems[0];
    }

    public void Update() {
        for (int i = 0; i < ammoItems.ToArray().Length; i++) {
            ammoItems[i].UpdateAmmo();
            if (ammoItems[i].enabled && ammoItems[i].selected)
                activeAmmoItem = ammoItems[i];
        }
        if (!activeAmmoItem.enabled) SelectFirstBullet();
        ApplyVisualChanges();
    }

    public bool ConsumeAmmo() {
        for (int i = 0; i < ammoItems.ToArray().Length; i++) {
            ammoItems[i].UpdateAmmo();
            if (ammoItems[i].Equals(activeAmmoItem)) {
                return ammoItems[i].ConsumeAmmo();
            }
        }
        return false;
    }

    public void SelectFirstBullet() {
        for (int i = 0; i < ammoItems.ToArray().Length; i++){
            ammoItems[i].selected = false;
            rf.ammoBulletButtons[i].gameObject.SetActive(false);
        }
        ammoItems[0].enabled = true;
        ammoItems[0].selected = true;
        activeAmmoItem = ammoItems[0];
        ApplyVisualChanges();
    }

    public void SelectNextBullet() {
        for (int current = 0; current < ammoItems.ToArray().Length; current++) {
            int next = (current + 1) % ammoItems.ToArray().Length;
            ammoItems[current].UpdateAmmo();
            ammoItems[next].UpdateAmmo();
            if (ammoItems[next].enabled) {
                ammoItems[current].selected = false;
                ammoItems[next].selected = true;
                activeAmmoItem = ammoItems[next];
                ApplyVisualChanges();
                break;
            }
        }
    }

    public void ApplyVisualChanges() {
        for (int i = 0; i < ammoItems.ToArray().Length; i++) {
            rf.ammoBulletButtons[i] = ammoItems[i].ammoButton;
            if (i != 0) rf.ammoBulletButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ammoItems[i].ammo.ToString();
            rf.ammoBulletButtons[i].gameObject.SetActive(ammoItems[i].selected);
        }
    }
}
