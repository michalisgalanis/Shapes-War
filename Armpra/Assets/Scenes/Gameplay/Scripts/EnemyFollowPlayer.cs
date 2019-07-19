using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    private Transform tf;

    public float speedFactor;
    private float rotationValue;
    private Vector2 positionValue;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    
    void Update(){
        Vector2 moveInput = new Vector2(player.transform.position.x - tf.position.x, player.transform.position.y - tf.position.y);
        rotationValue = (moveInput.x < 0) ? Vector2.Angle(new Vector2(0, 1), moveInput) : 0 - Vector2.Angle(new Vector2(0, 1), moveInput);
        positionValue = rb.position + moveInput.normalized * Time.fixedDeltaTime * speedFactor;
    }

    void FixedUpdate(){
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
