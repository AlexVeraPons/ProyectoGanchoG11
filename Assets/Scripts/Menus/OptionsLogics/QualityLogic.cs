using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityLogic : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _Dropdown;
    private int _quality;
    void Start()
    {
        _quality = PlayerPrefs.GetInt("qualityNumber", 3);
        _Dropdown.value = _quality;
        SetQuality();
    }

    public void SetQuality()
    {
        
    }
}
