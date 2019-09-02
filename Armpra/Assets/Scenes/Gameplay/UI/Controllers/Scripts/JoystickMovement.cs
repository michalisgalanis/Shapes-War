using UnityEngine;

public class JoystickMovement : MonoBehaviour {
    private enum direction {
        LEFT, CENTER, RIGHT
    }
    public bool movementJoystick;

    [HideInInspector] public Vector3 movementJoystickVector;
    [HideInInspector] public Vector3 attackJoystickVector;

    private Touch firstFinger;
    private Touch secondFinger;

    private static float HEIGHT_THRESHOLD;
    private static float LEFT_WIDTH_THRESHOLD;
    private static float RIGHT_WIDTH_THRESHOLD;
    private static float CENTER_WIDTH_THRESHOLD;

    public void Start() {
        HEIGHT_THRESHOLD = Camera.main.pixelHeight * 0.35f;
        LEFT_WIDTH_THRESHOLD = Camera.main.pixelWidth * 0.5f;
        RIGHT_WIDTH_THRESHOLD = Camera.main.pixelWidth - LEFT_WIDTH_THRESHOLD;
        CENTER_WIDTH_THRESHOLD = Camera.main.pixelWidth * 0.1f;
    }

    private void FixedUpdate() {
        for (int i = 0; i < Input.touchCount; i++) {
            if (i == 0) {
                firstFinger = Input.GetTouch(i);
            } else if (i == 1) {
                secondFinger = Input.GetTouch(i);
            }
        }
        if (Input.touchCount > 0) {
            if (movementJoystick) {
                if (AnalyzeFinger(firstFinger) == direction.LEFT) {
                    MoveJoysticks(firstFinger);
                } else if (Input.touchCount > 1 && (AnalyzeFinger(secondFinger) == direction.LEFT)) {
                    MoveJoysticks(secondFinger);
                } else {
                    transform.localPosition = Vector3.zero;
                }
            } else if (AnalyzeFinger(firstFinger) == direction.RIGHT) {
                MoveJoysticks(firstFinger);
            } else if (Input.touchCount > 1 && (AnalyzeFinger(secondFinger) == direction.RIGHT)) {
                MoveJoysticks(secondFinger);
            } else {
                transform.localPosition = Vector3.zero;
            }
        } else {
            transform.localPosition = Vector3.zero;
        }
    }

    private direction AnalyzeFinger(Touch finger) {
        direction tempDirection;
        if (finger.position.x >= (Camera.main.pixelWidth / 2f) - CENTER_WIDTH_THRESHOLD && finger.position.x <= (Camera.main.pixelWidth / 2f) + CENTER_WIDTH_THRESHOLD && finger.position.y <= Camera.main.pixelHeight * 0.1f) {
            tempDirection = direction.CENTER;
        } else if (finger.position.x <= LEFT_WIDTH_THRESHOLD && finger.position.y <= HEIGHT_THRESHOLD) {
            tempDirection = direction.LEFT;
        } else if (finger.position.x >= RIGHT_WIDTH_THRESHOLD && finger.position.y <= HEIGHT_THRESHOLD) {
            tempDirection = direction.RIGHT;
        } else {
            tempDirection = direction.CENTER;
        }
        return tempDirection;
    }

    private void MoveJoysticks(Touch finger) {
        transform.position = Camera.main.ScreenToWorldPoint(finger.position);
        Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), 110f);
        transform.localPosition = newTransform;
    }
}
