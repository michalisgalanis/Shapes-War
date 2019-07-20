using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer background;
    private Rigidbody2D rb;
    private Transform tf;

    public float speedFactor;
    private float rotationValue;
    private Vector2 positionValue;
    private const float PIXEL_TO_POSITION_FACTOR = 200f;
    private float horizontalMapBound, verticalMapBound;
    private float horizontalPlayerSize, verticalPlayerSize;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        horizontalMapBound = background.sprite.texture.width / PIXEL_TO_POSITION_FACTOR;
        verticalMapBound = background.sprite.texture.height / PIXEL_TO_POSITION_FACTOR;
        horizontalPlayerSize = GetComponent<SpriteRenderer>().sprite.texture.width / PIXEL_TO_POSITION_FACTOR * tf.localScale.x;
        verticalPlayerSize = GetComponent<SpriteRenderer>().sprite.texture.height / PIXEL_TO_POSITION_FACTOR * tf.localScale.y;
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput.x != 0 || moveInput.y != 0) rotationValue = Vector2.SignedAngle(new Vector2(0, 1), moveInput);
        positionValue = rb.position + moveInput.normalized * Time.fixedDeltaTime * speedFactor;
        positionValue.x = Mathf.Clamp(positionValue.x, 0 - horizontalMapBound + horizontalPlayerSize, horizontalMapBound - horizontalPlayerSize);
        positionValue.y = Mathf.Clamp(positionValue.y, 0 - verticalMapBound + verticalPlayerSize, verticalMapBound - verticalPlayerSize);
    }

    void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
