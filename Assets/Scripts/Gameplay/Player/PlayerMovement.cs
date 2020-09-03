using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //References
    private Referencer rf;
    private Rigidbody2D rb;
    private Transform tf;

    //Setup Variables
    [HideInInspector] public float velocityFactor;
    private const float acceleration = Utility.Gameplay.Player.PLAYER_ACCELERATION;

    //Runtime Variables
    [HideInInspector] public float currentSpeed = 0f;
    private float rotationValue;
    private Vector2 positionValue;
    private Vector2 lastMoveInput;
    private Vector2 totalMovementInput;
    private Vector2 totalAttackInput;

    private static float MIN_BORDER;
    private static float MAX_BORDER;

    private GameObject movementJoystick;
    private GameObject attackJoystick;

    [HideInInspector] public Utility.Gameplay.Controls.controlType controlType;
    public float slowDownFactor;
    private float slowDownTimer;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        rb = GetComponent<Rigidbody2D>();
        tf = transform;
    }

    void Start() {
        MIN_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
        slowDownFactor = 0f;
    }


    public void SetupControls(Utility.Gameplay.Controls.controlType controlType) {
        this.controlType = controlType;
        switch (this.controlType) {
            case Utility.Gameplay.Controls.controlType.NORMAL_JOYSTICK:
            movementJoystick = rf.normalJoystick.transform.GetChild(0).GetChild(2).gameObject;
            attackJoystick = rf.normalJoystick.transform.GetChild(1).GetChild(2).gameObject;
            break;
            case Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK:
            movementJoystick = rf.dualZoneJoystick.transform.GetChild(2).gameObject;
            attackJoystick = rf.dualZoneJoystick.transform.GetChild(3).gameObject;
            break;
        }
    }


    public void Update() {
        Vector2 movementJoystickVector = (new Vector2(movementJoystick.transform.localPosition.x, movementJoystick.transform.localPosition.y) * Utility.Gameplay.Controls.JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 attackJoystickVector = (new Vector2(attackJoystick.transform.localPosition.x, attackJoystick.transform.localPosition.y) * Utility.Gameplay.Controls.JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 keyboardInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Chosing Movement Input
        if (movementJoystickVector != Vector2.zero) {
            totalMovementInput = movementJoystickVector;
        } else if (keyboardInput != Vector2.zero) {
            totalMovementInput = keyboardInput;
        } else {
            totalMovementInput = Vector2.zero;
        }

        //Chosing Attack Input
        if (attackJoystickVector != Vector2.zero) {
            totalAttackInput = attackJoystickVector;
        } else if (keyboardInput != Vector2.zero) {
            totalAttackInput = keyboardInput;
        } else {
            totalAttackInput = Vector2.zero;
        }

        //Moving Player
        if (totalMovementInput != Vector2.zero) {
            currentSpeed = Mathf.Min(currentSpeed + acceleration, velocityFactor);
            lastMoveInput = totalMovementInput;
        } else {
            currentSpeed = Mathf.Max(currentSpeed - acceleration, 0);
        }
        positionValue = rb.position + lastMoveInput.normalized * Time.deltaTime * currentSpeed;

        //Rotating Player
        if (totalAttackInput != Vector2.zero) {
            rotationValue = Vector2.SignedAngle(new Vector2(0, (controlType == Utility.Gameplay.Controls.controlType.DUAL_ZONE_JOYSTICK && (GameSettings.platform == RuntimePlatform.Android)) ? -1 : 1), totalAttackInput);
        }
    }

    public void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }

    public void resetMovement() {
        //rf.player.transform.GetComponentInChildren<TrailRenderer>(true).time = 0f;
        tf.localRotation = Quaternion.identity;
        tf.localPosition = Vector3.zero;
        //rf.player.transform.GetComponentInChildren<TrailRenderer>(true).time = 0.3f;
    }

    public void generateRandomMovement() {
        //rf.player.transform.GetComponentInChildren<TrailRenderer>(true).time = 0f;
        tf.localPosition = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
        //rf.player.transform.GetComponentInChildren<TrailRenderer>(true).time = 0.3f;
    }
    public void startSlowDown() {
        if (slowDownFactor == 0f)
            StartCoroutine(SlowDown());
        else
            slowDownTimer = 5f;
    }
    private IEnumerator SlowDown() {
        slowDownFactor = 0.3f;
        rf.ps.EstimateStat(Utility.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED);
        slowDownTimer = 5f;
        while (slowDownTimer > 0f) {
            slowDownTimer -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }
        slowDownFactor = 0f;
        rf.ps.EstimateStat(Utility.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED);
    }
}
