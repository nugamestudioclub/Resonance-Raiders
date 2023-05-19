using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private Transform _shootingPointTransform;



    [SerializeField] private ObjectPool _destructionObjectPool;

    [SerializeField] private ObjectPool _disruptionObjectPool;


    [SerializeField] private PlayerValues _playerValues;



    private float _destructionCooldownTimer = 0f;
    private float _disruptionCooldownTimer = 0f;

    private void Update()
    {
        _destructionCooldownTimer += Time.deltaTime;
        _disruptionCooldownTimer += Time.deltaTime;


        //left click
        if (Input.GetMouseButtonDown(0))
        {
            switch (_playerValues.playerWaveType)
            {
                case PlayerValues.waveType.Destruction:
                    if (_destructionCooldownTimer >= _playerValues.destructionWaveCooldown)
                    {
                        shoot();
                        _destructionCooldownTimer = 0f;
                    }

                    break;
                case PlayerValues.waveType.Disruption:

                    if (_disruptionCooldownTimer >= _playerValues.disruptionWaveCooldown)
                    {
                        shoot();
                        _disruptionCooldownTimer = 0f;
                    }
                    break;
            }

        }

    }

    void shoot()
    {
        GameObject projectile = null;

        switch (_playerValues.playerWaveType)
        {
            case PlayerValues.waveType.Destruction:
                projectile = _destructionObjectPool.GetPooledObject();
                break;
            case PlayerValues.waveType.Disruption:
                projectile = _disruptionObjectPool.GetPooledObject();
                break;
        }


        if (projectile != null)
        {
            projectile.transform.position = _shootingPointTransform.position;
            projectile.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody>().velocity = _shootingPointTransform.forward * 20f;
        }
    }
}
