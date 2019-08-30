using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour {
    //References
    private Referencer rf;
    private GameObject player;
    private Rigidbody2D rb;

    //Setup Variables
    public float speedFactor;

    //Runtime Variables
    private float rotationValue;
    private Vector2 positionValue;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        player = rf.player;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update() {
        if (player != null) {
            Vector2 moveInput = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            rotationValue = (moveInput.x < 0) ? Vector2.Angle(new Vector2(0, 1), moveInput) : 0 - Vector2.Angle(new Vector2(0, 1), moveInput);
            positionValue = rb.position + moveInput.normalized * Time.fixedDeltaTime * speedFactor;
        } else {
            Instantiate(rf.enemyDeathExplosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
}
