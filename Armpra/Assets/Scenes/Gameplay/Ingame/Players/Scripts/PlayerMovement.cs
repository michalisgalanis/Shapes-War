using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public GameObject movementJoystickButton;
    public GameObject attackJoystickButton;
    private const int JOYSTICK_TO_VECTOR_FACTOR = 2;

    public SpriteRenderer background;
    private Rigidbody2D rb;
    private Transform tf;

    public float velocityFactor;
    public float acceleration;
    public float currentSpeed;
    private float rotationValue;
    private Vector2 positionValue;
    private Vector2 lastMoveInput;
    private Vector2 totalMovementInput;
    private Vector2 totalAttackInput;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        currentSpeed = 0;
    }

    private void Update() {
        Vector2 movementJoystick = (new Vector2(movementJoystickButton.GetComponent<Transform>().localPosition.x, movementJoystickButton.GetComponent<Transform>().localPosition.y) * JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 attackJoystick = (new Vector2(attackJoystickButton.GetComponent<Transform>().localPosition.x, attackJoystickButton.GetComponent<Transform>().localPosition.y) * JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 keyboardInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Chosing Movement Input
        if (movementJoystick != Vector2.zero && keyboardInput != Vector2.zero) {
            totalMovementInput = (movementJoystick + keyboardInput) / 2;
        } else if (movementJoystick != Vector2.zero) {
            totalMovementInput = movementJoystick;
        } else if (keyboardInput != Vector2.zero) {
            totalMovementInput = keyboardInput;
        } else {
            totalMovementInput = Vector2.zero;
        }

        //Chosing Attack Input
        if (attackJoystick != Vector2.zero && keyboardInput != Vector2.zero) {
            totalAttackInput = (attackJoystick + keyboardInput) / 2;
        } else if (attackJoystick != Vector2.zero) {
            totalAttackInput = attackJoystick;
        } else if (keyboardInput != Vector2.zero) {
            totalAttackInput = keyboardInput;
        } else {
            totalAttackInput = Vector2.zero;
        }

        //Moving Player
        if (totalMovementInput != Vector2.zero) {
            currentSpeed += acceleration;
            if (currentSpeed > velocityFactor) {
                currentSpeed = velocityFactor;
            }

            lastMoveInput = totalMovementInput;
        } else {
            currentSpeed -= acceleration;
            if (currentSpeed < 0) {
                currentSpeed = 0;
            }
        }
        positionValue = rb.position + lastMoveInput.normalized * Time.fixedDeltaTime * currentSpeed;

        //Rotating Player
        if (totalAttackInput != Vector2.zero) {
            rotationValue = Vector2.SignedAngle(new Vector2(0, 1), totalAttackInput);
        }
    }

    private void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
