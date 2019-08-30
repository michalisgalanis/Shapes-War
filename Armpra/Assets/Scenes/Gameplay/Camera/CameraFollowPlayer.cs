using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    //References
    private Referencer rf;
    private GameObject player;

    //Constants
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    //Setup Variables
    private readonly float smoothSpeed = Constants.Gameplay.Camera.SMOOTH_SPEED;

    //Runtime Variables
    private Vector3 desiredPosition;
    private float lastPositionX;
    private float lastPositionY;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        player = rf.player;
        Camera cam = rf.camScript;
        Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET = cam.orthographicSize * cam.aspect;
        Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET = cam.orthographicSize;
    }

    public void Start() {
        MIN_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * 0.5f;
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
        return (player.transform.position.x + Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET <= MAX_BORDER) && (player.transform.position.x - Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET >= MIN_BORDER);
    }

    private bool followY() {
        return (player.transform.position.y + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET <= MAX_BORDER) && (player.transform.position.y - Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET >= MIN_BORDER);
    }
}
