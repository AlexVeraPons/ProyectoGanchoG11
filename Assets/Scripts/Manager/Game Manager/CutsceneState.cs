using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneState : State
{
    VideoClip _videoClip;
    Scene _nextScene;

    float _timer = 0f;
    float _timeToFinish;
    bool _finished = false;

    public CutsceneState(StateMachine stateMachine) : base(stateMachine)
    {
        _videoClip = ((GameManagerStateMachine)stateMachine).Clip;
        _timeToFinish = (float)_videoClip.length;
        _nextScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        ResetTimer();
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(_timer < _timeToFinish) { _timer += Time.deltaTime; }
        else
        {
            if(_finished == false)
            {
                LoadNextScene();
                _finished = true;
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(_nextScene.buildIndex);
    }

    void ResetTimer()
    {
        _finished = false;
        _timeToFinish = 0;
    }
}