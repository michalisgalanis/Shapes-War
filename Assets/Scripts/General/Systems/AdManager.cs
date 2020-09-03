using UnityEngine.Advertisements;
using UnityEngine;
using System;
using TMPro;

public class AdManager {
    //References
    private Referencer rf;
    private static AdManager instance;
    private bool noAdsPurchase = false;

    public Utility.Gameplay.Ads.rewardedAdType rewardedAdType;
    public string rewardedAdTypeString = "";

    //Runtime Variables
    public int roundsPassedSinceLastRegularAd = 0;
    private int roudsForNextRegularAd = UnityEngine.Random.Range(4, 5);
    public int roundsPassedSinceLastRewardedAd = 0;
    private int roudsForNextRewardedAd = UnityEngine.Random.Range(3, 4);


    private AdManager() {
        instance = this; //Debug.Log(roundsPassedSinceLastAd);
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        roundsPassedSinceLastRewardedAd = 0;
        roundsPassedSinceLastRegularAd = 0;
        roudsForNextRegularAd = UnityEngine.Random.Range(4, 5);
        roudsForNextRewardedAd = UnityEngine.Random.Range(3, 4);
    }

    public bool CheckAdPropability() {
        if (noAdsPurchase) return false;
        //Debug.Log("Normal Ad Rounds Passed: " + roundsPassedSinceLastRegularAd);
        if (roundsPassedSinceLastRegularAd >= roudsForNextRegularAd) {
            roundsPassedSinceLastRegularAd = 0;
            roudsForNextRegularAd = UnityEngine.Random.Range(4, 6);
            return true;
        }
        roundsPassedSinceLastRegularAd++;
        return false;
    }

    public bool CheckRewardedAdPropability() {
        //Debug.Log("Rewarded Ad Rounds Passed: " + roundsPassedSinceLastRewardedAd);
        if (roundsPassedSinceLastRewardedAd >= roudsForNextRewardedAd) {
            roundsPassedSinceLastRewardedAd = 0;
            roudsForNextRewardedAd = UnityEngine.Random.Range(2, 3);
            rewardedAdType = (Utility.Gameplay.Ads.rewardedAdType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Utility.Gameplay.Ads.rewardedAdType)).Length);
            switch (rewardedAdType) {
                case Utility.Gameplay.Ads.rewardedAdType.DOUBLE_XP_COINS: rewardedAdTypeString = "Double XP & Coins"; break;
                case Utility.Gameplay.Ads.rewardedAdType.TRIPLE_COINS: rewardedAdTypeString = "Triple Coins"; break;
                case Utility.Gameplay.Ads.rewardedAdType.TRIPLE_XP: rewardedAdTypeString = "Triple XP"; break;
            }
            return true;
        }
        roundsPassedSinceLastRewardedAd++;
        return false;
    }

    public void PlayAd() {
#if UNITY_ADS
        AdSystem.getInstance().ShowRegularAd(OnAdClosed);
#endif
    }

    public void PlayRewardedAd() {
#if UNITY_ADS
        AdSystem.getInstance().ShowRewardedAd(OnRewardedAdClosed);
#endif
    }

#if UNITY_ADS
    private void OnAdClosed(ShowResult result) {
        //Debug.Log("Regular ad closed");
        rf.gm.ExitAd(rf.gm.adSource);
    }
#endif

#if UNITY_ADS
    private void OnRewardedAdClosed(ShowResult result) {
        //Debug.Log("Rewarded ad closed");
        if (result == ShowResult.Finished) {
            if (rf == null) rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
            rf.winMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
            rf.winMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
            rf.lostMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
            rf.lostMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, 0.6f);
            //Debug.Log("Ad Finished, reward player");
            switch (rewardedAdType) {
                case Utility.Gameplay.Ads.rewardedAdType.DOUBLE_XP_COINS:
                    rf.pe.AddXP(1 * RuntimeSpecs.xpCollected);
                    CoinSystem.AddCoins(1 * RuntimeSpecs.coinsCollected);
                    rf.winMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //coins collected green text
                    rf.winMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //xp collected green text
                    rf.lostMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //coins collected green text
                    rf.lostMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //xp collected green text
                    break;
                case Utility.Gameplay.Ads.rewardedAdType.TRIPLE_XP:
                    rf.pe.AddXP(2 * RuntimeSpecs.xpCollected);
                    rf.winMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //xp collected green text
                    rf.lostMenuUI.transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //xp collected green text
                    break;
                case Utility.Gameplay.Ads.rewardedAdType.TRIPLE_COINS:
                    CoinSystem.AddCoins(2 * RuntimeSpecs.coinsCollected);
                    rf.winMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //coins collected green text
                    rf.lostMenuUI.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Utility.Functions.GeneralFunctions.generateColor(0.27f, 1f, 1f, 0.6f); //coins collected green text
                    break;
            }
        }
        rf.gm.ExitAd(rf.gm.rewardedAdSource);
    }
#endif

    public static AdManager getInstance() {
        return instance ?? new AdManager();
    }
}
