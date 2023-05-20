using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionInterferenceChecker : MonoBehaviour
{

    /// <summary>
    /// Temp variable.
    /// </summary>
    [SerializeField]
    private PlayerValues playerValues;

    [SerializeField]
    private InterferenceDetector detector;
    
    /// <summary>
    /// Function will return if there exists a collision in current structure.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="cam"></param>
    /// <returns>true if collision exists, false if none exists.</returns>
    public bool HasCollision()
    {
        return detector.hasInterference;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            point /= playerValues.gridSize;
            point = new Vector3(Mathf.Round(point.x), Mathf.Round(point.y), Mathf.Round(point.z));
            point *= playerValues.gridSize;
            detector.transform.position = new Vector3(point.x,1,point.z);
        }
    }
}
