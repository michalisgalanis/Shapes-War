using UnityEngine;
using UnityEngine.UI;

public class SliderAnimation : MonoBehaviour {

    private float currentSpeed = 0;
    private float currentPercentage = 0;
    [HideInInspector] public float targetPercentage = -1f;
    [HideInInspector] public bool animationFinished = false;

    private float currentTimer = 0f;
    public float delay = 0f;

    void Start() {
        //Debug.Log("Target  BEFORE: " + targetPercentage + ", Slider Value: " + GetComponent<Slider>().value);
        targetPercentage = targetPercentage == -1f ? GetComponent<Slider>().value / GetComponent<Slider>().maxValue : targetPercentage;
        //Debug.Log("Target  AFTER: " + targetPercentage + ", Slider Value: " + GetComponent<Slider>().value);
        GetComponent<Slider>().onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
        GetComponent<Slider>().value = GetComponent<Slider>().minValue;
        ApproachTarget(targetPercentage);
    }


    private void ApproachTarget(float targetPercentage) {
        currentSpeed = 0.1f * (targetPercentage - currentPercentage) * GameSettings.animationSpeedFactor;
        if (Mathf.Abs(targetPercentage - currentPercentage) <= 0.002f) {
            currentSpeed = targetPercentage - currentPercentage;
        }
        if (!GameSettings.animationsEnabled) {
            currentPercentage = targetPercentage;
            GetComponent<Slider>().value = currentPercentage * GetComponent<Slider>().maxValue;
        }
    }

    void Update() {
        if (!GameSettings.animationsEnabled) return;
        if (currentTimer >= delay / GameSettings.animationSpeedFactor) {
            if (Mathf.Abs(targetPercentage - currentPercentage) > 0.001f) {
                ApproachTarget(targetPercentage);
                currentPercentage += currentSpeed;
                GetComponent<Slider>().value = currentPercentage * GetComponent<Slider>().maxValue;
            } else {
                GetComponent<Slider>().onValueChanged.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
                animationFinished = true;
            }
        } else
            currentTimer += Time.unscaledDeltaTime;
        //Debug.Log("SPEED: " + currentSpeed + ", Deviation from target: " + Mathf.Abs(targetPercentage - currentPercentage) / targetPercentage);
        //Debug.Log("TARGET: " + targetPercentage + ", CURRENT_PER: " + currentPercentage + ", VALUE: " + GetComponent<Slider>().value);
    }
}
