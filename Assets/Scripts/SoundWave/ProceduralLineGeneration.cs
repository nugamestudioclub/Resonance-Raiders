
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class ProceduralLineGeneration : MonoBehaviour
{
    private LineRenderer line;
    private List<LineRenderer> lines;
    
    [SerializeField]
    [Range(1, 200)]
    private float width = 5f;
    [SerializeField]
    [Range(0, 200)]
    private float depth = 1f;
    [SerializeField]
    [Range(2, 200)]
    [Tooltip("Number of vertices in line")]
    private int verticesCount = 20;
    //[SerializeField]
    [Range(0,200)]
    [Tooltip("Number of divisions between two line vertices")]
    private int subdivisions = 5;

    [SerializeField]
    private float clipDistance = 1f;

    

    private List<List<Vector3>> pointsPerSubdivision = new List<List<Vector3>>();
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        lines = new List<LineRenderer> { line };
    }
    public List<Vector3> InterpolatePoints(Vector3 p1, Vector3 p2, Vector3 p3, int numPoints, int numSubdivisions)
    {
        List<Vector3> points = new List<Vector3>();

        // Interpolation between p1, p2, and p3
        for (int i = 0; i <= numPoints; i++)
        {
            float t = i / (float)numPoints;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // Ease out
            Vector3 point = CalculateBezierPoint(t, p1, p2, p3);
            points.Add(point);
        }

        // Subdividing interpolated points
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 start = points[i];
            Vector3 end = points[i + 1];

            for (int j = 1; j <= numSubdivisions; j++)
            {
                float t = j / (float)(numSubdivisions + 1);
                t = Mathf.Sin(t * Mathf.PI * 0.5f); // Ease out
                
                Vector3 point = Vector3.Lerp(start, end, t);
                points.Insert(i + j, point);
            }

            i += numSubdivisions;
        }

        return points;
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; //term 1
        p += 2 * u * t * p1; //term 2
        p += tt * p2; //term 3

        return p;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.IsPlaying(this))
        {
            if (line == null)
            {
                line = GetComponent<LineRenderer>();
            }
            int positionsCount = verticesCount * subdivisions;

            Vector3 leftPos = -line.transform.right * width;
            Vector3 rightPos = line.transform.right * width;
            Vector3 centerPos = line.transform.forward * depth;

            List<Vector3> points = InterpolatePoints(leftPos, centerPos, rightPos, verticesCount, subdivisions);
            line.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                line.SetPosition(i, points[i]);
            }
        }
        
        
        //Make sure to remove left over items.


    }

    public List<Vector3> GetPoints()
    {
        List<Vector3> points = new List<Vector3>();
        for(int i = 0; i < line.positionCount; i++)
        {
            if (subdivisions==0 ||i % subdivisions == 0)
            {
                points.Add(line.GetPosition(i));
            }
        }
        return points;
    }
    

    public void UpdatePoints(List<Vector3> newPoints)
    {
        List<Vector3> current = this.GetPoints();
        for(int i = 0; i < newPoints.Count; i++)
        {
            Vector3 deltaPos = newPoints[i] - current[i];
            if (subdivisions == 0)
            {
                line.SetPosition(i, newPoints[i]);
            }
            else
            {
                for (int ii = 0; ii < subdivisions; ii++)
                {
                    int index = i * subdivisions + ii;
                    if (index < line.positionCount)
                    {
                        line.SetPosition(index, line.GetPosition(index) + deltaPos);
                    }

                }
            }
            
        }
        
    }
}
