using UnityEngine.UI;
using UnityEngine;

public class VolumeLogic : MonoBehaviour
{
    [SerializeField]
    private SliderLogic _sliderLogic;
    [SerializeField]
    private Slider _slider;
    private void OnEnable()
    {
        _sliderLogic.OnSliderValueChange += ChangeVolumeSlider;
    }
    private void OnDisable()
    {
        _sliderLogic.OnSliderValueChange -= ChangeVolumeSlider;
    }
    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("volumeAudio", 0.05f);
        AudioListener.volume = _slider.value;
    }
    void ChangeVolumeSlider(float value)
    {
        _slider.value += value;
        ChechVolumes();
        PlayerPrefs.SetFloat("volumeAudio", _slider.value);
        AudioListener.volume = _slider.value;
    }
    public void ChangeSlider(float value)
    {
        _slider.value = value;
        PlayerPrefs.SetFloat("volumeAudio", _slider.value);
        AudioListener.volume = _slider.value;
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
