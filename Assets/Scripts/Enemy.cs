using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 1;
    [SerializeField]
    private EnemyPathFollower pathFollower;

    // Start is called before the first frame update
    void Start()
    {
        if (pathFollower == null)
        {
            pathFollower = gameObject.GetComponent<EnemyPathFollower>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pathFollower.isPathFinished())
        {
            // TODO Reduce lives
            Destroy(gameObject);
        }
    }

    public void OnHit()
    {
        _health -= 1;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
