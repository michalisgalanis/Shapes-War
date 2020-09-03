using UnityEngine;

public class DualZoneJoystick : MonoBehaviour {
    public GameObject aimJoystick;
    private Vector3 fingerPosition;
    public float smallRadius;

    private void Start() {
        transform.localPosition = Vector3.zero;
        aimJoystick.SetActive(false);
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0) {
            fingerPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);// - (transform.parent.position);
            fingerPosition = new Vector3(fingerPosition.x, fingerPosition.y, 0f);
            float distance = Vector2.Distance(fingerPosition, transform.parent.position);
            //Debug.Log("FingerPosition = " + fingerPosition + "Distance = " + distance);
            if (distance < smallRadius) {
                aimJoystick.SetActive(false);
                transform.position = fingerPosition;
            } else if (distance > smallRadius + 0.34f) {
                if (aimJoystick.activeInHierarchy == false)
                    aimJoystick.SetActive(true);
                MoveAimJoystick();
            }
        } else {
            transform.localPosition = Vector2.zero;
            aimJoystick.SetActive(false);
        }
    }

    private void MoveAimJoystick() {
        aimJoystick.transform.position = fingerPosition;
        Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(aimJoystick.transform.localPosition.x, aimJoystick.transform.localPosition.y, 0), 226.5f);
        aimJoystick.transform.localPosition = newTransform;
    }
}