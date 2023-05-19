using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWaveTypeIndicator : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;

    [SerializeField] private Image _waveTypeIndicatorImage;

    [SerializeField] private Material _destructionWaveMaterial;
    [SerializeField] private Material _disruptionWaveMaterial;

    void Update()
    {
        switch (_playerValues.playerWaveType)
        {
            case PlayerValues.waveType.Destruction:

                _waveTypeIndicatorImage.color = _destructionWaveMaterial.color;
                break;

            case PlayerValues.waveType.Disruption:
                _waveTypeIndicatorImage.color = _disruptionWaveMaterial.color;
                break;
        }

    }

}
