using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class CameraController : MonoBehaviour
{
    private CameraMovement cm;
    /**
        Will move the camera over the given location.
     */
    public void MoveTo(Vector3 point)
    {
        cm.LerpCameraMovement(point);
    }

    /**
        Overloaded version of the method with an added speed
        parameter.
    */
    public void MoveTo(Vector3 point, float speed)
    {
        cm.LerpCameraMovement(point, speed);
    }

    // Start is called before the first frame update
    void Awake()
    {
        cm = GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }
}

