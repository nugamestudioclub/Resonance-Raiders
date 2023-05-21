using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridHandler : MonoBehaviour
{
    
    [SerializeField]
    private PlayerValues vals;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<MeshRenderer>().materials[0].SetVector("_Tiling", transform.localScale/vals.gridSize);
        
    }
}
