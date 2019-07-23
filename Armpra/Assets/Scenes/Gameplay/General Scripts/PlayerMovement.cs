using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput != Vector2.zero){
            rotationValue = Vector2.SignedAngle(new Vector2(0, 1), moveInput);
            currentSpeed += acceleration;
            if (currentSpeed > velocityFactor) currentSpeed = velocityFactor;
            lastMoveInput = moveInput;
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
