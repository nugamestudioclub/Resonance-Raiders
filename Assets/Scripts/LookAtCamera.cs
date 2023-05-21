using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Camera worldCamera;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(worldCamera.transform);
    }
}
