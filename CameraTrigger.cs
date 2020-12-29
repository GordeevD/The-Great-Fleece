using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Transform myCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //   Debug.Log("Trigger Activated");
            Activate();
        }
    }

    public void Activate()
    {
        Camera.main.transform.position = myCamera.transform.position;
        Camera.main.transform.rotation = myCamera.transform.rotation;
    }
}
