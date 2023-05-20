using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 1f;
    private int iterNum = 0;
    [SerializeField]
    private Transform child;
    private bool pActive = false;
    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(0, -90, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotSpeed*Time.deltaTime, 0);
        if (pActive != child.gameObject.activeSelf)
        {
            
        }

        pActive = child.gameObject.activeSelf;
    }
}
