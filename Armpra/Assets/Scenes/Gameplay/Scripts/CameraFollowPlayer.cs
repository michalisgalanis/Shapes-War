using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform tf;
    private GameObject player;

    Vector3 position;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        position = player.transform.position;
        position.z = -10;
        tf.position = position;
    }
}
