using UnityEngine;

public class IntendedParticleDeath : MonoBehaviour {
    private ParticleSystem ps;

    public void Start() {
        ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.main.duration);
    }
}
