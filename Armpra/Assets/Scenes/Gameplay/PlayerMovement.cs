using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;

    public float speedFactor;
    private float rotationValue;
    private Vector2 positionValue; 

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput.x == -1 && moveInput.y != 0)
            rotationValue = Vector2.Angle(new Vector2(0, 1), moveInput);
        else if (moveInput.x == 1 && moveInput.y != 0)
            rotationValue = 0 - Vector2.Angle(new Vector2(0, 1), moveInput);
        else if (moveInput.y == -1)
            rotationValue = 180;
        else if (moveInput.y == 1)
            rotationValue = 0;
        positionValue = rb.position + moveInput.normalized * Time.fixedDeltaTime * speedFactor;
    }

    void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
