using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyCC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!enabled || hit.rigidbody == null || hit.rigidbody.isKinematic) return;
        hit.rigidbody.AddForce(hit.moveDirection * 20);
    }
}
