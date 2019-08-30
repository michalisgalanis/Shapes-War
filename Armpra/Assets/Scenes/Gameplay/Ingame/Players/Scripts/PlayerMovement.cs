using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //References
    private Referencer rf;
    private Rigidbody2D rb;

    //Setup Variables
    [HideInInspector] public float velocityFactor;
    private const float acceleration = Constants.Gameplay.Player.PLAYER_ACCELERATION;

    //Runtime Variables
    [HideInInspector] public float currentSpeed = 0f;
    private float rotationValue;
    private Vector2 positionValue;
    private Vector2 lastMoveInput;
    private Vector2 totalMovementInput;
    private Vector2 totalAttackInput;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update() {
        Vector2 movementJoystick = (new Vector2(rf.movementJoystickUIInnerCircle.localPosition.x, rf.movementJoystickUIInnerCircle.localPosition.y) * Constants.Gameplay.Joystick.JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 attackJoystick = (new Vector2(rf.attackJoystickUIInnerCircle.localPosition.x, rf.attackJoystickUIInnerCircle.localPosition.y) * Constants.Gameplay.Joystick.JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 keyboardInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Chosing Movement Input
        if (movementJoystick != Vector2.zero) {
            totalMovementInput = movementJoystick;
        } else if (keyboardInput != Vector2.zero) {
            totalMovementInput = keyboardInput;
        } else {
            totalMovementInput = Vector2.zero;
        }

        //Chosing Attack Input
        if (attackJoystick != Vector2.zero) {
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

    public void FixedUpdate() {
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }

    public void resetMovement() {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localPosition = Vector3.zero;
    }
}
