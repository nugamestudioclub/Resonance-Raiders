using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterferenceDetector : MonoBehaviour
{
    private List<DeflectorComponent> interferences = new List<DeflectorComponent>();
    public bool hasInterference { get { return interferences.Count > 0;} }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DeflectorComponent>() != null)
        {
            interferences.Add(other.GetComponent<DeflectorComponent>());
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DeflectorComponent>() != null)
        {
            interferences.Remove(other.gameObject.GetComponent<DeflectorComponent>());
           
        }
    }
}
