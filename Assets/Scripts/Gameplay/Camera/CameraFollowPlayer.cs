using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
    //References
    private Referencer rf;
    private GameObject player;

    //Constants
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    //Setup Variables
    [HideInInspector] public float smoothSpeed = Utility.Gameplay.Camera.SMOOTH_SPEED;
    public float zoomAcceleration;

    //Runtime Variables
    private Vector3 desiredPosition;
    [HideInInspector] public float lastPositionX;
    [HideInInspector] public float lastPositionY;

    [HideInInspector] public bool zoomOutEffectActive;
    public float maxZoomOutFactor;
    private float currentZoomOutFactor = 0f;

    private float currentShakeTimer = 0f;
    private float shakeDuration = 0f;
    private float shakeIntensity = 0f;
    private bool shakeEnabled = true;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        player = rf.player;
        Camera cam = rf.cam;
        Utility.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET = cam.orthographicSize * cam.aspect;
        Utility.Gameplay.Camera.VERTICAL_CAMERA_OFFSET = cam.orthographicSize;
    }

    public void Start() {
        MIN_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
    }

    private void LateUpdate() {
        if (player == null) return;
        currentZoomOutFactor = zoomOutEffectActive ? Mathf.Min(currentZoomOutFactor + zoomAcceleration, maxZoomOutFactor) : Mathf.Max(currentZoomOutFactor - zoomAcceleration, 0);
        rf.cam.orthographicSize = Utility.Gameplay.Camera.VERTICAL_CAMERA_OFFSET * (1 + currentZoomOutFactor);

        //x
        if (player.transform.position.x + (rf.cam.orthographicSize * rf.cam.aspect) > MAX_BORDER || player.transform.position.x - (rf.cam.orthographicSize * rf.cam.aspect) < MIN_BORDER)
            lastPositionX = ((player.transform.position.x > 0) ? 1 : -1) * (MAX_BORDER - (rf.cam.orthographicSize * rf.cam.aspect));
        else
            lastPositionX = player.transform.position.x;

        //y
        if (player.transform.position.y + rf.cam.orthographicSize > MAX_BORDER || player.transform.position.y - (rf.cam.orthographicSize) < MIN_BORDER)
            lastPositionY = ((player.transform.position.y > 0) ? 1 : -1) * (MAX_BORDER - rf.cam.orthographicSize);
        else
            lastPositionY = player.transform.position.y;

        if (shakeEnabled) {
            currentShakeTimer += Time.unscaledDeltaTime;
            lastPositionX += Random.Range(-1f, 1f) * shakeIntensity;
            lastPositionY += Random.Range(-1f, 1f) * shakeIntensity;
            if (currentShakeTimer >= shakeDuration) {
                shakeEnabled = false;
                currentShakeTimer = 0f;
                shakeDuration = 0f;
            }
        }

        desiredPosition = new Vector3(lastPositionX, lastPositionY, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        //Debug.Log("Position: " + desiredPosition + ", Shake Enabled: " + shakeEnabled);
        //Debug.Log(Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET + " ,  " + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET + " || " + Constants.Gameplay.Camera.HORIZONTAL_CAMERA_OFFSET * (1 + maxZoomOutFactor) + " , " + Constants.Gameplay.Camera.VERTICAL_CAMERA_OFFSET * (1 + maxZoomOutFactor));
        //Debug.Log(Constants.Functions.GeneralFunctions.getRoundedFloat(lastPositionXNormal, 0) + " , " + Constants.Functions.GeneralFunctions.getRoundedFloat(lastPositionYNormal, 0) + "  ||  " + Constants.Functions.GeneralFunctions.getRoundedFloat(lastPositionXZoomedOut, 0) + " , " + Constants.Functions.GeneralFunctions.getRoundedFloat(lastPositionYZoomedOut, 0));
    }

    public void EnableShake(float duration, float intensity) {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeEnabled = true;
        currentShakeTimer = 0f;
    }
}
