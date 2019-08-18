using UnityEngine;

public class JoybuttonBounds : MonoBehaviour {
    private void Update() {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        Vector3 newTransform = Vector3.ClampMagnitude(new Vector3(x, y, 0), 0.5f);
        transform.localPosition = newTransform;
    }
}