using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private Transform _shootingPointTransform;

    [SerializeField] private ObjectPool _destructionObjectPool;

    [SerializeField] private ObjectPool _disruptionObjectPool;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] private PlayerValues _playerValues;

    [HideInInspector] public float _destructionCooldownTimer;
    [HideInInspector] public float _disruptionCooldownTimer;

    private void Start()
    {
        _destructionCooldownTimer = _playerValues.destructionWaveCooldown;
        _disruptionCooldownTimer = _playerValues.disruptionWaveCooldown;
}

    private void Update()
    {
        _destructionCooldownTimer -= Time.deltaTime;
        _disruptionCooldownTimer -= Time.deltaTime;

        //left click
        if (Input.GetMouseButtonDown(0))
        {
            _playerValues.playerWaveType = PlayerValues.waveType.Destruction;
            if (_destructionCooldownTimer <= 0)

            {
                GameObject projectile = _destructionObjectPool.GetPooledObject();
                shoot(projectile);
                _destructionCooldownTimer = _playerValues.destructionWaveCooldown;
            }
        }

        //right click
        if (Input.GetMouseButtonDown(1))
        {
            _playerValues.playerWaveType = PlayerValues.waveType.Disruption;
            if (_disruptionCooldownTimer <= 0)
            {
                GameObject projectile = _disruptionObjectPool.GetPooledObject();
                shoot(projectile);
                _disruptionCooldownTimer = _playerValues.disruptionWaveCooldown;
            }
        }

    }



    void shoot(GameObject projectile)
    {
        if (projectile != null)
        {
            projectile.transform.position = _shootingPointTransform.position;
            projectile.SetActive(true);
            projectile.GetComponent<ProceduralLineGeneration2>().initialVelocity = _shootingPointTransform.forward;
            projectile.transform.forward = _shootingPointTransform.forward;
            //projectile.transform.rotation = Quaternion.identity;
            //projectile.transform.Rotate(new Vector3(0f, _playerTransform.rotation.y, 0f));
            //projectile.transform.rotation = Quaternion.Euler(0f, 0f, _playerTransform.rotation.y);
            //Debug.Log(projectile.transform.rotation);
            //Debug.Log(_playerTransform.rotation.y);
        }
    }
}
