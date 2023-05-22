using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;

    [SerializeField] private PlayerValues _playerValues;

    private void Awake()
    {
        int newDamage = (int)_playerValues.defaultDisruptionDamage;

        if (newDamage != _damage)
        {
            Debug.LogWarning(string.Format("Player Damage has been modified from {0} to {1}.", _damage, newDamage));
        }

        _damage = newDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            _playerValues.playerHealth -= _damage;

            if (_playerValues.playerHealth <= 0)
            {
                _playerValues.playerHealth = 0;
                GameStateController.Controller.gameIsOver = true;
            }
        }
    }

}
