using UnityEngine;

public class Shape : MonoBehaviour {
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

    public void Start() {
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
    }

    private void Update() {
        rotationValue = Vector2.SignedAngle(new Vector2(0, 1), rotationDirection) * rotationSpeedFactor * Time.deltaTime;
        positionValue = rb.position + positionDirection.normalized * Time.deltaTime * positionSpeedFactor;
    }

    private void FixedUpdate() {
        rb.MovePosition(positionValue);
        tf.Rotate(0, 0, rotationValue);
    }
}
