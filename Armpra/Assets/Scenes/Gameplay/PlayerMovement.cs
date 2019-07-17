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
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            positionValue = rb.position + moveInput.normalized * Time.fixedDeltaTime * speedFactor;
            if (moveInput.x == 0 && moveInput.y == 1)
                rotationValue = 0;
            else if (moveInput.x == -1 && moveInput.y == 1)
                rotationValue = 45;
            else if (moveInput.x == -1 && moveInput.y == 0)
                rotationValue = 90;
            else if (moveInput.x == -1 && moveInput.y == -1)
                rotationValue = 135;
            else if (moveInput.x == 0 && moveInput.y == -1)
                rotationValue = 180;
            else if (moveInput.x == 1 && moveInput.y == -1)
                rotationValue = -135;
            else if (moveInput.x == 1 && moveInput.y == 0)
                rotationValue = -90;
            else if (moveInput.x == 1 && moveInput.y == 1)
                rotationValue = -45;
        }
        
    }

    void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
