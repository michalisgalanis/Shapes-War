using UnityEngine;

public class ProgressbarAnimation : MonoBehaviour {

    //References
    public RectTransform filler;
    public RectTransform parentPanel;

    //Setup Variables
    private float acceleration = 0.001f;
    private float maxWidth;

    //Runtime Variables
    private float currentSpeed;
    private float currentPercentage;
    private float targetPercentage;

    private void Start() {
        maxWidth = parentPanel.rect.width;
        currentPercentage = 0f;
        currentSpeed = 0f;
    }

    public void ApproachTarget(float target) {
        targetPercentage = target;
        currentSpeed = 0.3f * (targetPercentage - currentPercentage) * GameSettings.animationSpeedFactor;
        if (!GameSettings.animationsEnabled) {
            currentPercentage = target;
            FixedUpdate();
        }
    }

    void Update() {
        if (!GameSettings.animationsEnabled) return;
        currentSpeed = Mathf.Max(currentSpeed - acceleration, 0);
        if (Mathf.Abs(targetPercentage - currentPercentage) > 0.001f) {
            ApproachTarget(targetPercentage);
            currentPercentage += currentSpeed;
        }
        //Debug.Log("TARGET: " +  Utility.Functions.GeneralFunctions.getRoundedFloat(targetPercentage, 4) + ", CURRENT_PER: " + Utility.Functions.GeneralFunctions.getRoundedFloat(currentPercentage, 4) + ", VARIATION: " + Utility.Functions.GeneralFunctions.getRoundedFloat((targetPercentage - currentPercentage), 4));
    }

    private void FixedUpdate() {
        filler.offsetMax = new Vector2(Mathf.Abs(currentPercentage * maxWidth * (-1)) - maxWidth, 0);
    }
}
