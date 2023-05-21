using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static WaveCollider;

public class ProceduralLineGeneration2 : MonoBehaviour
{
    private LineRenderer line;
    private List<LineRenderer> lines;

    [SerializeField]
    [Range(0, 200)]
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
    public WaveCollider.WaveType type;

    [SerializeField]
    private PlayerValues playerValues;
    [SerializeField] private float maxAliveTime = 20f;

    private int iter = 0;
    private bool firstTime = true;
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
        

    }
    private void OnEnable()
    {
        iter = 0;
        
    }

    void Generate(bool isNew)
    {
        int id = Random.Range(0, 1000);
        int positionsCount = verticesCount * subdivisions;

        Vector3 leftPos = -transform.right * width;
        Vector3 rightPos = transform.right * width;
        Vector3 centerPos = transform.forward * depth;

        List<Vector3> points = InterpolatePoints(leftPos, centerPos, rightPos, verticesCount, 0);
        //line.positionCount = points.Count;
        print("Points:"+points.Count);
        for (int i = 0; i < points.Count; i++)
        {
            GameObject childCollider;
            
            if (isNew)
                childCollider = Instantiate(colliderPrefab, transform);
            else
            {
                childCollider = colliders[i].gameObject;
                colliders[i].ResetPos();
                childCollider.transform.parent = transform;
                
            }
                
            childCollider.transform.localPosition = points[i];

            SphereCollider col = childCollider.GetComponent<SphereCollider>();

            WaveCollider collider = childCollider.GetComponent<WaveCollider>();
            collider.speed = speed;
            collider.velocity = initialVelocity - (collider.transform.forward * (((float)i / points.Count) - .5f) * spread);
            collider.velocityReductionOnHit = velocityMultiplierOnHit;
            collider.id = id;
            collider.type = type;
            collider.playerValues = playerValues;
            collider.timeLeft = maxAliveTime;

            switch (type)
            {
                case WaveCollider.WaveType.DAMAGE:
                    collider.damage = playerValues.defaultDestructionDamage;
                    collider.disruption = 0;

                    break;
                case WaveCollider.WaveType.DISRUPTION:
                    collider.damage = 0;
                    collider.disruption = playerValues.defaultDisruptionDamage;

                    break;
            }
            if (colliders.Count > 0)
                colliders[colliders.Count - 1].next = collider;
            colliders.Add(collider);
        }
    }


    Vector3 iRot;
    // Update is called once per frame
    void Update()
    {
        if(iter == 0)
        {
            iRot = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
        }
        if (iter == 1)
        {
            Generate(firstTime);
            firstTime = false;
            transform.eulerAngles = iRot;
        }
        if (iter > 3)
        {
            int disabledCount = 0;
            for (int i = 0; i < colliders.Count; i++)
            {
                if (!colliders[i].gameObject.activeInHierarchy)
                {
                    disabledCount++;
                }
            }
            if (disabledCount == colliders.Count)
            {
                gameObject.SetActive(false);
            }
        }

        iter++;
    }
}
