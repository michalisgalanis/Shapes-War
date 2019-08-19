using UnityEngine;

public class JoystickMovement : MonoBehaviour {
    private enum direction { LEFT, CENTER, RIGHT }

    private Vector3 movementJoystickVector;
    private Vector3 attackJoystickVector;

    public bool movementJoystick;

    private Touch firstFinger;
    private Touch secondFinger;

    private static float HEIGHT_THRESHOLD;
    private static float LEFT_WIDTH_THRESHOLD;
    private static float RIGHT_WIDTH_THRESHOLD;

    private void Start() {
        HEIGHT_THRESHOLD = Camera.main.pixelHeight * 0.3f;
        LEFT_WIDTH_THRESHOLD = Camera.main.pixelWidth * 0.3f;
        RIGHT_WIDTH_THRESHOLD = Camera.main.pixelWidth - HEIGHT_THRESHOLD;
    }

    private void Update() {
        bool firstFingerDown = Input.GetMouseButton(0);
        bool secondFingerDown = Input.GetMouseButton(1);

        for (int i = 0; i < Input.touchCount; i++) {
            if (i == 0) firstFinger = Input.GetTouch(i);
            else if (i == 1) secondFinger = Input.GetTouch(i);
        }

        direction firstFingerDirection = (firstFingerDown) ? AnalyzeFinger(firstFinger) : direction.CENTER;
        direction secondFingerDirection = (secondFingerDown) ? AnalyzeFinger(secondFinger) : direction.CENTER;

        if (firstFingerDown) {
            if (firstFingerDirection == direction.LEFT) {
                movementJoystickVector = Camera.main.ScreenToWorldPoint(firstFinger.position);
                if (movementJoystick) {
                    transform.position = movementJoystickVector;
                    Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), 0.5f);
                    transform.localPosition = newTransform;
                }

                if (secondFingerDirection == direction.RIGHT) {
                    attackJoystickVector = Camera.main.ScreenToWorldPoint(secondFinger.position);
                    if (!movementJoystick) {
                        transform.position = attackJoystickVector;
                        Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), 0.5f);
                        transform.localPosition = newTransform;
                    }
                }
            }
            if (firstFingerDirection == direction.RIGHT) {
                attackJoystickVector = Camera.main.ScreenToWorldPoint(firstFinger.position);
                if (!movementJoystick) {
                    transform.position = attackJoystickVector;
                    Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), 0.5f);
                    transform.localPosition = newTransform;
                }

                if (secondFingerDirection == direction.LEFT) {
                    movementJoystickVector = Camera.main.ScreenToWorldPoint(secondFinger.position);
                    if (movementJoystick) {
                        transform.position = movementJoystickVector;
                        Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), 0.5f);
                        transform.localPosition = newTransform;
                    }
                }
            }
        } else {
            transform.localPosition = Vector3.zero;
        }
    }

    private direction AnalyzeFinger(Touch finger) {
        direction tempDirection;
        if (finger.position.x <= LEFT_WIDTH_THRESHOLD && finger.position.y <= HEIGHT_THRESHOLD) tempDirection = direction.LEFT;
        else if (finger.position.x >= RIGHT_WIDTH_THRESHOLD && finger.position.y <= HEIGHT_THRESHOLD) tempDirection = direction.RIGHT;
        else tempDirection = direction.CENTER;
        return tempDirection;
    }
}