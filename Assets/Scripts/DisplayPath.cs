using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class DisplayPath : MonoBehaviour
{
    [SerializeField]
    private GameObject pathArrow;
    [SerializeField]
    private float snapSize = 1;

    private PathCreator _pathCreator;

    [SerializeField]
    private float yOffset = 1;
    [SerializeField]
    private float arrowScale = 1;
    [SerializeField]
    private LineRenderer line;

    public void RemoveColliders()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        rb.Sleep();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        _pathCreator = gameObject.GetComponent<PathCreator>();

        if (_pathCreator != null)
        {
            MakePath();
            _pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void ClearPath()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void MakePath()
    {
        List<Vector3> points = Utils.GetPathPoints(_pathCreator, snapSize);
        line.positionCount = points.Count;
        //line.useWorldSpace = true;
        for (int i = 0; i < points.Count-1; i++)
        {
            Vector3 point = points[i];
            Vector3 nextPoint = points[i + 1];
            
            line.SetPosition(i, points[i]);
            //line.SetPosition(1+(i*2), points[i]+Vector3.up*yOffset);
            BoxCollider col = line.gameObject.AddComponent<BoxCollider>();
            col.center = (point + ((nextPoint - point) / 2)) ;
            col.center = new Vector3(col.center.x, -1, col.center.z);
            col.size = new Vector3(Mathf.Max(0.9f, Mathf.Abs(nextPoint.x - point.x)),1,Mathf.Max(0.9f,Mathf.Abs(nextPoint.z-point.z)));
            col.isTrigger = false;
            
        }
        line.SetPosition(line.positionCount - 1, points[points.Count - 1]);
    }

    void OnPathChanged()
    {
        ClearPath();
        MakePath();
    }
}
