using UnityEngine;

public class IntendedParticleDeath : MonoBehaviour {
    private ParticleSystem ps;

    public void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update() {
        if (Time.timeScale < 0.01f) {
            ps.Simulate(Time.unscaledDeltaTime, true, false);
        }
        if (ps && !ps.IsAlive()) {
            Destroy(gameObject);
        }
    }
}
