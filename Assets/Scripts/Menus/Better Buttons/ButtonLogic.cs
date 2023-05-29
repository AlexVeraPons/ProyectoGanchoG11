using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using TMPro;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    private MenusMap _myInput;
    public Action<int> OnChangeLeft;
    public Action<int> OnChangeRight;
    private int _defaultIndex = 0;
    private int _index = 0;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
            _text.text = _data[Index];
        }
    }
    [SerializeField]
    private List<string> _data = new List<string>();

    public string value
    {
        get
        {
            return _data[_index];
        }
    }
    void Start()
    {
        _myInput = new MenusMap();
        _myInput.ButtonNavegation.Enable();

        _index = _defaultIndex;
        _text.text = _data[_index];
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            if (_myInput.ButtonNavegation.GoLeft.WasPressedThisFrame())
            {
                ClickedLeft();
            }

            if (_myInput.ButtonNavegation.GoRight.WasPressedThisFrame())
            {
                ClickedRight();
            }
        }
    }
    public void ClickedLeft()
    {
        if (_index == 0)
        {
            _index = _data.Count - 1;
        }
        else
        {
            _index--;
        }
        _text.text = _data[_index];
        OnChangeLeft?.Invoke(_index);
    }
    public void ClickedRight()
    {
        if ((_index + 1) >= _data.Count)
        {
            _index = 0;
        }
        else
        {
            _index++;
        }
        _text.text = _data[_index];
        OnChangeRight?.Invoke(_index);
    }
}
