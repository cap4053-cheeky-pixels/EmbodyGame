using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This class should be on every enemy that can move. No path finding
    or movement calculation should be done here, simple exposes a simple
    move method for scripts that do path finding.
*/
[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private CharacterController cc;
    private Vector3 cachedDirection;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        cc.detectCollisions = false;
    }

    // Method should only be called once per frame
    public void Move(Vector3 direction)
    {
        cc.SimpleMove(direction * speed);
    }
}
