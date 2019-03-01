using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class ProjectileHitEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hitParticleSystem;
    private Projectile pro;

    void Awake()
    {
        pro = GetComponent<Projectile>();
    }

    void Emit(Vector3 position, Vector3 direction)
    {
        if (hitParticleSystem == null) return;
        Instantiate(hitParticleSystem, position + direction, Quaternion.LookRotation(direction));
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag != "Wall") return;

        Vector3 position = c.ClosestPointOnBounds(transform.position);
        Vector3 direction = Vector3.Normalize(-pro.velocity);
        Emit(position, direction);
    }
}
