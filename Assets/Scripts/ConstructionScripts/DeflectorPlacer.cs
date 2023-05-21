using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeflectorPlacer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject deflectorPrefab;
    
    public int deflectorCount = 0;
    [SerializeField] private ConstructionInterferenceChecker checker;
    
    [SerializeField] private GameObject selectionBall;
    [SerializeField] private PlayerValues playerValues;
    [SerializeField]
    private List<GameObject> builtObjects = new List<GameObject>();

    private Vector3 sBallOffset;

    private void OnEnable()
    {
        selectionBall.SetActive(true);
    }
    private void OnDisable()
    {
        selectionBall.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        sBallOffset = selectionBall.transform.localPosition;
        
    }


    public void Clear()
    {
        for(int i = 0; i < builtObjects.Count; i++)
        {
            Destroy(builtObjects[i]);
        }
        builtObjects.Clear();
    }

    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 p = hit.point / playerValues.gridSize;
            Vector3 gridBasedPoint = new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), Mathf.Round(p.z)) * playerValues.gridSize;
            selectionBall.transform.position = new Vector3(gridBasedPoint.x, 1, gridBasedPoint.z);
            print("Can build?:" + (!checker.HasCollision() && deflectorCount > builtObjects.Count).ToString());
            if (Input.GetMouseButtonDown(0)&&!checker.HasCollision()&&deflectorCount>builtObjects.Count)
            {
                
                GameObject deflector = Instantiate(deflectorPrefab, transform);
                deflector.transform.position = new Vector3(gridBasedPoint.x, 1, gridBasedPoint.z);
                builtObjects.Add(deflector);
                
            }
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (builtObjects.Count > 0)
            {
                GameObject obj = builtObjects[builtObjects.Count - 1];
                builtObjects.Remove(obj);
                Destroy(obj);
                
            }
            
        }

        
            
    }
}
