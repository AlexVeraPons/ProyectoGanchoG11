using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionLogic : MonoBehaviour
{
    [SerializeField]
    private ButtonLogic _buttonLogic;

    [SerializeField]
    private List<int> _widthResolutions;
    [SerializeField]
    private List<int> _heightResolutions;
        private void OnEnable()
    {
        _buttonLogic.OnChangeLeft += ChangeResolution;
        _buttonLogic.OnChangeRight += ChangeResolution;
    }
    private void OnDisable()
    {
        _buttonLogic.OnChangeLeft -= ChangeResolution;
        _buttonLogic.OnChangeRight -= ChangeResolution;
    }
    void Start()
    {

    }

    void Update()
    {
        //Debug.Log(_widthResolutions[_resolutionLogic.Index] + " " + _heightResolutions[_resolutionLogic.Index]);
    }
    public void ChangeResolution(int Index)
    {
        Screen.SetResolution(_widthResolutions[Index], _heightResolutions[Index], Screen.fullScreen);
    }
}
