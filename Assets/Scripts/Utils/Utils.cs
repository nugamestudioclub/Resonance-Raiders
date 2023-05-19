using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Utils
{
    public static List<Vector3> GetPathPoints(PathCreator pathCreator, float size)
    {
        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < pathCreator.bezierPath.NumAnchorPoints; i++)
        {
            Vector3 point = pathCreator.bezierPath.GetPoint(i * 3);
            point = new Vector3(Mathf.Round(point.x / size) * size, 
                Mathf.Round(point.y / size) * size, 
                Mathf.Round(point.z / size) * size);
            path.Add(point);
        }
        return path;
    }
}
