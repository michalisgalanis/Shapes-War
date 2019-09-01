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
    public float zoomAcceleration;
    
    //Runtime Variables
    private Vector3 desiredPosition;
    private float lastPositionXNormal;
    private float lastPositionYNormal;
    private float lastPositionXZoomedOut;
    private float lastPositionYZoomedOut;
    [HideInInspector] public bool zoomOutEffectActive;
    [HideInInspector] public bool zoomDirection; //0 for in, 1 for out
    public float maxZoomOutFactor;
    private float currentZoomOutFactor = 0f;

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

    private void Update() {
        if (zoomOutEffectActive) {
            currentZoomOutFactor = Mathf.Min(currentZoomOutFactor + zoomAcceleration, maxZoomOutFactor);
        } else {
            currentZoomOutFactor = Mathf.Max(currentZoomOutFactor - zoomAcceleration, 0);
        }
        rf.camScript.orthographicSize = Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET * (1 + currentZoomOutFactor);

        if (player != null) {
            managePositions();
        }
        //Debug.Log(Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET + " ,  " + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET + " || " + Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET * (1 + maxZoomOutFactor) + " , " + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET * (1 + maxZoomOutFactor));
        //Debug.Log(Constants.Functions.getRoundedFloat(lastPositionXNormal, 0) + " , " + Constants.Functions.getRoundedFloat(lastPositionYNormal, 0) + "  ||  " + Constants.Functions.getRoundedFloat(lastPositionXZoomedOut, 0) + " , " + Constants.Functions.getRoundedFloat(lastPositionYZoomedOut, 0));
    }

    private void FixedUpdate() {
        if (!zoomOutEffectActive)
            desiredPosition = new Vector3(lastPositionXNormal, lastPositionYNormal, -10);
        else
            desiredPosition = new Vector3(lastPositionXZoomedOut, lastPositionYZoomedOut, -10);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    private void managePositions() {
        if (player.transform.position.x + Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET <= MAX_BORDER && player.transform.position.x - Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET >= MIN_BORDER)
            lastPositionXNormal = player.transform.position.x;
        if (player.transform.position.y + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET <= MAX_BORDER && player.transform.position.y - Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET >= MIN_BORDER)
            lastPositionYNormal = player.transform.position.y;
        if (player.transform.position.x + Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET * (1 + maxZoomOutFactor) <= MAX_BORDER && player.transform.position.x - Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET * (1 + maxZoomOutFactor) >= MIN_BORDER)
            lastPositionXZoomedOut = player.transform.position.x;
        if (player.transform.position.y + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET * (1 + maxZoomOutFactor) <= MAX_BORDER && player.transform.position.y - Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET * (1 + maxZoomOutFactor) >= MIN_BORDER)
            lastPositionYZoomedOut = player.transform.position.y;
    }
}
