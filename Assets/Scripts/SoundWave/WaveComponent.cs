using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ProceduralLineGeneration))] 
public class WaveComponent : MonoBehaviour
{
    private ProceduralLineGeneration line;
    [HideInInspector]
    public List<WaveCollider> colliders = new List<WaveCollider>();
    [SerializeField]
    private float colliderRadius = 0.1f;
    [SerializeField]
    private float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<ProceduralLineGeneration>();
        foreach(Vector3 v in line.GetPoints())
        {
            GameObject childCollider = new GameObject("Collider"+colliders.Count);
            childCollider.transform.parent = transform;
            childCollider.transform.localPosition = v;
            SphereCollider col = childCollider.AddComponent<SphereCollider>();
            col.radius = colliderRadius;
            col.isTrigger= true;
            WaveCollider collider = childCollider.AddComponent<WaveCollider>();
            collider.speed = speed;
            colliders.Add(collider);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3> points = line.GetPoints();
        for(int i =0;i < points.Count;i++)
        {
            
            Vector3 pos = colliders[i].transform.localPosition;
            //Vector3 pos = points[i] + transform.forward * Time.deltaTime * speed;
            points[i] = pos;
        }
        
        line.UpdatePoints(points);
    }
}
