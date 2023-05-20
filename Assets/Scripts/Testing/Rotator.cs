using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 1f;
    private int iterNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (iterNum == 5)
        {
            transform.Rotate(0, -90, 0);
            print(transform.eulerAngles.y);
        }*/
        iterNum++;
    }
}
