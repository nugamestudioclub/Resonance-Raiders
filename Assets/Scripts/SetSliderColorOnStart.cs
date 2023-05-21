using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderColorOnStart : MonoBehaviour
{
    [SerializeField] private Material _destructionWaveMaterial;
    [SerializeField] private Material _disruptionWaveMaterial;

    [SerializeField] private Image _destructionSliderFill;
    [SerializeField] private Image _disruptionSliderFill;
    // Start is called before the first frame update
    void Start()
    {
        _destructionSliderFill.color = _destructionWaveMaterial.color;
        _disruptionSliderFill.color = _disruptionWaveMaterial.color;

    }


}
