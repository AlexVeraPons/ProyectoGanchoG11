using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
public class SliderLogic : MonoBehaviour
{
    private MenusMap _myInput;
    public Action<float> OnSliderValueChange;
    public Action OnChangeLeft;
    public Action OnChangeRight;
    void Start()
    {
        _myInput = new MenusMap();
        _myInput.ButtonNavegation.Enable();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            if (_myInput.ButtonNavegation.GoLeft.WasPressedThisFrame())
            {
                OnSliderValueChange?.Invoke(-0.1f);
                OnChangeLeft?.Invoke();
            }

            if (_myInput.ButtonNavegation.GoRight.WasPressedThisFrame())
            {
                OnSliderValueChange?.Invoke(0.1f);
                OnChangeRight?.Invoke();
            }
        }
    }
}
