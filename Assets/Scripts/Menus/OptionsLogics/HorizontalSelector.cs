using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HorizontalSelector : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
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
        _index = _defaultIndex;
        _text.text = _data[_index];
    }

    public void OnLeftClick()
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
    }
    public void OnRightClick()
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
    }
}
