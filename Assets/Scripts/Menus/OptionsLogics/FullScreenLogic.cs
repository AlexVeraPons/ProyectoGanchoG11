using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenLogic : MonoBehaviour
{
    [SerializeField]
    private ButtonLogic _buttonLogic;
    // private int _buttonIndex;
    private bool _isFullscreen;
    private void OnEnable()
    {
        _buttonLogic.OnChangeIndex += CheckIndex;
    }
    private void OnDisable()
    {
        _buttonLogic.OnChangeIndex -= CheckIndex;
    }
    void Start()
    {
        if (Screen.fullScreen)
        {
            _isFullscreen = true;
            _buttonLogic.Index = 0;
        }
        else
        {
            _isFullscreen = false;
            _buttonLogic.Index = 1;
        }

    }
    private void CheckIndex(int Index)
    {
        //Debug.Log("fullscreen");
        if (Index == 0)
        {
            _isFullscreen = true;
            Screen.fullScreen = _isFullscreen;
        }
        if (Index == 1)
        {
            _isFullscreen = false;
            Screen.fullScreen = _isFullscreen;
        }
    }
}
