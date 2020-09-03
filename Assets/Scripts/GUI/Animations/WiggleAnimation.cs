using UnityEngine;

public class WiggleAnimation : MonoBehaviour {
    private float MAX_ROTATION;
    private float MAX_POSITION;
    private float frequency;
    private int infiniteTimer = 0;
    public bool rotate = true;
    public bool move = true;

    private void Start() {
        if (rotate) MAX_ROTATION = Random.Range(1.5f, 4f);
        if (move) MAX_POSITION = Random.Range(0.0015f, 0.002f);
        frequency = Random.Range(0.05f, 0.11f) * GameSettings.animationSpeedFactor;
    }

    void Update() {
        if (!GameSettings.animationsEnabled) return;
        if (rotate) GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Mathf.Sin(frequency * infiniteTimer++) * MAX_ROTATION);
        if (move) GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x + MAX_POSITION * Mathf.Cos(frequency * infiniteTimer), GetComponent<RectTransform>().position.y + MAX_POSITION * Mathf.Sin(frequency * infiniteTimer), 0);
    }
}
