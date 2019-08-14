using UnityEngine;

public class IntendedParticleDeath : MonoBehaviour {
    private ParticleSystem ps;

    private void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (ps && !ps.IsAlive()) {
            Destroy(gameObject);
        }
    }
}
