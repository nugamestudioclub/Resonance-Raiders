using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class ProceduralLineGeneration : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField]
    private float width;
    [SerializeField]
    [Tooltip("Number of vertices in line")]
    private int verticesCount;
    [SerializeField]
    [Tooltip("Number of divisions between two line vertices")]
    private int subdivisions;

    private List<List<Vector3>> pointsPerSubdivision = new List<List<Vector3>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        int positionsCount = verticesCount * subdivisions;
        for(int vertex =0; vertex < verticesCount; vertex++)
        {
            List<Vector3> curve;
            if (pointsPerSubdivision.Count <= vertex)
            {
                //Add more since no more positions
                curve = new List<Vector3>();
                pointsPerSubdivision.Add(curve);
            }
            else
            {
                //Set index
                curve = pointsPerSubdivision[vertex];
            }
            for(int subdiv =0; subdiv < subdivisions; subdiv++)
            {

            }
        }
        //Make sure to remove left over items.


    }
}
