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
    private const float PIXEL_TO_POSITION_FACTOR = 2048 / 19.2f;
    private float horizontalBound;
    private float verticalBound;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        horizontalBound = background.sprite.texture.width / PIXEL_TO_POSITION_FACTOR;
        verticalBound = background.sprite.texture.height / PIXEL_TO_POSITION_FACTOR;
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
        positionValue.x = Mathf.Clamp(positionValue.x, 0 - horizontalBound, horizontalBound);
        positionValue.y = Mathf.Clamp(positionValue.y, 0 - verticalBound, verticalBound);
    }

    void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
