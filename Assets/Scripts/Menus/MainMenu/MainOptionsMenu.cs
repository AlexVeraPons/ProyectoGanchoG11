using UnityEngine.SceneManagement;
using UnityEngine;

public class MainOptionsMenu : MonoBehaviour
{
    private ScenePicker _scenePicker;
    private string _mainMenuScene;
    void Start()
    {
        _scenePicker = GetComponent<ScenePicker>();
        _mainMenuScene = _scenePicker.scenePath.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //new input
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
}
