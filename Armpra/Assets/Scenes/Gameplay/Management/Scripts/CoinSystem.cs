using TMPro;
using UnityEngine;

public class CoinSystem : MonoBehaviour {
    //References
    private Referencer rf;

    public void Awake() {
        rf = GetComponent<Referencer>();
    }

    public void FixedUpdate() {
        for (int i = 0; i < rf.coinTexts.Length; i++) {
            rf.coinTexts[i].GetComponent<TextMeshProUGUI>().text = RuntimeSpecs.currentCoins.ToString();
        }
    }

    public void addCoins(int coins) {
        RuntimeSpecs.currentCoins += coins;
    }

    public void spendCoins(int coins) {
        RuntimeSpecs.currentCoins -= coins;
    }

    public bool canSpendCoins(int coins) {
        return RuntimeSpecs.currentCoins >= coins;
    }
}
