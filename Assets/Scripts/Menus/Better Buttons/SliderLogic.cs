using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
public class SliderLogic : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private MenusMap _myInput;
    public Action OnChangeLeft;
    public Action OnChangeRight;
    // Start is called before the first frame update
    void Start()
    {
        _myInput = new MenusMap();
        _myInput.ButtonNavegation.Enable();

        //_slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat("volumeAudio", 0.05f);
        AudioListener.volume = _slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            if (_myInput.ButtonNavegation.GoLeft.WasPressedThisFrame())
            {
                ChangeSlider(-0.1f);
                OnChangeLeft?.Invoke();
                Debug.Log("esquerdaSlider");
            }

            if (_myInput.ButtonNavegation.GoRight.WasPressedThisFrame())
            {
                ChangeSlider(0.1f);
                OnChangeRight?.Invoke();
                Debug.Log("dereitaSlider");
            }
        }
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
        if(_slider.value < _slider.minValue){
            _slider.value = _slider.minValue;
        }
        if(_slider.value > _slider.maxValue){
            _slider.value = _slider.maxValue;
        }
    }
}
