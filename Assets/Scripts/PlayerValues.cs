using UnityEngine;

[CreateAssetMenu (fileName = "New PlayerValues", menuName = "PlayerValues")]
public class PlayerValues : ScriptableObject
{
    public int playerHealth;

    public float shootCooldown;

    public enum waveType { Destruction, Disruption };

    public waveType playerWaveType;

}
