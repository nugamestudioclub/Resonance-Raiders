using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetWaveTypeIndicator : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;

    [SerializeField] private Image _waveTypeIndicatorImage;
    [SerializeField] private TextMeshProUGUI _waveTypeIndicatorText;

    [SerializeField] private Material _destructionWaveMaterial;
    [SerializeField] private Material _disruptionWaveMaterial;

    private string _startingText;

    private void Start()
    {
        _startingText = _waveTypeIndicatorText.text;
    }

    void Update()
    {
        switch (_playerValues.playerWaveType)
        {
            case PlayerValues.waveType.Destruction:

                _waveTypeIndicatorImage.color = _destructionWaveMaterial.color;
                _waveTypeIndicatorText.text = _startingText + "Destruction";

                break;

            case PlayerValues.waveType.Disruption:
                _waveTypeIndicatorImage.color = _disruptionWaveMaterial.color;
                _waveTypeIndicatorText.text = _startingText + "Disruption";

                break;
        }

    }

}
