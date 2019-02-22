using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class InputController : MonoBehaviour
{
    private PlayerMovement pm;

    // Start is called before the first frame update
    void Awake()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (hori != 0 || vert != 0)
        {
            pm.Move(new Vector3(hori, 0, vert));
        }
    }
}
