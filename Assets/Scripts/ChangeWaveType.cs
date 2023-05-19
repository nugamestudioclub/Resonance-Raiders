using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWaveType : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;
    
    void Update()
    {
        //right click
        if (Input.GetMouseButtonDown(1))
        {
            switch (_playerValues.playerWaveType)
            {
                case PlayerValues.waveType.Destruction:
                    _playerValues.playerWaveType = PlayerValues.waveType.Disruption;
                    break;
                case PlayerValues.waveType.Disruption:
                    _playerValues.playerWaveType = PlayerValues.waveType.Destruction;
                    break;
            }

        }
    }

}
