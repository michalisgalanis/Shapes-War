using TMPro;
using UnityEngine;

public class CoinSystem : MonoBehaviour {
    public GameObject[] coinsText;
    private int currentCoins;

    private void Start() {
        currentCoins = 0;
    }

    private void Update() {
        for (int i = 0; i < coinsText.Length; i++) {
            coinsText[i].GetComponent<TextMeshProUGUI>().text = currentCoins.ToString();
        }
    }

    public void addCoins(int coins) {
        currentCoins += coins;
    }

    public void removeCoins(int coins) {
        currentCoins -= coins;
    }

    public bool canRemoveCoins(int coins) {
        return currentCoins >= coins;
    }
}
