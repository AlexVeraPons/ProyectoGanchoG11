using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrilloLogic : MonoBehaviour
{
    [SerializeField]
    private SliderLogic _sliderLogic;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Image _brightnessPanel;
    public float SliderValue;
    private void OnEnable()
    {
        _sliderLogic.OnSliderValueChange += ChangeBrightnessSlider;
    }
    private void OnDisable()
    {
        _sliderLogic.OnSliderValueChange -= ChangeBrightnessSlider;
    }

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("brillo", 0.05f);
        _brightnessPanel.color = new Color(_brightnessPanel.color.r, _brightnessPanel.color.g, _brightnessPanel.color.g, _slider.value);
    }
    private void ChangeBrightnessSlider(float value)
    {
        SliderValue -= value; //se que no es lo ideal, pero soluciona un problema que em dona el slider al ficarlo de left to right a right to left que de normal no passa
        _slider.value = SliderValue;
        ChechBrightness();
        PlayerPrefs.SetFloat("brillo", SliderValue);
        _brightnessPanel.color = new Color(_brightnessPanel.color.r, _brightnessPanel.color.g, _brightnessPanel.color.g, _slider.value);
    }
    public void ChangeSlider(float value)
    {
        SliderValue = value; //se que no es lo ideal, pero soluciona un problema que em dona el slider al ficarlo de left to right a right to left que de normal no passa
        _slider.value = SliderValue;
        //ChechBrightness();
        PlayerPrefs.SetFloat("brillo", SliderValue);
        _brightnessPanel.color = new Color(_brightnessPanel.color.r, _brightnessPanel.color.g, _brightnessPanel.color.g, _slider.value);
    }
    private void ChechBrightness()
    {
        if (SliderValue < _slider.minValue)
        {
            SliderValue = _slider.minValue;
        }
        if (SliderValue > _slider.maxValue)
        {
            SliderValue = _slider.maxValue;
        }
    }
}
