using System.Collections;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour {
    //References
    private Referencer rf;
    private Rigidbody2D rb;
    private Transform tf;

    //Setup Variables
    [HideInInspector] public float speedFactor;
    [HideInInspector] public float slowDownFactor;
    private float slowDownTimer;


    //Runtime Variables
    private float rotationValue;
    private Vector2 positionValue;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        rb = GetComponent<Rigidbody2D>();
        tf = transform;
        slowDownFactor = 0f;
    }
    void Update() {
        if (rf.enemyTarget != null) {
            Vector2 moveInput = new Vector2(rf.enemyTarget.position.x - tf.position.x, rf.enemyTarget.position.y - tf.position.y);
            rotationValue = (moveInput.x < 0) ? Vector2.Angle(new Vector2(0, 1), moveInput) : 0 - Vector2.Angle(new Vector2(0, 1), moveInput);
            positionValue = rb.position + moveInput.normalized * Time.deltaTime * speedFactor * (1 - slowDownFactor);
        } else {
            Instantiate(rf.enemyDeathExplosionParticles, tf.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        tf.localRotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MovePosition(positionValue);
    }
    public void startSlowDown() {
        if (slowDownFactor == 0f)
            StartCoroutine(SlowDown());
        else
            slowDownTimer = 5f;
    }
    public IEnumerator SlowDown() {
        slowDownFactor = 0.3f;
        slowDownTimer = 5f;
        while (slowDownTimer > 0f) {
            slowDownTimer -= (1f / Application.targetFrameRate);
            yield return new WaitForFixedUpdate();
        }
        slowDownFactor = 0f;
    }
}
