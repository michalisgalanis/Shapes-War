using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour {
    public bool player;

    private bool locked = false;

    public void Hit(float intensity, float acceleration) {
        if (!locked) {
            locked = true;
            StartCoroutine(StartEffect(intensity, acceleration));
        }
    }

    private IEnumerator StartEffect(float intensity, float acceleration) { //default 1f, 0.1f
        float initialValue;
        float targetValue;
        if (player) {
            Color currentColor = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color;
            Color.RGBToHSV(currentColor, out _, out _, out initialValue);
        } else {
            Color currentColor = transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color;
            Color.RGBToHSV(currentColor, out _, out _, out initialValue);
        }
        targetValue = Mathf.Clamp((intensity + 1f) * initialValue, 0.3f, 1f);
        float accelerator = acceleration * (targetValue - initialValue);
        float currentValue = initialValue; 
        float currentSpeed = 0f;
        bool decelerating = false;
        while (true) {
            //Debug.Log("[" + initialValue + "] ----> " + currentValue +  " (" + currentSpeed + "/" + acceleration + ") ----> [" + targetValue + "]");
            if (!decelerating && currentValue >= targetValue) {
                currentSpeed = -currentSpeed;
                accelerator *= 0.6f;
                decelerating = true;
            }
            currentSpeed += accelerator;
            currentValue += currentSpeed;
            if (decelerating && currentSpeed >= 0) {
                accelerator = 0;
                currentSpeed = 0;
                currentValue = initialValue;
            }
            if (player) {
                for (int i = 0; i < transform.GetChild(0).childCount; i++) {
                    Color currentColor = transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().color;
                    Color.RGBToHSV(currentColor, out float H, out float S, out _); float A = currentColor.a;
                    transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().color = Utility.Functions.GeneralFunctions.generateColor(H, S, currentValue, A);
                }
            } else {
                for (int i = 0; i < transform.GetChild(2).childCount; i++) {
                    Color currentColor = transform.GetChild(2).GetChild(i).GetComponent<SpriteRenderer>().color;
                    Color.RGBToHSV(currentColor, out float H, out float S, out _); float A = currentColor.a;
                    transform.GetChild(2).GetChild(i).GetComponent<SpriteRenderer>().color = Utility.Functions.GeneralFunctions.generateColor(H, S, currentValue, A);
                }
            }
            if (currentValue == initialValue) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        locked = false;
    }
}
