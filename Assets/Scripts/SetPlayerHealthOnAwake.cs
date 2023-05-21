using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerHealthOnAwake : MonoBehaviour
{
    [SerializeField] private intVariable _playerHP;

    [SerializeField] private PlayerValues _playerValues;

    private void Awake()
    {
        _playerValues.playerHealth = _playerHP.Value;
    }
}
