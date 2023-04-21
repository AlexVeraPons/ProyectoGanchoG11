using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManagerStateMachine : StateMachine
{
    public static GameManagerStateMachine GameManager;

    // Cutscene Related Values
    public VideoClip Clip => _videoClip;
    VideoClip _videoClip;

    List<Scene> _allScenes = new List<Scene>();
    Scene _currentScene = SceneManager.GetActiveScene();

    private void Awake()
    {
        if (GameManager == null)
        {
            GameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            _allScenes.Add(SceneManager.GetSceneByBuildIndex(i));
        }

        Initialize(new CutsceneState(this));
    }
}