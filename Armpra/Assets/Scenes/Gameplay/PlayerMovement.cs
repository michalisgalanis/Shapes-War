using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Transform tf;
    private Vector2 moveVelocity;

    private const float FACE_UP = 0;
    private const float FACE_UP_LEFT = 45;
    private const float FACE_LEFT = 90;
    private const float FACE_DOWN_LEFT = 135;
    private const float FACE_DOWN = 180;
    private const float FACE_DOWN_RIGHT = -135;
    private const float FACE_RIGHT = -90;
    private const float FACE_UP_RIGHT = -45;

    private float rotationValue;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 1)
            rotationValue = FACE_UP;
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == 1)
            rotationValue = FACE_UP_LEFT;
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == 0)
            rotationValue = FACE_LEFT;
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == -1)
            rotationValue = FACE_DOWN_LEFT;
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == -1)
            rotationValue = FACE_DOWN;
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == -1)
            rotationValue = FACE_DOWN_RIGHT;
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == 0)
            rotationValue = FACE_RIGHT;
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == 1)
            rotationValue = FACE_UP_RIGHT;
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
    }
}
