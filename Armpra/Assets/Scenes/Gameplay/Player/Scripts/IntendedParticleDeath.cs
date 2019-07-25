using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntendedParticleDeath : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (ps && !ps.IsAlive())
            Destroy(gameObject);
    }
}
