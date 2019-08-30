using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour {
    //References
    private Referencer rf;

    //Runtime Variables
    [HideInInspector] public List<Button> enabledButtons;
    private int[] bulletAmmo;           //generated here
    [HideInInspector] public BulletSpecs currentActiveBullet;     //output
    private Button currentActiveButton;
    private int currentActiveIndex = 0;

    public void Awake() {
        rf = GetComponent<Referencer>();
        bulletAmmo = new int[rf.bulletTypes.Length];
        enabledButtons = new List<Button>();
    }

    public void Start() {
        for (int i = 1; i < rf.ammoBulletButtons.Length; i++) {
            rf.ammoBulletButtons[i].gameObject.SetActive(false);
        }
        currentActiveButton = rf.ammoBulletButtons[currentActiveIndex];
        currentActiveBullet = rf.bulletTypes[currentActiveIndex].GetComponent<BulletSpecs>();
    }

    public void Update() {
        enabledButtons.Clear();
        int tempCounter = 0;
        foreach (StoreItem item in rf.ss.upgrades) {
            if (item.isAmmo) {
                bulletAmmo[tempCounter++] = item.counter;
            }
        }

        for (int i = 0; i < rf.ammoBulletButtons.Length; i++) {
            if (bulletAmmo[i] > 0) {
                enabledButtons.Add(rf.ammoBulletButtons[i]);
            }
            if (i != 0) {
                rf.ammoBulletButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bulletAmmo[i].ToString();
            }
        }
        //Debug.Log("[" + bulletAmmo[0] + "," + bulletAmmo[1] + "," + bulletAmmo[2] + "," + bulletAmmo[3] + "]" + ", bul: " + currentActiveBullet.name + ",but: " + currentActiveButton.name + ", index: " + currentActiveIndex); //+ " , [" + enabledButtons[0].IsActive() + "," + enabledButtons[1].IsActive() + ", " + enabledButtons[2].IsActive() + ", " + enabledButtons[3].IsActive() + "]");

        if ((!currentActiveButton.Equals(rf.ammoBulletButtons[0]) && int.Parse(currentActiveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) <= 0) || !enabledButtons.Contains(currentActiveButton)) {
            NextInCycle();
        }
    }

    public bool ConsumeAmmo() {
        int ammoCounter = 0;
        foreach (StoreItem item in rf.ss.upgrades) {
            if (item.isAmmo && item.counter > 0 && (ammoCounter++) == currentActiveIndex) {
                if (item.item != Constants.Gameplay.Store.storeItem.NORMAL) item.counter--;
                return true;
            }
        }
        return false;
    }

    public void NextInCycle() {
        currentActiveIndex++;
        if (currentActiveIndex == rf.ammoBulletButtons.Length) {
            currentActiveIndex %= rf.ammoBulletButtons.Length;
        }
        currentActiveButton = rf.ammoBulletButtons[currentActiveIndex];
        currentActiveBullet = rf.bulletTypes[currentActiveIndex].GetComponent<BulletSpecs>();
        for (int i = 0; i < rf.ammoBulletButtons.Length; i++) {
            rf.ammoBulletButtons[i].gameObject.SetActive(i == currentActiveIndex);
        }
        if (!enabledButtons.Contains(currentActiveButton) || (!currentActiveButton.Equals(rf.ammoBulletButtons[0]) && int.Parse(currentActiveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) <= 0) || !enabledButtons.Contains(currentActiveButton)) {
            NextInCycle();
        }
    }
}
