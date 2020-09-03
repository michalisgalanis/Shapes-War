using UnityEngine;
using System;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
public class AdSystem {
    //References
    private static AdSystem instance;

    //Setup Variables
    [Header("Setup Variables")]
    private string gameID = "";
    [SerializeField] private bool testMode = true;
    private string rewardedVideoPlacementId = "rewardedVideo";
    private string regularPlacementId = "video";

    private AdSystem() {
        instance = this;
        gameID = (GameSettings.platform == RuntimePlatform.Android) ? "3304096" : "3304097";
#if UNITY_ADS
        Advertisement.Initialize(gameID, testMode);
#endif
    }

#if UNITY_ADS
    public void ShowRegularAd(Action<ShowResult> callback) {
        Time.timeScale = 0;
        if (Advertisement.IsReady(regularPlacementId)) {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(regularPlacementId, so);
        } else
            Debug.Log("Ad not ready");
    }
#endif

#if UNITY_ADS
    public void ShowRewardedAd(Action<ShowResult> callback) {
        Time.timeScale = 0;
        if (Advertisement.IsReady(rewardedVideoPlacementId)) {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(rewardedVideoPlacementId, so);
        } else
            Debug.Log("Rewarded ad not ready");
    }
#endif


    public static AdSystem getInstance() {
        return instance ?? new AdSystem();
    }
}
