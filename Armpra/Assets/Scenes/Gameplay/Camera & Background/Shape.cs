using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speedFactor;
    private Vector2 positionValue;
    private Vector2 direction;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        float xRand = Random.Range(-1f, 1f);
        float yRand = Random.Range(-1f, 1f);
        direction = new Vector2(xRand, yRand);
    }


    void Update(){
        
        positionValue = rb.position + direction.normalized * Time.fixedDeltaTime * speedFactor;
    }

    void FixedUpdate()
    {
        rb.MovePosition(positionValue);
    }
}
