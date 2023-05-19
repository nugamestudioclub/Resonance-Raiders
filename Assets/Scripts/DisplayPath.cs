using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class DisplayPath : MonoBehaviour
{
    [SerializeField]
    private Sprite _image;
    [SerializeField]
    private float snapSize = 1;

    private PathCreator _pathCreator;

    public GameObject pathArrow;

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
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 point = points[i];
            Vector3 nextPoint = points[i + 1];

            GameObject gameObject = Instantiate(pathArrow, transform);

            gameObject.transform.position = point;
            gameObject.transform.LookAt(nextPoint);
        }
    }

    void OnPathChanged()
    {
        ClearPath();
        MakePath();
    }
}
