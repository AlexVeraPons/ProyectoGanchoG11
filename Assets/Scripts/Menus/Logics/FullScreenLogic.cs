using UnityEngine;

public class FullScreenLogic : MonoBehaviour
{
    [SerializeField]
    private ButtonLogic _buttonLogic;
    private bool _isFullscreen;
    private void OnEnable()
    {
        _buttonLogic.OnChangeLeft += CheckIndex;
        _buttonLogic.OnChangeRight += CheckIndex;
    }
    private void OnDisable()
    {
        _buttonLogic.OnChangeLeft -= CheckIndex;
        _buttonLogic.OnChangeRight -= CheckIndex;
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
