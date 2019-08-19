using UnityEngine;

public class BulletSpecs : MonoBehaviour
{
    [HideInInspector] public enum bulletTypes { NORMAL, HV, EXPLOSIVE, POISONOUS }

    public bulletTypes bulletType;
    public int damage;
    public float acceleration;
    public float maxSpeed;

}
