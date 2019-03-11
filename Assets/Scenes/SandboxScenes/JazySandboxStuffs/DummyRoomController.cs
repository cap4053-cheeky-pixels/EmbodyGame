using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRoomController : MonoBehaviour
{
    // Perhaps the camera controller should handle the height?
    public float cameraHeight;
    private CameraController cc;
    // Start is called before the first frame update
    void Awake()
    {
        cc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider c)
    {
        if (cc == null) return;
        Debug.Log("Apples");
        if (c.tag == "Player")
        {
            cc.MoveTo(transform.position + new Vector3(0, cameraHeight, 0));
        }
    }
}
