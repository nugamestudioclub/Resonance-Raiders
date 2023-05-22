using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Camera worldCamera;

    private void Start()
    {
        worldCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(worldCamera.transform);
    }
}
