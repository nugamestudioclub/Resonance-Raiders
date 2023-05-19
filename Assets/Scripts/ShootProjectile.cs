using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private Transform _shootingPointTransform;

    private void Start()
    {
        InvokeRepeating("shoot", 0f, 1f);
    }

    void shoot()
    {
        GameObject projectile = ObjectPool.instance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = _shootingPointTransform.position;
            projectile.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody>().velocity = _shootingPointTransform.forward * 20f;
        }
    }
}
