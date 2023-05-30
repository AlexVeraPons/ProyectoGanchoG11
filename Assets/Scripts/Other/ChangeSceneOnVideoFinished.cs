using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ChangeSceneOnVideoFinished : MonoBehaviour
{
    [SerializeField] VideoPlayer _videoPlayer;
    [Tooltip("This must be 1 always")]
    [SerializeField] int _sceneID = 1;

    [Header("EXTRA FUNCITONS")]
    [SerializeField] bool _canSkip = false;
    [SerializeField] InputAction _skipAction;

    private void Awake()
    {
        _skipAction.Enable();
    }

    private void OnEnable()
    {
        _videoPlayer.loopPointReached += GoToScene;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= GoToScene;
    }

    private void Update()
    {
        if(_canSkip)
        {
            if(_skipAction.triggered)
            {
                GoToScene(_videoPlayer);
            }
        }
    }

    private void GoToScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(_sceneID);
    }
}
