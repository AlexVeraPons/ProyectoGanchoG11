using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenLogic : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;
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
    }
    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
}
