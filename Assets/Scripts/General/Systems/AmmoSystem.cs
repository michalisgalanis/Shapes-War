using System;
using System.Collections.Generic;
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

    void Start() {
        for (int i = 0; i < rf.bulletTypes.Length; i++) {
            ammoItems.Add(new AmmoItem(rf.ammoBulletButtons[i], rf.bulletTypes[i].GetComponent<Bullet>(), (Utility.Gameplay.Bullet.bulletTypes)Enum.GetValues(typeof(Utility.Gameplay.Bullet.bulletTypes)).GetValue(i)));
        }
        ammoItems[0].enabled = true;
        ammoItems[0].selected = true;
        activeAmmoItem = ammoItems[0];
        rf.wp.ResetShooting();
        StartCoroutine(rf.wp.Shoot());
    }

    void Update() {
        for (int i = 0; i < ammoItems.Count; i++) {
            ammoItems[i].UpdateAmmo();
            if (ammoItems[i].enabled && ammoItems[i].selected)
                activeAmmoItem = ammoItems[i];
        }
        if (!activeAmmoItem.enabled)
            SelectFirstBullet();
        ApplyVisualChanges();
    }

    public bool ConsumeAmmo() {
        for (int i = 0; i < ammoItems.Count; i++) {
            ammoItems[i].UpdateAmmo();
            if (ammoItems[i].Equals(activeAmmoItem)) {
                return ammoItems[i].ConsumeAmmo();
            }
        }
        return false;
    }

    public void SelectFirstBullet() {
        for (int i = 0; i < ammoItems.Count; i++) {
            ammoItems[i].selected = false;
            rf.ammoBulletButtons[i].gameObject.SetActive(false);
        }
        ammoItems[0].enabled = true;
        ammoItems[0].selected = true;
        activeAmmoItem = ammoItems[0];
        ApplyVisualChanges();
        rf.wp.activeBullet = activeAmmoItem.bullet;
    }

    public void SelectNextBullet() {
        rf.audioManagerComp.PlayByNumber(Utility.Audio.soundTypes.UI_SOUNDS, 0);
        int current = 0;
        for (int j = 0; j < ammoItems.Count; j++) {
            if (activeAmmoItem.Equals(ammoItems[j]))
                current = j;
        }
        int i = 0, next;
        do {
            next = (current + 1 + i) % ammoItems.Count;
            ammoItems[current].UpdateAmmo();
            ammoItems[next].UpdateAmmo();
            //Debug.Log("Considering Bullet Switch: (" + ammoItems[next].bulletType + "|" + ammoItems[next].ammoButton + "|" + ammoItems[next].bullet + (ammoItems[next].enabled ? "- Enabled" : "- Disabled") + ") in index " + next);
            if (ammoItems[next].enabled) {
                ammoItems[current].selected = false;
                ammoItems[next].selected = true;
                activeAmmoItem = ammoItems[next];
                ApplyVisualChanges();
                rf.wp.activeBullet = activeAmmoItem.bullet;
                break;
            }
            i++;
        } while (!ammoItems[next].enabled);
    }

    public void ApplyVisualChanges() {
        for (int i = 0; i < ammoItems.Count; i++) {
            rf.ammoBulletButtons[i] = ammoItems[i].ammoButton;
            if (i != 0)
                rf.ammoBulletButtons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ammoItems[i].ammo.ToString();
            rf.ammoBulletButtons[i].gameObject.SetActive(ammoItems[i].selected);
        }
    }
}
