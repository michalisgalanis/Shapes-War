using UnityEngine;
using UnityEngine.UI;

public class EnlargeAnimation : MonoBehaviour {

    private Vector3 acceleration;
    private float accelerationFactor = 0.003f;
    private Vector3 currentSpeed = Vector3.zero;
    private Vector3 currentSize = Vector3.zero;
    [HideInInspector] public Vector3 finalScale;
    [HideInInspector] public Vector3 backupScale;
    private float currentTimer = 0f;
    public float delay = 0f;
    public bool uiElement = true;
    public bool isMask = false;

    private void Start() {
        uiElement = uiElement && GetComponent<RectTransform>() != null;
        if (uiElement) {
            finalScale = new Vector3(1, 1, 1);
            if (GameSettings.animationsEnabled)
                GetComponent<RectTransform>().localScale = Vector3.zero;
            else
                GetComponent<RectTransform>().localScale = finalScale;
        } else {
            finalScale = (backupScale.Equals(Vector3.zero)) ? transform.localScale : backupScale;
            if (GameSettings.animationsEnabled)
                transform.localScale = Vector3.zero;
            else
                transform.localScale = finalScale;
        }
        //Debug.Log(finalScale);
        if (isMask) accelerationFactor *= 10f / GameSettings.animationSpeedFactor;
        acceleration = new Vector3(accelerationFactor * GameSettings.animationSpeedFactor * finalScale.x, accelerationFactor * GameSettings.animationSpeedFactor * finalScale.y, accelerationFactor * GameSettings.animationSpeedFactor * finalScale.z);
    }

    private void Update() {
        if (!GameSettings.animationsEnabled) return;
        if (currentTimer >= delay / GameSettings.animationSpeedFactor) {
            //Updating Speed 
            currentSpeed.x = (currentSize.x >= 0.5f * finalScale.x) ? Mathf.Max(currentSpeed.x - acceleration.x, 0.01f) : Mathf.Min(currentSpeed.x + acceleration.x, 0.5f);
            currentSpeed.y = (currentSize.y >= 0.5f * finalScale.y) ? Mathf.Max(currentSpeed.y - acceleration.y, 0.01f) : Mathf.Min(currentSpeed.y + acceleration.y, 0.5f);
            currentSpeed.z = (currentSize.z >= 0.5f * finalScale.z) ? Mathf.Max(currentSpeed.z - acceleration.z, 0.01f) : Mathf.Min(currentSpeed.z + acceleration.z, 0.5f);
            //Updating Size 
            currentSize.x = Mathf.Min(finalScale.x, currentSize.x + currentSpeed.x);
            currentSize.y = Mathf.Min(finalScale.y, currentSize.y + currentSpeed.y);
            currentSize.z = Mathf.Min(finalScale.z, currentSize.z + currentSpeed.z);
            //Applying Changes
            if (uiElement) {
                GetComponent<RectTransform>().localScale = currentSize;
                if (GetComponentInParent<LayoutGroup>() != null) {
                    GetComponentInParent<LayoutGroup>().enabled = false;
                    GetComponentInParent<LayoutGroup>().enabled = true;
                }
            } else transform.localScale = currentSize;
        } else
            currentTimer += Time.unscaledDeltaTime;
        if (!uiElement && transform.localScale.Equals(finalScale)) Destroy(this);
    }
}