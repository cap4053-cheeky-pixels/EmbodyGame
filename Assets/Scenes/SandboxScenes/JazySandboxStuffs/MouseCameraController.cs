using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    public GameObject player;
    public float xMouseSensitivity;
    public float yMouseSensitivity;
    public float zoomSensitivity;
    public bool invertXMouse;
    public bool invertYMouse;
    private Vector3 offset;

    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.localPosition;
    }

    void LateUpdate()
    {
        //float inPlayerRotate = Input.GetAxis("TogglePlayerRotate");
        //float inCameraRotate = Input.GetAxis("ToggleCameraRotate");
        float inPlayerRotate = (Input.GetMouseButton(1)) ? 1 : 0;
        float inCameraRotate = (Input.GetMouseButton(0)) ? 1 : 0;
        if (inPlayerRotate > 0)
        {
            RotateCamera();
            RotatePlayer();
        }
        else if (inCameraRotate > 0)
        {
            RotateCamera();
        }
        Zoom();

        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
    }

    void RotateCamera()
    {
        //float inViewX = Input.GetAxis("View X");
        //float inViewY = Input.GetAxis("View Y");
        float inViewX = Input.GetAxis("Mouse X");
        float inViewY = Input.GetAxis("Mouse Y");

        float viewX = inViewX * Time.deltaTime * xMouseSensitivity;
        float viewY = inViewY * Time.deltaTime * yMouseSensitivity;

        viewX *= (invertXMouse) ? -1 : 1;
        viewY *= (invertYMouse) ? -1 : 1;

        Quaternion rotX = Quaternion.AngleAxis(viewX, Vector3.up);
        Quaternion rotY = Quaternion.AngleAxis(viewY, transform.right);

        offset = rotX * offset;
        offset = rotY * offset;

        // TODO Restrict y rotation
    }

    void RotatePlayer()
    {
        // Nullify y axis
        Vector3 newForward = transform.forward;
        newForward.y = 0;
        player.transform.forward = newForward;
    }

    void Zoom()
    {
        //float inScroll = Input.GetAxis("Mouse ScrollWheel");
        float inScroll = Input.mouseScrollDelta.y;
        float scroll = inScroll * Time.deltaTime * zoomSensitivity;

        offset += transform.forward * scroll;
    }
}
