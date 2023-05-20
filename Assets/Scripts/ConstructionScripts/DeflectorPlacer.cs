using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorPlacer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject deflectorPrefab;
    [SerializeField]
    private int deflectorCount = 0;
    [SerializeField] private ConstructionInterferenceChecker checker;
    public int DeflectorCount { get { return this.deflectorCount; } }
    [SerializeField] private GameObject selectionBall;
    [SerializeField] private PlayerValues playerValues;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 p = hit.point;
            p /= playerValues.gridSize;
            //Vector3 gridBasedPoint = new Vector3() * playerValues.gridSize;
        }
            
    }
}
