using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float horizontalMove;
    public float verticalMove;

    

    private Rigidbody2D rb;
    private Transform tf;
    private Vector2 moveVelocity;


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        tf.Rotate(moveVelocity);
    }
}
