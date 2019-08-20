using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoSystem : MonoBehaviour{
    public Button[] bulletButtons;      //input
    public GameObject[] bulletTypes;    //input

    private int[] bulletAmmo;           //generated here

    [HideInInspector] public List<Button> enabledButtons;

    public BulletSpecs currentActiveBullet;     //output
    private Button currentActiveButton;
    private int currentActiveIndex;

    //0-normalbullet, 1-hvbullet, 2-explosivebullet, 3-poisonousbullet

    private StoreSystem ss;
    void Start()
    {
        ss = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoreSystem>();
        bulletAmmo = new int[bulletTypes.Length];
        enabledButtons = new List<Button>();

        for (int i = 1; i < bulletButtons.Length; i++) {
            bulletButtons[i].gameObject.SetActive(false);
        }

        currentActiveIndex = 0;
        currentActiveButton = bulletButtons[currentActiveIndex];
        currentActiveBullet = bulletTypes[currentActiveIndex].GetComponent<BulletSpecs>();
    }

    void Update()
    {
        enabledButtons.Clear();
        bulletAmmo[0] = ss.normalBulletAmmo;
        bulletAmmo[1] = ss.highVelocityBulletAmmo;
        bulletAmmo[2] = ss.explosiveBulletAmmo;
        bulletAmmo[3] = ss.poisonousBulletAmmo;
        
        for (int i = 0; i < bulletButtons.Length; i++) {
            if (bulletAmmo[i] > 0) {
                enabledButtons.Add(bulletButtons[i]);
            }
            if (i != 0) bulletButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bulletAmmo[i].ToString();
        }
        //Debug.Log("[" + bulletAmmo[0] + "," + bulletAmmo[1] + "," + bulletAmmo[2] + "," + bulletAmmo[3] + "]");

        if ((!currentActiveButton.Equals(bulletButtons[0]) && int.Parse(currentActiveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) <= 0 ) || !enabledButtons.Contains(currentActiveButton)) NextInCycle();
    }

    public bool ConsumeAmmo() {
        switch (currentActiveBullet.bulletType) {
            case (BulletSpecs.bulletTypes.NORMAL):
            if (ss.normalBulletAmmo > 0) {
                ss.normalBulletAmmo--;
                return true;
            }
            break;
            case (BulletSpecs.bulletTypes.HV):
                if (ss.highVelocityBulletAmmo > 0) {
                ss.highVelocityBulletAmmo--;
                return true;
            }
            break;
            case (BulletSpecs.bulletTypes.EXPLOSIVE):
                if (ss.explosiveBulletAmmo > 0) {
                ss.explosiveBulletAmmo--;
                return true;
            }
            break;
            case (BulletSpecs.bulletTypes.POISONOUS):
                if (ss.poisonousBulletAmmo > 0) {
                ss.poisonousBulletAmmo--;
                return true;
            }
            break;
        }
        currentActiveIndex = 0;
        currentActiveButton = bulletButtons[currentActiveIndex];
        currentActiveBullet = bulletTypes[currentActiveIndex].GetComponent<BulletSpecs>();
        return false;
    }

    public void NextInCycle() {
        currentActiveIndex++;
        if (currentActiveIndex == bulletButtons.Length) currentActiveIndex %= bulletButtons.Length;
        currentActiveButton = bulletButtons[currentActiveIndex];
        currentActiveBullet = bulletTypes[currentActiveIndex].GetComponent<BulletSpecs>();
        for (int i = 0; i < bulletButtons.Length; i++) {
            bulletButtons[i].gameObject.SetActive(i == currentActiveIndex);
        }
        if (!enabledButtons.Contains(currentActiveButton)) NextInCycle();
    }
}
