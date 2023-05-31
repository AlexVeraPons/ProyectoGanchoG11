using UnityEngine;

public class TestForm : MonoBehaviour
{
    public HorizontalSelector FullScreen;
    private bool _isFullscreen;
    void Start()
    {
        if (Screen.fullScreen)
        {
            _isFullscreen = true;
            FullScreen.Index = 0;
        }
        else
        {
            _isFullscreen = false;
            FullScreen.Index = 1;
        }

    }
    void Update()
    {
    }
    public void ActivateFullScreen() //bool fullScreen
    {
        _isFullscreen = !_isFullscreen;
        Screen.fullScreen = _isFullscreen;
    }
}
