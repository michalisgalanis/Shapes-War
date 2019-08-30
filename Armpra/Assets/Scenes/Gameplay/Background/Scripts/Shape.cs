using UnityEngine;

public class Shape : MonoBehaviour {
    //References
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    //Setup Variables
    private const float positionSpeedFactor = Constants.Gameplay.Background.SHAPES_POSITION_SPEED_FACTOR;
    private const float rotationSpeedFactor = Constants.Gameplay.Background.SHAPES_ROTATION_SPEED_FACTOR;

    //Position Variables
    private Vector2 positionDirection;
    private Vector2 positionValue;
    //Rotation Variables
    private Vector2 rotationDirection;
    private float rotationValue;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Start() {
        sr.sortingOrder = Random.Range(-1, 1);
        transform.Rotate(0, 0, 0);
        GenerateRandomStats();
    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag(Constants.Tags.MAP_BOUNDS_TAG)) {
            GenerateRandomStats();
        }
    }

    private void Update() {
        rotationValue = Vector2.SignedAngle(new Vector2(0, 1), rotationDirection) * rotationSpeedFactor * Time.deltaTime;
        positionValue = rb.position + positionDirection.normalized * Time.deltaTime * positionSpeedFactor;
    }

    private void FixedUpdate() {
        rb.MovePosition(positionValue);
        transform.Rotate(0, 0, rotationValue);
    }

    private void GenerateRandomStats() {
        //Position
        float positionRandX = Random.Range(-1f, 1f);
        float positionRandY = Random.Range(-1f, 1f);
        positionDirection = new Vector2(positionRandX, positionRandY);
        //Rotation
        float rotationRandX = Random.Range(-1f, 1f);
        float rotationRandY = Random.Range(-1f, 1f);
        rotationDirection = new Vector2(rotationRandX, rotationRandY);
    }
}
