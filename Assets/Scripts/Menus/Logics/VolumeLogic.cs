using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
public class VolumeLogic : MonoBehaviour
{
    [SerializeField]
    private SliderLogic _sliderLogic;
    [SerializeField]
    private Slider _slider;
    private void OnEnable()
    {
        _sliderLogic.OnSliderValueChange += ChangeSlider;
    }
    private void OnDisable()
    {
        _sliderLogic.OnSliderValueChange -= ChangeSlider;
    }
    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("volumeAudio", 0.05f);
        AudioListener.volume = _slider.value;
    }
    void ChangeSlider(float value)
    {
        _slider.value += value;
        ChechVolumes();
        PlayerPrefs.SetFloat("volumeAudio", _slider.value);
        AudioListener.volume = _slider.value;
        //am i mute
    }
    private void ChechVolumes()
    {
        if (_slider.value < _slider.minValue)
        {
            _slider.value = _slider.minValue;
        }
        if (_slider.value > _slider.maxValue)
        {
            _slider.value = _slider.maxValue;
        }
    }
}
