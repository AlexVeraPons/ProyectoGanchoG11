using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField]
    private float _sliderValue;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat("volumeAudio", 0.05f);
        AudioListener.volume = _slider.value;
        //maybe funcio per a ficar una imagen de estar mute    
    }
    public void ChangeSlider(float value)
    {
        _sliderValue = value;
        PlayerPrefs.SetFloat("volumeAudio", _sliderValue);
        AudioListener.volume = _slider.value;
        //am i mute
    }
}
