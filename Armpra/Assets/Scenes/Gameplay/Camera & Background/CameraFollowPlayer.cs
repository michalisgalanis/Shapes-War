using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform tf;
    private GameObject player;

    private Vector3 position;
    private float cameraSize;
    
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        tf = GetComponent<Transform>();
        Debug.Log(cameraSize = GetComponent<Camera>().scaledPixelHeight);
        Debug.Log(cameraSize = GetComponent<Camera>().scaledPixelWidth);

    }

    // Update is called once per frame
    void Update(){
        position = player.transform.position;
        position.z = -10;
        tf.position = position;
    }
}
