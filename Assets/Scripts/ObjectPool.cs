using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    [SerializeField] private int _amountToPool;

    [SerializeField] private GameObject _prefabToPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject bullet = Instantiate(_prefabToPool);
            bullet.SetActive(false);
            _pooledObjects.Add(bullet);
        }
    }



    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }

}
