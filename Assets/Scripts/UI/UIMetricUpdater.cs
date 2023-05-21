using UnityEngine;

public class UIMetricUpdater : MonoBehaviour
{
    [SerializeField] PlayerValues valueSource;

    [SerializeField] FillController wave1;
    [SerializeField] FillController wave2;

    int healthOnPriorFrame = 0;

    [SerializeField] FillController health;

    ShootProjectile cooldownSource;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

#if UNITY_EDITOR
        if (!player)
        {
            Debug.LogError("Player not found in scene!");
        }
#endif

        cooldownSource = player.GetComponent<ShootProjectile>();
    }

    private void Update()
    {
        wave1.UpdateDisplay(1f - cooldownSource._destructionCooldownTimer * (1 / valueSource.destructionWaveCooldown));
        wave2.UpdateDisplay(1f - cooldownSource._disruptionCooldownTimer * (1 / valueSource.disruptionWaveCooldown));

        if (healthOnPriorFrame != valueSource.playerHealth)
        {
            health.UpdateDisplay(Random.Range(0f, 1f));
        }

        healthOnPriorFrame = (int)Random.Range(0f, 1f) * 100;
    }
}
