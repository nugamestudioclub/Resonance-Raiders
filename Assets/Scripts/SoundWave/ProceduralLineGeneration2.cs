using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLineGeneration2 : MonoBehaviour
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
    [Range(0, 5)]
    [SerializeField]
    [Tooltip("How far the wave expands on its sides")]
    private float spread = 0.1f;
    //[SerializeField]
    [Range(0, 200)]
    [Tooltip("Number of divisions between two line vertices")]
    private int subdivisions = 5;
    [Range(0,1)]
    [SerializeField]
    private float velocityMultiplierOnHit = 1f;

    [SerializeField]
    private float clipDistance = 1f;
    [HideInInspector]
    public List<WaveCollider> colliders = new List<WaveCollider>();
    [SerializeField]
    private float colliderRadius = .1f;
    
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private GameObject colliderPrefab;
    public Vector3 initialVelocity;
    [SerializeField]
    private WaveCollider.WaveType type;

    [SerializeField]
    private PlayerValues playerValues;
    [SerializeField] private float maxAliveTime = 20f;
    
    

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
    private void Awake()
    {
        if (GetComponentInChildren<ProceduralLineGeneration>() != null)
        {
            GetComponentInChildren<ProceduralLineGeneration>().gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.IsPlaying(this))
        {
            int id = Random.Range(0, 1000);
            int positionsCount = verticesCount * subdivisions;

            Vector3 leftPos = -transform.right * width;
            Vector3 rightPos = transform.right * width;
            Vector3 centerPos = transform.forward * depth;

            List<Vector3> points = InterpolatePoints(leftPos, centerPos, rightPos, verticesCount, subdivisions);
            //line.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                GameObject childCollider = Instantiate(colliderPrefab, transform);
                childCollider.transform.localPosition = points[i];
                
                SphereCollider col = childCollider.GetComponent<SphereCollider>();

                WaveCollider collider = childCollider.GetComponent<WaveCollider>();
                collider.speed = speed;
                collider.velocity = initialVelocity - (collider.transform.forward*(((float)i/points.Count)-.5f)*spread);
                collider.velocityReductionOnHit = velocityMultiplierOnHit;
                collider.id = id;
                collider.type = type;
                collider.playerValues = playerValues;
                collider.timeLeft = maxAliveTime;
                
                switch (type)
                {
                    case WaveCollider.WaveType.DAMAGE:
                        collider.damage = 1;
                        collider.disruption = 0;
                        
                        break;
                    case WaveCollider.WaveType.DISRUPTION:
                        collider.damage = 0;
                        collider.disruption = 1;
                        
                        break;
                }
                if (colliders.Count>0)
                    colliders[colliders.Count - 1].next = collider;
                colliders.Add(collider);
            }
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
