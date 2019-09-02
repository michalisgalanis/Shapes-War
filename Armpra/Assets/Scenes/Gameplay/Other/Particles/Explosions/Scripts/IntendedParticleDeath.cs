using UnityEngine;

public class IntendedParticleDeath : MonoBehaviour {
    private ParticleSystem ps;

    public void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update() {
        ps.Simulate((Time.timeScale < 0.01f) ? Time.unscaledDeltaTime : Time.deltaTime, true, false);
        if (ps && !ps.IsAlive())
            Destroy(gameObject);
    }
}
