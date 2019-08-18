using UnityEngine;

public class JoystickMovement : MonoBehaviour {
    private Vector2 pointA;
    private bool heldDown;
    public bool leftController;

    private void Start() {
        heldDown = false;
        transform.localPosition = Vector3.zero;
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            if (leftController) {
                if (Input.mousePosition.x <= 400 && Input.mousePosition.y <= 400 || heldDown) {
                    pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                    transform.position = pointA;
                    heldDown = true;
                }
            } else {
                if (Input.mousePosition.x >= 680 && Input.mousePosition.y <= 400 || heldDown) {
                    pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
                    transform.position = pointA;
                    heldDown = true;
                }
            }

        } else {
            heldDown = false;
            transform.localPosition = Vector3.zero;
        }
    }
}