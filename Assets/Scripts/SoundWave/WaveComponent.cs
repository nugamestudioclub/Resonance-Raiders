using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ProceduralLineGeneration))] 
public class WaveComponent : MonoBehaviour
{
    private ProceduralLineGeneration line;
    private List<WaveCollider> colliders = new List<WaveCollider>();
    [SerializeField]
    private float colliderRadius = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        foreach(Vector3 v in line.GetPoints())
        {
            GameObject childCollider = new GameObject("Collider"+colliders.Count);
            childCollider.transform.parent = transform;
            childCollider.transform.localPosition = v;
            SphereCollider col = childCollider.AddComponent<SphereCollider>();
            col.radius = colliderRadius;
            col.isTrigger= true;
            WaveCollider collider = col.AddComponent<WaveCollider>();
            colliders.Add(collider);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
