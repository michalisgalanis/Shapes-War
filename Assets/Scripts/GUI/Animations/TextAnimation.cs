using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour {
    //Setup Variables
    private float accelerationFactor;
    private float decelerationFactor;
    [HideInInspector] public string finalString;
    [HideInInspector] public string startString;
    [HideInInspector] public string backupStartString = "";
    [HideInInspector] public string backupFinalString = "";
    private float finalValue;
    private string prefixChar;
    private string suffixChar;
    public float delay = 0f;

    //Runtime Variables
    private float currentSpeed;
    private float startValue;
    private float currentValue;
    private float currentTimer;
    [HideInInspector] public bool finished = false;
    private bool showDecimalPart;

    private void Awake() {
        currentSpeed = 0f;
        currentTimer = 0f;
        finished = false;
    }

    void Start() {
        //Debug.Log(transform.parent.parent.parent.name + " BEFORE || (Start) \"" + startString + "\" ==> \"" + finalString + "\" (Finish), BCKP_START: " + backupStartString + ", BCKP_FINAL: " + backupFinalString);
        backupFinalString = GetComponent<TextMeshProUGUI>().text;
        EstimateChars(backupFinalString, out _, out _, out float tempValue);
        finalString = (backupFinalString.Equals("") || tempValue == 0) ? ((finalString == null || finalString.Equals("")) ?  GetComponent<TextMeshProUGUI>().text  : finalString) : backupFinalString;
        EstimateChars(finalString, out prefixChar, out suffixChar, out finalValue);
        startString = backupStartString.Equals("") ? ((startString == null || startString.Equals("")) ? prefixChar + "0" + suffixChar : startString) : backupStartString ;
        EstimateChars(startString, out _, out _, out startValue);
        //Debug.Log(transform.parent.parent.parent.name +  " AFTER || (Start) \"" + startString + "\" ==> \"" + finalString + "\" (Finish), BCKP_START: " + backupStartString + ", BCKP_FINAL: " + backupFinalString);

        showDecimalPart = finalValue - (int)finalValue == 0;
        accelerationFactor = 0.0005f * (finalValue - startValue) * GameSettings.animationSpeedFactor;
        decelerationFactor = 0.0005f * (finalValue - startValue) * GameSettings.animationSpeedFactor;
        currentValue = startValue;
        if (GetComponent<TextMeshProUGUI>().text.Equals("MAX"))
            finished = true;
        else
            GetComponent<TextMeshProUGUI>().text = prefixChar + currentValue.ToString() + suffixChar;
        if (!GameSettings.animationsEnabled && !finished) {
            currentValue = finalValue;
            GetComponent<TextMeshProUGUI>().text = prefixChar + currentValue.ToString() + suffixChar;
        }
    }

    void Update() {
        if (finished || !GameSettings.animationsEnabled)
            return;
        else if (currentValue == finalValue) {
            GetComponent<TextMeshProUGUI>().text = prefixChar + currentValue.ToString() + suffixChar;
            finished = true;
        }

        if (currentTimer >= delay / GameSettings.animationSpeedFactor) {
            currentSpeed = (currentValue - startValue >= 0.51f * (finalValue - startValue)) ? Mathf.Max(currentSpeed - decelerationFactor, 0f) : currentSpeed + accelerationFactor;
            currentValue = Utility.Functions.GeneralFunctions.getRoundedFloat(Mathf.Min(finalValue, currentValue + currentSpeed), 1);
            backupStartString = prefixChar + currentValue + suffixChar;
            GetComponent<TextMeshProUGUI>().text = prefixChar + (showDecimalPart ? ((int)currentValue).ToString() : (currentValue).ToString()) + suffixChar;
        } else
            currentTimer += Time.unscaledDeltaTime;
        //Debug.Log("Value: " + currentValue + ", Speed: " + currentSpeed + ", Devation: " + Mathf.Abs(finalValue - currentValue) / finalValue);
    }

    private void EstimateChars(string sourceString, out string prefixChar, out string suffixChar, out float finalValue) {
        prefixChar = "";
        suffixChar = "";
        if (sourceString.Equals("")) {
            finalValue = 0;
            return; }
        if (!float.TryParse(sourceString, out finalValue) || sourceString.Contains("+")) {
            if (float.TryParse(sourceString.Substring(1), out finalValue))
                prefixChar = sourceString[0].ToString();
            else if (float.TryParse(sourceString.Substring(0, sourceString.Length - 1), out finalValue))
                suffixChar = sourceString[sourceString.Length - 1].ToString();
        }
    }
}
