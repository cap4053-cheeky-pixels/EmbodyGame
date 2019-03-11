using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMenu : MonoBehaviour
{
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float timeUntilMenuLoad;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeUntilMenuLoad)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
