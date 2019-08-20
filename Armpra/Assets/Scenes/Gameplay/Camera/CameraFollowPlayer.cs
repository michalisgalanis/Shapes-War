using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    private GameObject player;
    public GameObject background;
    

    private Vector3 desiredPosition;
    public Vector3 offset;
    public float smoothSpeed;

    //constants
    private static float MIN_BORDER;
    private static float MAX_BORDER;
    private static float HORIZONTAL_CAMERA_OFFSET;
    private static float VERTICAL_CAMERA_OFFSET;

    private float lastPositionX;
    private float lastPositionY;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        MIN_BORDER = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit) * (-0.5f);
        MAX_BORDER = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit) * 0.5f;
        VERTICAL_CAMERA_OFFSET = cam.orthographicSize;
        HORIZONTAL_CAMERA_OFFSET = cam.orthographicSize * cam.aspect;
    }

    private void FixedUpdate() {
        if (player != null) {
            if (followX()) {
                lastPositionX = player.transform.position.x;
            }

            if (followY()) {
                lastPositionY = player.transform.position.y;
            }

            desiredPosition = new Vector3(lastPositionX, lastPositionY, -10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }

    private bool followX() {
        return (player.transform.position.x + HORIZONTAL_CAMERA_OFFSET <= MAX_BORDER) && (player.transform.position.x - HORIZONTAL_CAMERA_OFFSET >= MIN_BORDER);
    }

    private bool followY() {
        return (player.transform.position.y + VERTICAL_CAMERA_OFFSET <= MAX_BORDER) && (player.transform.position.y - VERTICAL_CAMERA_OFFSET >= MIN_BORDER);
    }
}
