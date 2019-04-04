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

    void OnDestroy()
    {
        Vector3 direction = Vector3.Normalize(-pro.velocity);
        Vector3 position = transform.position;
        Emit(position, direction);
    }
}
