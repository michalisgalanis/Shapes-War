using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject movementJoystickButton;
    public GameObject attackJoystickButton;
    private const int JOYSTICK_TO_VECTOR_FACTOR = 2;

    public SpriteRenderer background;
    private Rigidbody2D rb;
    private Transform tf;

    public float velocityFactor;
    public float acceleration;
    private float currentSpeed;
    private float rotationValue;
    private Vector2 positionValue;
    private Vector2 lastMoveInput;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        currentSpeed = 0;
    }

    void Update() {
        Vector2 joystickInput = (new Vector2(movementJoystickButton.GetComponent<Transform>().localPosition.x, movementJoystickButton.GetComponent<Transform>().localPosition.y) * JOYSTICK_TO_VECTOR_FACTOR).normalized;
        Vector2 keyboardInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 totalInput;

        //Chosing Input
        if (joystickInput != Vector2.zero && keyboardInput != Vector2.zero)
            totalInput = (joystickInput + keyboardInput) / 2;
        else if (joystickInput != Vector2.zero)
            totalInput = joystickInput;
        else if (keyboardInput != Vector2.zero)
            totalInput = keyboardInput;
        else totalInput = Vector2.zero;

        /*Debug.Log(joystickInput.x);
        Debug.Log(joystickInput.y);*/

        if (totalInput != Vector2.zero){
            rotationValue = Vector2.SignedAngle(new Vector2(0, 1), totalInput);
            currentSpeed += acceleration;
            if (currentSpeed > velocityFactor) currentSpeed = velocityFactor;
            lastMoveInput = totalInput;
        } else {
            currentSpeed -= acceleration;
            if (currentSpeed < 0) currentSpeed = 0;
        }
        positionValue = rb.position + lastMoveInput.normalized * Time.fixedDeltaTime * currentSpeed;
    }

    void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
