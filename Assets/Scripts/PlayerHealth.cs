using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;

    [SerializeField] private Transform _healthBarTransform;

    private int _damage = 1;

    private void Start()
    {
        _playerValues.playerHealth = 10;
    }
    private void Update()
    {
        _healthBarTransform.localScale = new Vector3(1f, 1f, _playerValues.playerHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage();
    }


    void TakeDamage()
    {
        Debug.Log("Player took damage");
        _playerValues.playerHealth -= _damage;
        if(_playerValues.playerHealth <= 0)
        {
            _playerValues.playerHealth = 0;
            Debug.Log("Player out of health");
        }
    }

}
