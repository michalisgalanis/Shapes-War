using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform tf;
    private GameObject player;

    private Vector3 position;


    //constants
    private const float MIN_BORDER = -19f;
    private const float MAX_BORDER = 19f;
    private const float HORIZONTAL_CAMERA_OFFSET = 2.6f;
    private const float VERTICAL_CAMERA_OFFSET = 4.8f;

    private float lastPositionX;
    private float lastPositionY;

    void Start(){
        tf = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update(){
        if (player != null){
            if (followX())
                lastPositionX = player.transform.position.x;
            if (followY())
                lastPositionY = player.transform.position.y;
            position = new Vector3(lastPositionX, lastPositionY, -10);
            tf.position = position;
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
