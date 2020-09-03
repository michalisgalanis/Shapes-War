using TMPro;
using UnityEngine;

public class CoinSystem {

    private static readonly Referencer rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();

    public static void RefreshCoins() {
        if (rf == null) return;
        foreach (TextMeshProUGUI text in rf.totalCoinsTexts) {
            text.text = Utility.Functions.GeneralFunctions.compressCoinsText(RuntimeSpecs.currentCoins);
            if (text.gameObject != null && text.gameObject.activeInHierarchy) AnimationManager.ResetAnimation(text.gameObject, true);
        }
        foreach (TextMeshProUGUI text in rf.coinsCollectedTexts) {
            text.text = "+" + Utility.Functions.GeneralFunctions.compressCoinsText(RuntimeSpecs.coinsCollected);
            if (text.gameObject != null && text.gameObject.activeInHierarchy) AnimationManager.ResetAnimation(text.gameObject, false);
        }
    }

    public static void AddCoins(int coins) {
        RuntimeSpecs.currentCoins += coins;
        RuntimeSpecs.coinsCollected += coins;
        RuntimeSpecs.totalCoinsCollected += coins;
        RefreshCoins();
    }

    public static void SpendCoins(int coins) {
        RuntimeSpecs.currentCoins -= coins;
        RefreshCoins();
    }

    public static bool CanSpendCoins(int coins) {
        return RuntimeSpecs.currentCoins >= coins;
    }
}
