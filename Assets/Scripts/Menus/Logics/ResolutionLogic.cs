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

    public void ChangeResolution(int Index)
    {
        Screen.SetResolution(_widthResolutions[Index], _heightResolutions[Index], Screen.fullScreen);
    }
}
