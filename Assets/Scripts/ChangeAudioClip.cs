using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudioClip : MonoBehaviour
{
    [SerializeField] private AudioSource _combatAudioSource;
    [SerializeField] private AudioSource _preliminaryAudioSource;

    [SerializeField] private GameStateController _gameStateController;

    private bool combatIsPlaying = false;

    private void Update()
    {
       
        switch (_gameStateController.state)
        {
            case GameState.COMBAT:
                ChangeAudioPreliminary();
                break;
            case GameState.PRELIMINARY:
                ChangeAudioCombat();
                break;
        }
    }

    private void ChangeAudioCombat()
    {
        if (combatIsPlaying)
        {
            _preliminaryAudioSource.Play();
            _preliminaryAudioSource.volume += Time.deltaTime;

            _combatAudioSource.volume -= Time.deltaTime;
            if (_combatAudioSource.volume <= 0f)
            {
                _combatAudioSource.Stop();
                combatIsPlaying = false;
            }
        }


    }
    private void ChangeAudioPreliminary()
    {
        if (!combatIsPlaying)
        {
            _combatAudioSource.Play();
            _combatAudioSource.volume += Time.deltaTime;

            _preliminaryAudioSource.volume -= Time.deltaTime;
            if (_preliminaryAudioSource.volume <= 0f)
            {
                _preliminaryAudioSource.Stop();
                combatIsPlaying = true;
            }


        }

    }
}
