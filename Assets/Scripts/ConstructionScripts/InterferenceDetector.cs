using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterferenceDetector : MonoBehaviour
{
    [SerializeField]
    private List<DeflectorComponent> interferences = new List<DeflectorComponent>();
    public bool hasInterference { get { return interferences.Count > 0;} }

    private void Update()
    {
        for(int i = 0; i < interferences.Count; i++)
        {
            if (interferences[i] == null || !interferences[i].gameObject.activeInHierarchy)
            {
                interferences.RemoveAt(i);
                break;
            }
        }
    }
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
