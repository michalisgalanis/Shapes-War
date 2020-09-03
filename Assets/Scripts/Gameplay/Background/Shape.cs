using UnityEngine;

public class Shape : MonoBehaviour {
    //References
    private Rigidbody2D rb;

    //Setup Variables
    [HideInInspector] public float positionSpeedFactor;
    [HideInInspector] public float rotationSpeedFactor;
    //Random Number Generator
    private float maxVelocity = 0.5f;
    private float minVelocity = 0.2f;
    private float maxAngularVelocity = 40f;
    private float minAngularVelocity = 20f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        float scaleRandom = Random.Range(0.4f, 0.4f + Utility.Gameplay.Background.SHAPES_SIZE_VAR);
        transform.localScale = new Vector3(scaleRandom, scaleRandom, 0);
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        GetComponent<Shape>().GenerateRandomStats();
        positionSpeedFactor = /*bossLevel ? 0f :*/ Utility.Gameplay.Background.SHAPES_POSITION_SPEED_FACTOR;
        rotationSpeedFactor = /*bossLevel ? 0f :*/ Utility.Gameplay.Background.SHAPES_ROTATION_SPEED_FACTOR;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag(Utility.Tags.MAP_BOUNDS_TAG)) {
            GenerateRandomStats();
        }
    }

    public void GenerateRandomStats() {
        rb.velocity = new Vector2(RandomNumberGenerator(minVelocity, maxVelocity), RandomNumberGenerator(minVelocity, maxVelocity));
        rb.angularVelocity = RandomNumberGenerator(minAngularVelocity, maxAngularVelocity);
    }

    public void SetColor(bool bossLevel) {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Utility.Functions.GeneralFunctions.generateColor(Random.Range(0f, 1f), bossLevel ? 0f : Random.Range(0.4f, 0.6f), bossLevel ? 0.08f : Random.Range(0.4f, 0.55f), bossLevel ? 0.5f : Random.Range(0.1f, 0.25f));
        sr.sortingOrder = Random.Range(-1, 1);
    }

    private float RandomNumberGenerator(float min, float max) {
        int sign = Random.Range(0, 2) * 2 - 1;
        float randomNumber = Random.Range(min, max);
        randomNumber *= sign;
        return randomNumber;
    }
}
