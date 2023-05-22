using UnityEngine;

public class SetPlayerHealthOnAwake : MonoBehaviour
{
    [SerializeField] private intVariable _playerHP;

    [SerializeField] private PlayerValues _playerValues;

    // redundant and stupid? we're one hour off of submission :/ no thinking
    private void Awake()
    {
        _playerValues.playerHealth = _playerHP.Value;
    }

    public void OnEnable()
    {
        _playerValues.playerHealth = _playerHP.Value;
    }
}
