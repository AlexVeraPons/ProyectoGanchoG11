using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionLogic : MonoBehaviour
{
    [SerializeField]
    private HorizontalSelector _resolutionLogic;
    [SerializeField]
    private List<int> _widthResolutions;
    [SerializeField]
    private List<int> _heightResolutions;
    void Start()
    {
        //_resolutions = Screen.resolutions;
        // _widthResolutions.Add(1920);
        // _widthResolutions.Add(1366);
        // _widthResolutions.Add(1536);

        // _heightResolutions.Add(1080);
        // _heightResolutions.Add(768);
        // _heightResolutions.Add(864);
    }

    void Update()
    {
        //Debug.Log(_widthResolutions[_resolutionLogic.Index] + " " + _heightResolutions[_resolutionLogic.Index]);
    }
    public void ChangeResolution()
    {
        Screen.SetResolution(_widthResolutions[_resolutionLogic.Index], _heightResolutions[_resolutionLogic.Index], Screen.fullScreen);
    }
}
