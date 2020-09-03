using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour {
    public void RefreshStats() {
        ////PLAYER-STATS
        //Bullets Shot
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.bulletsFired.ToString();
        //Enemies Killed
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.enemiesKilled.ToString();
        //Bullet Hit Ratio
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.bulletsFired == 0 ? "0%" : Utility.Functions.GeneralFunctions.getRoundedFloat(100f * RuntimeSpecs.bulletsHit/ RuntimeSpecs.bulletsFired, 2) + "%";
        //Damage Dealt to Enemies
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = ((int)RuntimeSpecs.damageDealtToEnemies).ToString();
        //Damage Dealt to Player
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = ((int)RuntimeSpecs.damageDealtToPlayer).ToString();
        //Times Dead
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.timesDead.ToString();
        //Hp Healed
        transform.GetChild(2).GetChild(0).GetChild(3).GetChild(6).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.hpHealed.ToString();

        ////GENERAL-STATS
        //Time Played
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Coming Soon"; //Coming soon
        //Average Round Time
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Coming Soon"; //Coming soon
        //Coins Collected
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.totalCoinsCollected.ToString();
        //XP Gained
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.totalXpGained.ToString();
        //Items Upgraded
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.itemUpgradesMade.ToString();
        //Powerups Used
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.powerupsActivated.ToString();
        //Powerups Lasted
        transform.GetChild(2).GetChild(1).GetChild(3).GetChild(6).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Coming Soon"; //Coming soon
    }
}
