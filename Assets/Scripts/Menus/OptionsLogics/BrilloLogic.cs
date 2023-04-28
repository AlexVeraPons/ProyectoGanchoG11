using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrilloLogic : MonoBehaviour
{
    [SerializeField]
    private Slider _Slider;
    [SerializeField]
    private Image _brightnessPanel;
    public float SliderValue;

    void Start()
    {
        _Slider.value = PlayerPrefs.GetFloat("brillo", 0.05f);
        _brightnessPanel.color = new Color(_brightnessPanel.color.r, _brightnessPanel.color.g, _brightnessPanel.color.g, _Slider.value);
    }
    public void ChangeBrightnessSlider(float value)
    {
        SliderValue = value;
        PlayerPrefs.SetFloat("brillo", SliderValue);
        _brightnessPanel.color = new Color(_brightnessPanel.color.r, _brightnessPanel.color.g, _brightnessPanel.color.g, _Slider.value);
    }
}
