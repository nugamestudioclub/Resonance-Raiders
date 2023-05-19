using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterferenceDetector : MonoBehaviour
{
    /*private List<Structure> interferences = new List<Structure>();*/
    public bool hasInteference { get { return interferences.Count > 0;} }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Structure>() != null)
        {
            interferences.Add((Structure)other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Structure>() != null)
        {
            interferences.Remove((Structure)other);
        }
    }*/
}
