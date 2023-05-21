using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;

    [SerializeField] private Slider _healthSlider;

    private float _maxHealth;

    private void Start()
    {
        _healthSlider.value = 1f;
        _maxHealth = _playerValues.playerHealth;
    }
    private void Update()
    {
        _healthSlider.value = _playerValues.playerHealth / _maxHealth;
        //print(_playerValues.playerHealth / _maxHealth);
    }
}
