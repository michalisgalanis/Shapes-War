using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoybuttonBounds : MonoBehaviour
{
    void Update(){
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        x = Mathf.Clamp(x, -0.5f, 0.5f);
        y = Mathf.Clamp(y, -0.5f, 0.5f);
        Vector3 newTransform = new Vector3(x, y, 0);
        transform.localPosition = newTransform;
    }
}
