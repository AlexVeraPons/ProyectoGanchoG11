using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullScreenLogic : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;
    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;
    Resolution[] _resolutions;
    void Start()
    {
        if (Screen.fullScreen)
        {
            _toggle.isOn = true;
        }
        else
        {
            _toggle.isOn = false;
        }
        CheckResolutions();
    }
    public void CheckResolutions()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int actualResolution = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && _resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = actualResolution;
        _resolutionDropdown.RefreshShownValue();

        _resolutionDropdown.value = PlayerPrefs.GetInt("resolutionNumber", 0);
    }
    public void ChangeResolution(int resoltuionIndex)
    {
        PlayerPrefs.SetInt("resolutionNumber", _resolutionDropdown.value);
        Resolution resolution = _resolutions[resoltuionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
} 
