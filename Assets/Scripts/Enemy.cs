using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 10;
    public PathCreator pathCreator;
    [field: SerializeField]
    public float baseSpeed { get; private set; } = 3;
    public float speed;
    private float _distanceTraveled;
    private List<EnemyEffect> _effects = new List<EnemyEffect>();

    [SerializeField]
    private float yOffset = 1;

    public Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        if (pathCreator == null)
        {
            Debug.Log($"path creator of {name} isnt set");
        }
        else
        {
            transform.position = pathCreator.path.GetPointAtDistance(0);
        }
        _collider = gameObject.GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.Log("Collider not set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = baseSpeed;
        List<EnemyEffect> immuntableList = new List<EnemyEffect>(_effects);
        foreach (EnemyEffect e in immuntableList)
        {
            e.Apply(this);
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        if (isPathFinished())
        {
            // TODO Reduce lives
            Destroy(gameObject);
        }

        if (pathCreator != null)
        {
            _distanceTraveled += speed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
            transform.position += Vector3.up * yOffset;
            // TODO edit rotation logic when necessary
            Vector3 dir = pathCreator.path.GetDirectionAtDistance(_distanceTraveled, EndOfPathInstruction.Stop);
            float deg;
            deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, deg);
        }
    }

    /**
     * <summary>
     * Returns if the enemy has finished travelling the path
     * </summary>
     * */
    public bool isPathFinished()
    {
        return _distanceTraveled >= pathCreator.path.length;
    }

    /**
     * <summary>
     * Reduces the enemy's health by the given amount
     * </summary>
     */
    public void ReduceHealth(int red)
    {
        _health -= red;
    }

    public void AddEffect(EnemyEffect e)
    {
        e.GetType();
        _effects.Add(e);
    }

    public void RemoveEffect(EnemyEffect e)
    {
        _effects.Remove(e);
    }

}
