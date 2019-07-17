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
        if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == -1)
            tf.Rotate(0,0,(135 - tf.rotation.z) * Time.fixedDeltaTime) ;
        else if (Input.GetAxis("Horizontal") == -1 && Input.GetAxis("Vertical") == 1)
            tf.Rotate(0, 0, (45 - tf.rotation.z) * Time.fixedDeltaTime);
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == -1)
            tf.Rotate(0, 0, (-135 - tf.rotation.z) * Time.fixedDeltaTime);
        else if (Input.GetAxis("Horizontal") == 1 && Input.GetAxis("Vertical") == 1)
            tf.Rotate(0, 0, (-45 - tf.rotation.z) * Time.fixedDeltaTime);
        else tf.Rotate(0, 0, 0 * Time.fixedDeltaTime);
    }
}
