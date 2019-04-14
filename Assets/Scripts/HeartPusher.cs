using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPusher : MonoBehaviour
{
    public float pushMultiplier = 5;

    void OnCollisionEnter(Collision col)
    {
        PushHeart(col);
    }

    void OnCollisionStay(Collision col)
    {
        PushHeart(col);
    }

    void PushHeart(Collision col)
    {
        GameObject other = col.gameObject;
        if (!other.CompareTag("Heart")) return;

        Rigidbody heartRB = col.rigidbody;

        // Get the direction vector to push the object
        Vector3 direction = col.GetContact(0).point - transform.position;
        direction.y = 0; // Dont punt it plz
        heartRB.AddForce(direction * pushMultiplier, ForceMode.VelocityChange);
    }
}
