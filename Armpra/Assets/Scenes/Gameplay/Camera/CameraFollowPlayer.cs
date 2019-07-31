using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject player;

    private Vector3 desiredPosition;
    
    public Vector3 offset;
    public float smoothSpeed;

    //constants
    private const float MIN_BORDER = -19f;
    private const float MAX_BORDER = 19f;
    private const float HORIZONTAL_CAMERA_OFFSET = 2.6f;
    private const float VERTICAL_CAMERA_OFFSET = 4.8f;

    private float lastPositionX;
    private float lastPositionY;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate(){
       if (player != null)
        {
            if (followX())
                lastPositionX = player.transform.position.x;
            if (followY())
                lastPositionY = player.transform.position.y;
            desiredPosition = new Vector3(lastPositionX, lastPositionY, -10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }

    bool followX(){
        return (player.transform.position.x + HORIZONTAL_CAMERA_OFFSET <= MAX_BORDER) && (player.transform.position.x - HORIZONTAL_CAMERA_OFFSET >= MIN_BORDER);
    }

    bool followY()
    {
        return (player.transform.position.y + VERTICAL_CAMERA_OFFSET <= MAX_BORDER) && (player.transform.position.y - VERTICAL_CAMERA_OFFSET >= MIN_BORDER);
    }
}
