using UnityEngine;
using TMPro;

// probably couldve abstracted MetricUpdate and this but meh
public class UIDeflectorCounter : MonoBehaviour
{
    TextMeshProUGUI textUGUI;

    [SerializeField] DeflectorPlacer placer;

    private void Awake()
    {
        textUGUI = GetComponent<TextMeshProUGUI>();
        textUGUI.autoSizeTextContainer = false;

        if (!placer)
        {
            Debug.LogError("You must assign the DeflectorPlacer field on GameObject " + name);
        }
    }

    private void Update()
    {
        textUGUI.text = string.Format("x{0}", placer.deflectorCount - placer.GetCurrentCount());
    }
}
