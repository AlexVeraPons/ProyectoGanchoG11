using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class LeaveGameOnVideoFinished : MonoBehaviour
{
    [SerializeField] VideoPlayer _videoPlayer;
    
    [Header("EXTRA FUNCITONS")]
    [SerializeField] bool _canSkip = false;
    [SerializeField] InputAction _skipAction;

    private void Awake()
    {
        _skipAction.Enable();
    }

    private void OnEnable()
    {
        _videoPlayer.loopPointReached += QuitApp;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= QuitApp;
    }

    private void Update()
    {
        if(_canSkip)
        {
            if(_skipAction.triggered)
            {
                Application.Quit();
            }
        }
    }

    private void QuitApp(VideoPlayer vp)
    {
        Application.Quit();
    }
}
