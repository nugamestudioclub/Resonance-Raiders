using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyPathFollower : MonoBehaviour
{
    [SerializeField]
    private PathCreator pathCreator;
    [SerializeField]
    private float _speed = 3;
    private float _distanceTraveled;

    void Start()
    {
        if (pathCreator == null)
        {
            Debug.Log($"path creator of {name} isnt set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pathCreator != null && !isPathFinished())
        {
            _distanceTraveled += _speed * Time.deltaTime;

            this.transform.position = pathCreator.path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
            // TODO edit rotation logic when necessary
            Vector3 dir = pathCreator.path.GetDirectionAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
            float deg;
            deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, deg);
        }
    }

    public bool isPathFinished()
    {
        return _distanceTraveled >= pathCreator.path.length;
    }
}

