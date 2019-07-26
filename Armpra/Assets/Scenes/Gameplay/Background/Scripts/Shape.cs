using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tf;

    //Position Variables
    public float positionSpeedFactor;
    private Vector2 positionDirection;
    private Vector2 positionValue;
    private float positionRandX;
    private float positionRandY;
    //Rotation Variables
    public float rotationSpeedFactor;
    private Vector2 rotationDirection;
    private float rotationValue;
    private float rotationRandX;
    private float rotationRandY;

    void Start(){
        //Components
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        //Position
        positionRandX = Random.Range(-1f, 1f);
        positionRandY = Random.Range(-1f, 1f);
        positionDirection = new Vector2(positionRandX, positionRandY);
        //Rotation
        rotationRandX = Random.Range(-1f, 1f);
        rotationRandY = Random.Range(-1f, 1f);
        rotationDirection = new Vector2(rotationRandX, rotationRandY);
        //Ignore Collision
        Physics2D.IgnoreLayerCollision(8, 5);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(8, 11);
        Physics2D.IgnoreLayerCollision(8, 12);
        Physics2D.IgnoreLayerCollision(8, 13);

    }

    void Update(){
        rotationValue = Vector2.SignedAngle(new Vector2(0, 1), rotationDirection) * rotationSpeedFactor  * Time.fixedDeltaTime;
        positionValue = rb.position + positionDirection.normalized * Time.fixedDeltaTime * positionSpeedFactor;
    }

    void FixedUpdate()
    {
        rb.MovePosition(positionValue);
        tf.Rotate(0, 0, rotationValue);
    }
}
