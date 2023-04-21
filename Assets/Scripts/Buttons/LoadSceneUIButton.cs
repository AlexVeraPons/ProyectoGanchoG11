using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneUIButton : UIButton
{
    [SerializeField] string _sceneName;
    [SerializeField] int _sceneIndex = -1;

    public override void OnPress()
    {
        if (_sceneName != null)
        {
            Scene scene = SceneManager.GetSceneByName(_sceneName);
            if(scene != null)
            {
                SceneManager.LoadScene(_sceneName);
            }
            else
            {
                Debug.LogWarning("There's no scene called " + _sceneName);
            }
        }
        else if (_sceneIndex != -1)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(_sceneIndex);
            if (scene != null)
            {
                SceneManager.LoadScene(_sceneIndex);
            }
            else
            {
                Debug.LogWarning("There's no scene with index " + _sceneIndex.ToString());
            }
        }
        else
        {
            Debug.LogWarning("You forgot to add a Name or a Number for the scene!");
        }
    }
}
