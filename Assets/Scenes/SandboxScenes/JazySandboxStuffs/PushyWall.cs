using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyWall : MonoBehaviour
{
    public float force = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        Vector3 dir = c.transform.position - c.contacts[0].point;
        dir = dir.normalized;
        dir.y = 0;
        c.rigidbody.AddForce(dir * force, ForceMode.Impulse);
    }
}
