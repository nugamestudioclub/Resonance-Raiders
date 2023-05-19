using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionInterferenceChecker : MonoBehaviour
{

    /// <summary>
    /// Temp variable.
    /// </summary>
    [SerializeField]
    private float tempGridSize = 5f;

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
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
