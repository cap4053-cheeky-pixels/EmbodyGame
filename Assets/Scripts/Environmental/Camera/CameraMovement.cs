using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float defaultSpeed;

    private Vector3 destinationPoint;
    private Vector3 initialPoint;
    private float startTime;
    private float distance;
    private float speed;

    public void LerpCameraMovement(Vector3 point, float speed)
    {
        // Reset animation variables
        this.speed = speed;
        destinationPoint = point;
        initialPoint = transform.position;
        startTime = Time.time;
        distance = Vector3.Distance(initialPoint, destinationPoint);
    }
    public void LerpCameraMovement(Vector3 point)
    {
        LerpCameraMovement(point, defaultSpeed);
    }

    void Awake()
    {
        destinationPoint = transform.position;
    }

    void Update()
    {
        if (transform.position == destinationPoint) return;
        float traveled = (Time.time - startTime) * speed;
        float fracTraveled = traveled / distance;
        transform.position = Vector3.Lerp(initialPoint, destinationPoint, fracTraveled);
    }
}
