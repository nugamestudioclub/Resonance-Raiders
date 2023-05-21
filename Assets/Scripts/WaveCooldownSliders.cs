using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCooldownSliders : MonoBehaviour
{

    [SerializeField] private Slider _destructionSlider;
    [SerializeField] private Slider _disruptionSlider;

    [SerializeField] private ShootProjectile _shootProjectile;

    [SerializeField] private PlayerValues _playerValues;

    // Update is called once per frame
    void Update()
    {
        _destructionSlider.value = _shootProjectile._destructionCooldownTimer * (1 / _playerValues.destructionWaveCooldown);
        _disruptionSlider.value = _shootProjectile._disruptionCooldownTimer * (1 / _playerValues.disruptionWaveCooldown);

    }
}
