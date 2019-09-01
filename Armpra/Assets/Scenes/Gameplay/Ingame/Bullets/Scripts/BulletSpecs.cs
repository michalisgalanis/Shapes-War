using UnityEngine;
public class BulletSpecs : MonoBehaviour {
    public Constants.Gameplay.Bullet.bulletTypes bulletType;
    [Range(20,150)] public float damage;
    [Range(1, 3)] public float acceleration;
    [Range(10, 60)] public float maxSpeed;
    public bool aoe;
    [HideInInspector] public float blastDamage;
    [HideInInspector] public float blastRadius;
}
