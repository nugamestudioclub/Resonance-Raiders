using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private int _damage = 1;

    [SerializeField] private PlayerValues _playerValues;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            Debug.Log("Player took damage");
            _playerValues.playerHealth -= _damage;

            if (_playerValues.playerHealth <= 0)
            {
                _playerValues.playerHealth = 0;
                Debug.Log("Player out of health");
            }
        }
    }

}
